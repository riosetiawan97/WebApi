using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Data.Models
{
    public  class ResponseModel
    {
        public int StatusCode { get; set; }
        public string ? Message { get; set; }
    }
}
