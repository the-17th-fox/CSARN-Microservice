using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSARN.SharedLib.Constants
{
    public abstract class CommonLoggingForms
    {
        public const string MicroserviceIsAboutToRun = "[{MsvcName}] is about to run.";
        public const string RequiredParamsMissed = "Required parameters hasn't been provided: [{params}]";
        public const string ExceptionForm = "Unhandled exception has occured: [{Name}]:[{message}]";
    }
}
