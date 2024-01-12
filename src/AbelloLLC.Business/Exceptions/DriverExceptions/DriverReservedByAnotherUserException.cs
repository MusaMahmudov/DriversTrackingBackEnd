using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Exceptions.DriverExceptions
{
    public class DriverReservedByAnotherUserException : Exception, IBaseException
    {
        public string errorMessage { get; set; }

        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public DriverReservedByAnotherUserException(string message) : base(message) 
        {
            errorMessage = message;
        }
    }
}
