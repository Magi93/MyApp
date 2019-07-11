using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        public IUserActions _userActions;
        public UserController(IUserActions userActions)
        {
            _userActions = userActions;
        }
        // GET: api/User

        [HttpGet]
        public async Task<ApiResponse> GetAllUser()
        {
            var response = new ApiResponse();
            response = await _userActions.GetAllUser();
            return response;
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        //// POST: api/User
        //[HttpPost]
        //public async Task<ApiResponse> Post([FromBody] User user)
        //{
        //    //string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        //    //using (SqlConnection connection = new SqlConnection(connectionString))
        //    //{
        //    //    string sql = $"Insert Into User (UserName, Email, Password) Values ('{user.UserName}', '{user.Email}','{user.Password}')";
        //    //    using (SqlCommand command = new SqlCommand(sql, connection))
        //    //    {
        //    //        command.CommandType = CommandType.Text;
        //    //        connection.Open();
        //    //        command.ExecuteNonQuery();
        //    //        connection.Close();
        //    //    }

        //    //}
        //   
        //}
        // POST: api/User
        [HttpPost]
        public async Task<ApiResponse> Post(PostUser user)
        {
            var res = await _userActions.Insert(user);
            return res;
        }



        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
