using ConsoleApp.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp.repositores
{
    internal interface IGenericDao<T,K>
    {
        int CountAll();
        void ListAll();
        T GetById(K id);

        decimal GetTotalByMonth(K id);

        decimal CalculoCustoTotal();

        void Save(T funcionario);

    }
}
