using ConsoleApp.model;
using ConsoleApp.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.repositores;

namespace ConsoleApp.dao
{
    internal class FuncionarioCLTDao : GenericDao<FuncionarioCLT,Guid> , IFuncionarioCLTDao
    {
        public FuncionarioCLTDao(IList<FuncionarioCLT> lista) : base(lista)
        {
        }
    }
}
