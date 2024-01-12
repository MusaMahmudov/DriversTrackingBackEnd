﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Exceptions.UserExceptions
{
    public class ChangePasswordFailException : Exception, IBaseException
    {
        public string errorMessage { get; set; }

        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public ChangePasswordFailException(IEnumerable<IdentityError> errors)  
        {
            errorMessage = string.Join(",",errors.Select(e=>e.Description));
        }
    }
}