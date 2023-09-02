using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.exception
{
    internal class FuncionarioNotFoundException : Exception
    {
        public FuncionarioNotFoundException(string msg) : base(msg) { }    
    }
}
