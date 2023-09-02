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
    internal abstract class GenericDao<T,K> : IGenericDao<T, K> where T : Pessoa
    {
        private readonly IList<T> _lista;
        
        public GenericDao(IList<T> lista ) { _lista = lista; }
        
        public decimal CalculoCustoTotal()
            {
                decimal total = 0;
                
                    foreach (T funcionario in _lista)
                    {
                        total += funcionario.CalculoCusto();
                    }
                    return total;
                
            }

        public int CountAll()
        {
            return _lista.Count;
        }

        public T GetById(K id)
        {
            foreach (T funcionario in _lista)
            {
                if (funcionario.IdRegister.Equals(id))
                {
                    return funcionario;
                }
            }

            throw new FuncionarioNotFoundException("Funcionario not Found");
        }

        public decimal GetTotalByMonth(K id)
        {
            return GetById(id).CalculoCusto();
        }

        public void ListAll()
        {
            if (_lista.Count > 0)
            {
                Console.WriteLine("Segue lista de Funcionarios: \n");
                foreach (T func in _lista)
                {
                    Console.WriteLine(func.ToString() + "\n");
                    Console.WriteLine("------------------------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Nenhum funcionario cadastrado!\n");
            }

        }

        public void Save(T funcionario)
        {
            funcionario.IdRegister = Guid.NewGuid();
            _lista.Add(funcionario);
        }
    }
}
