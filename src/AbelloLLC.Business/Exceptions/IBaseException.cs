using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Exceptions
{
    public interface IBaseException
    {
        public string errorMessage { get;  }
        public HttpStatusCode StatusCode { get;  }

    }
}
