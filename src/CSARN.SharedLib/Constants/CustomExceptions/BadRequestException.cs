using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSARN.SharedLib.Constants.CustomExceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message = "Specified data is unacceptable.") : base(message)
        {
        }
    }
}
