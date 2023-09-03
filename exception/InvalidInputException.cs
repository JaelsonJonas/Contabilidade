using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp.exception
{
    internal class InvalidInputException : Exception
    {
        public InvalidInputException() {}

        public InvalidInputException(string msg) : base(msg) {}

    }
}
