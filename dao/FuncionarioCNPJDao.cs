using ConsoleApp.exception;
using ConsoleApp.model;
using ConsoleApp.repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.dao
{
    internal class FuncionarioCNPJDao : GenericDao<FuncionarioCNPJ, Guid>, IFuncionarioCNPJDao
    {
        public FuncionarioCNPJDao(IList<FuncionarioCNPJ> lista) : base(lista)
        {
        }
    }


}
