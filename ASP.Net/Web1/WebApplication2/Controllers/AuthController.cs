using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration { get; }
        public IUserActions _userAction;
        public AuthController(IConfiguration configuration, IUserActions userActions)
        {
            _configuration = configuration;
            _userAction = userActions;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ApiResponse> Authenticate([FromBody]Login userParam)
        {

            var response = await _userAction.Authenticate(userParam.UserName, userParam.Password);

            return response;
        }

        //[Route("login")]
        //[HttpPost]
        //public async Task<ApiResponse> Login([FromBody] Login model)
        //{
        //    try
        //    {
        //        var response = new ApiResponse();
        //        var username = model.UserName;
        //        if (username != null & _userAction.CheckPasswordAsync(username, model.Password))
        //        {
        //            var claim = new[]{
        //                   new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,model.UserName)};

        //            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt").GetSection("Key").Value));
        //            int expiryInMinutes = Convert.ToInt32(_configuration.GetValue<string>("Jwt:ExpiryInMinutes"));

        //            var token = new JwtSecurityToken(
        //            issuer: _configuration["Jwt:Issuer"],
        //            audience: _configuration["Jwt:Issuer"],
        //            expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
        //            signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256));
        //            response.Response = new
        //            {
        //                token = new JwtSecurityTokenHandler().WriteToken(token),
        //                expiration = token.ValidTo
        //            };
        //            response.ResponseCode = System.Net.HttpStatusCode.OK;
        //            response.Message = "";
        //            return response;

        //        }
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
    }
}