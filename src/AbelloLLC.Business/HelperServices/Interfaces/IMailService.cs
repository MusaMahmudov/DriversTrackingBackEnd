using AbelloLLC.Business.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.HelperServices.Interfaces
{
    public interface IMailService 
    {
        Task SendEmail(MailRequestDTO mailRequestDTO);
    }
}
