using ConsoleApp.repositores;
using ConsoleApp.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.model
{
    internal class FuncionarioCNPJ : Pessoa
    {
        public decimal ValorHora { get; set; }
        public decimal QuantHoraTrabalhada { get; set; }

        public string? CNPJ { get; set; }

        public decimal CalculoHoraExtra(string horasExtras)
        {
            return ValorHora * (QuantHoraTrabalhada + int.Parse(horasExtras));
        }

        public void AumentoValorHora(string valor)
        {
            ValorHora += decimal.Parse(valor);
        }

        public override decimal CalculoCusto()
        {
            return ValorHora * QuantHoraTrabalhada;
        }

        public override string ToString()
        {
            return $"Registro: {IdRegister}\n" +
          $"Nome: {Nome}\n" +
          $"Gênero: {Genero}\n" +
          $"Valor Hora: {ValorHora}\n" +
          $"Quantidade Hora Trabalhada: {QuantHoraTrabalhada}\n" +
          $"CNPJ: {CNPJ}\n";
        }

    }
}
