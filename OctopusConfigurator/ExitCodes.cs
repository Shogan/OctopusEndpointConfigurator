using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusConfigurator
{
    public static class ExitCodes
    {
        public enum ExitCode : int
        {
            Success = 0,
            InvalidCommandSpecified = 1,
            NoActionTaken = 2,
            UnknownError = -1,
            
        }
    }
}
