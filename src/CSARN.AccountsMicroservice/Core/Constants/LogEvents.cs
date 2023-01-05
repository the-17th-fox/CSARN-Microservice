using CSARN.SharedLib.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
    public class LogEvents : CommonLoggingForms
    {
        public const string LoginAttempt = "The user [{email}] is trying to log in";
        public const string TokenReturned = "JWT was successfully signed and returned to [{email}]";
    }
}
