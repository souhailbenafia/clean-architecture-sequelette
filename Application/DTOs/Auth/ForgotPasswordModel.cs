using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class ForgotPasswordModel
    {
        public string UserName { get; set; }

        public string ClientUrl { get; set; }
    }
}
