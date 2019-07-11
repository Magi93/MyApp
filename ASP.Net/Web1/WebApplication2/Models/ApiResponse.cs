using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class ApiResponse : BaseResponse
    {
        [JsonProperty("response")]
        public dynamic Response { get; set; }
    }
    public class BaseResponse
    {
        [JsonProperty("responsecode")]
        public HttpStatusCode ResponseCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("callstarttime")]
        public string CallStartTime { get; set; }
        [JsonProperty("callendtime")]
        public string CallEndTime { get; set; }

        private readonly string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

        public BaseResponse()
        {
            this.CallStartTime = DateTime.UtcNow.ToString(_dateTimeFormat);
            //this.ServerName = System.Environment.MachineName;
        }

        public void Complete()
        {
            this.CallEndTime = DateTime.UtcNow.ToString(_dateTimeFormat);
        }
    }
}
