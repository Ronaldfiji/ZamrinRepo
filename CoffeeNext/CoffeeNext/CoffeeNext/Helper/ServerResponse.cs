using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeNext.Helper
{
    public class ServerResponse<T>
    {
        //public  T Data { get; set; } = default(T);
        public List<T> DataList { get; set; } = new List<T>();
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
        public Dictionary<string, string> ErrorStatusCodes { get; set; } = new Dictionary<string, string>();
        public DateTime? ExpireDate { get; set; }

    }

    public class Result<T>
    {
        //public ServerResponse<object> serverResponse { get; set; }
        public List<T> DataList { get; set; } = new List<T>();
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
        public Dictionary<string, string> ErrorStatusCodes { get; set; } = new Dictionary<string, string>();
        public DateTime? ExpireDate { get; set; }
    }
    public class ApiResponse
    {
        public Result<object> result {get; set;}
        public string message { get; set; }
    }

}
