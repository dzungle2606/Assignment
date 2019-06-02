using System;
using Newtonsoft.Json;

namespace HelixAssignment.DataContracts
{
    public class Result
    {
        public Result()
        {
        }

        public Object Data { get; set; }
        public string Message { get; set; }
        public bool ResponseResult { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
