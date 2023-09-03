using ConsoleApp.repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.model
{
    internal abstract class Pessoa
    {
        public Guid IdRegister { get; set; }
        
        public string? Nome { get; set; }

        public Genero Genero { get; set; }

        public abstract decimal CalculoCusto();

        public string NomeComRegistro()
        {
            return $"Nome: {Nome} - Registo: {IdRegister}";
        }
        
    }
}
