using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure
{
    public interface IEmailSender
    {
       void SendEmail(Email email);
    }
}
