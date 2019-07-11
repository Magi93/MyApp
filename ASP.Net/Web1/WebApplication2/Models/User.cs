using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("role")]
        public int Role { get; set; }

        internal string Token { get; set; }
        [JsonProperty("addedon")]
        public DateTime AddedOn { get; set; }
        [JsonProperty("addedby")]
        public int AddedBy { get; set; }
        [DefaultValue(true)]
        internal bool Active { get; set; }
    }
    public class PostUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        [JsonProperty("username")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("role")]
        public int Role { get; set; }

        internal string Token { get; set; }
        [JsonProperty("addedon")]
        public DateTime AddedOn { get; set; }
        [JsonProperty("addedby")]
        public int AddedBy { get; set; }
        [DefaultValue(true)]
        internal bool Active { get; set; }
    }
    public class Login
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        public DateTime LoggedIn { get; set; }
        public DateTime LoggedOut { get; set; }
    }
}
