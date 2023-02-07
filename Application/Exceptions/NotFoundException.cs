using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Exceptions
{
    public class NotFoundException :ApplicationException 
    {
        public NotFoundException(string name , object key):base ($"{name} ({key}) was not found")
        {

        }
    }
}
