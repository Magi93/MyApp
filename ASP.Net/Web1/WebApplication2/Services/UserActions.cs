using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class UserActions : IUserActions
    {
        IConfiguration _config { get; }
        private readonly string connectionString;
        public UserActions(IConfiguration config)
        {
            _config = config;
            connectionString = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<ApiResponse> Authenticate(string username, string password)
        {
            var response = new ApiResponse();
            response = await GetAllUser();
            List<User> users = response.Response;
            if (users != null)
            {
                var user = users.Find((x) => x.UserName == username && ValidatePassword(x.Password) == password);
                // var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);        

                if (user == null)
                {
                    response.Response = null;
                    response.Message = "Username or password is incorrect";
                    response.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }


                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.GetSection("Jwt").GetSection("Key").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                response.Response = new
                {
                    username = user.UserName,
                    role = user.Role,
                    token = tokenHandler.WriteToken(token),
                };
            }
            //user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            //user.Password = null;

            return response;
        }

        public string ValidatePassword(string cipherPassword)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string plainText = Cryptography.Decrypt(
                cipherPassword,
                "TestPassphrase",
                salt.ToString(),
                "SHA256",
                10000,  //can be any number
                "!1A3g2D4s9K556g7",// must be 16 bytes
                256                // can be 192 or 128
                );
            return plainText;
        }

        public string GeneratePassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            //return hashed;

            string cipherText = Cryptography.Encrypt
                                (
                                 password,
                                "TestPassphrase",
                                 salt.ToString(),
                                 "SHA256",
                                 10000,
                                 "!1A3g2D4s9K556g7",
                                 256
                                );
            return cipherText;
        }

        public async Task<ApiResponse> Insert(PostUser user)
        {
            var response = new ApiResponse();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_AddUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userName", user.UserName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", GeneratePassword(user.Password));
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.Parameters.AddWithValue("@addedBy", user.AddedBy);
                    await con.OpenAsync();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                response.Response = user;
                response.Message = "User has been added successfully.";
                response.ResponseCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Response = null;
                response.Message = ex.Message;
                response.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                return response;
                throw ex;
            }
        }
        public async Task<ApiResponse> GetAllUser()
        {
            try
            {
                string procedureName = "[usp_GetAllUsers]";
                var response = new ApiResponse();
                var result = new List<User>();
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State != ConnectionState.Open)
                    con.Open();

                using (SqlCommand command = new SqlCommand(procedureName, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.Add(new SqlParameter("@Country", country));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = Convert.ToInt32(reader["id"]);
                            user.UserName = reader["username"].ToString();
                            user.Email = reader["email"].ToString();
                            user.Password = reader["password"].ToString();
                            user.Role = Convert.ToInt32(reader["role"]);
                            user.AddedOn = Convert.ToDateTime(reader["addedon"]);
                            user.AddedBy = Convert.ToInt32(reader["addedby"]);
                            user.Active = Convert.ToBoolean(reader["Active"]);
                            result.Add(user);
                        }
                    }
                }
                response.Response = result;
                response.ResponseCode = System.Net.HttpStatusCode.OK;
                return response;

            }
            catch (Exception ex)
            {
                var response = new ApiResponse();
                response.Response = null;
                response.ResponseCode = System.Net.HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
                throw ex;
            }
        }
    }
}
