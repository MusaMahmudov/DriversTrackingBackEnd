using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.DTOs.Common
{
    public class ResponseDTO
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ResponseDTO(string message,HttpStatusCode statusCode) 
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
