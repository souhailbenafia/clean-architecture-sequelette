using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Email
    {
        public List<MailboxAddress>? To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public Email(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("email",x)));
            Subject = subject;
            Body = content;
        }

    }
}
