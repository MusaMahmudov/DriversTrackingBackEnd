using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Exceptions.UserExceptions
{
    public class UserNotFoundByIdException : Exception, IBaseException
    {
        public string errorMessage { get; set; }

        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public UserNotFoundByIdException(string message) : base(message) 
        {
        errorMessage = message;
        }
    }
}
