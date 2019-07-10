using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace InstinetTicketer
{
    public class Logger
    {
        private string Message { get; set; }
        private List<string> ErrorObjects { get; set; }


        public Logger(string _message)
        {
            Message = _message;
        }

        public Logger(string _message, List<string> _errorObj)
        {
            Message = _message;
            ErrorObjects = _errorObj;
        }

        private string generateErrorMessage(List<string> __errorObj)
        {
            var error = String.Join(", ",__errorObj);
            return error;

        }


        public void Log()
        {

            System.IO.File.AppendAllText(@"G:\adas\Instinet\Logger\Logger.txt", "\r\n  <- " + DateTime.Now + " -> \r\n" + Message + "\r\n" + generateErrorMessage(ErrorObjects));

        }
    }
}
