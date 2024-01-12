using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Exceptions.VehicleTypeExceptions
{
    internal class VehicleTypeNotFoundByIdException : Exception, IBaseException
    {
        public string errorMessage { get; set; }

        public HttpStatusCode StatusCode =>  HttpStatusCode.BadRequest;
        public VehicleTypeNotFoundByIdException(string message)  : base(message) 
        { 
         errorMessage = message;
        }
    }
}
