using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeNext.Models
{
    public class HttpResponse<T>
    {
        public T Data { get; set; }
        public List<T> DataList { get; set; } = new List<T>();
        public bool IsSuccess { get; set; } 
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
        public int ErrorCode { get; set; }

        public List<T> Result { get; set; } = new List<T>();

        //public Dictionary<string, List<T>> Result { get; set; } = new Dictionary<string, List<T>>();
    }
}
