using ConsoleApp.repositores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.model
{
    internal class FuncionarioCLT : Pessoa
    {
        public decimal Salario { get; set; }

        public bool CargoConfianca {get; set; }

        public void AumentoSalario(string percentual)
        {
            Salario *=  1 + (Convert.ToDecimal(percentual) / 100);
        }

        public override decimal CalculoCusto()
        {
            return Salario * 1.01111m * 1.0833m * 1.08m * 1.04m * 1.0793m;
        }

        public override string ToString()
        {
            return $"Registro: {IdRegister}\n" +
              $"Nome: {Nome}\n" +
              $"Gênero: {Genero}\n" +
              $"Salário: {Salario}\n" +
              $"Possui Cargo de Confiança: {(CargoConfianca ? "Sim" : "Não")}\n";
        }



    }
}
