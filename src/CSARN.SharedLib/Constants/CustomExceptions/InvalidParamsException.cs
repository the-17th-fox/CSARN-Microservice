using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSARN.SharedLib.Constants.CustomExceptions
{
    public class InvalidParamsException : Exception
    {
        public InvalidParamsException(string? message = "Required parameters weren't provided.") : base(message)
        {
        }
    }
}
