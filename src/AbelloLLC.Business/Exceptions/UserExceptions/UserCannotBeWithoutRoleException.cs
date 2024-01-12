using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Exceptions.UserExceptions
{
    public class UserCannotBeWithoutRoleException : Exception, IBaseException
    {
        public string errorMessage { get; set; }

        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public UserCannotBeWithoutRoleException(string message): base(message) 
        {
         errorMessage = message;
        }

    }
}
