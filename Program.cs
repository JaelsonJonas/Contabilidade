using ConsoleApp.dao;
using ConsoleApp.exception;
using ConsoleApp.model;
using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Channels;


IList<FuncionarioCLT> listaCLT = new List<FuncionarioCLT>();
IList<FuncionarioCNPJ> listaCNPJ = new List<FuncionarioCNPJ>();

var _DaoClt = new FuncionarioCLTDao(listaCLT);
var _DaoCnpj = new FuncionarioCNPJDao(listaCNPJ);

string _menu = "1-Cadastro de funcionario.\n2-Exibir os dados de todos os funcionários CLT.\n3-Exibir os dados de todos os funcionários CNPJ.\n4-Exibir a soma do custo total mensal de todos os funcionários.\n5-Aumentar o salário de um funcionário CLT.\n6-Aumentar o salário de um funcionário PJ.\n7-Pesquisar um funcionário e exibir todos os seus dados.\n8-Pesquisar um funcionário e exibir o custo total mensal dele para a empresa.\n9-Custo do funcionario PJ com Hora extra.\n10-Sair";

string _menuContrato = "1-CLT.\n2-CNPJ.";

string _menuGenero = "1-Masculino.\n2-Feminino.\n3-Indefinido.";

string _menuConfianca = "1-Sim.\n2-Não.\n";


Console.WriteLine("Bem vindo ao sistema de Funcionarios!!\n");

Regex _regexLetras = new Regex("^[A-Za-z]+$");
Regex _regexDecimal = new Regex("^\\d+(\\.\\d+)?$");
Regex _regexCNPJ = new Regex("^\\d{14}$");
Regex _regexPercentual = new Regex("^(?:100|[1-9]\\d|\\d)$");
Regex _regexUUID = new Regex("^[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}$");
Regex _regexNatural = new Regex("^\\d+$");

string _nome = "";
bool _confianca = false;
Genero _genero = Genero.Indefinido;
string _salario = "";
string _valorHora = "";
string _horasContratada = "";
string _CPNJ = "";

string _input = "";
string _id = "";

FuncionarioCLT _Clt = null;
FuncionarioCNPJ _Cnpj = null;



bool _inputOK = true;

int _resposta = 0;

while (_inputOK)
{
    while (_inputOK)
    {
        try
        {
            Console.WriteLine(_menu);
            Console.WriteLine("\r\n------------------------------------------------------------\n");

            Console.Write("Selecione uma opção abaixo: ");

            _resposta = int.Parse(Console.ReadLine() ?? "");

            if (_resposta <= 0 || _resposta > 10)
            {
                throw new InvalidInputException();
            }
            _inputOK = false;
        }
        catch (InvalidInputException)
        {
            Console.WriteLine("Input Invalido!\n");
        }
        catch (FormatException)
        {
            Console.WriteLine("Input Invalido!\n");

        }
    }
    _inputOK = true;
    switch (_resposta)
    {
        case 1:

            while (_inputOK)
            {
                try
                {
                    Console.WriteLine(_menuContrato);
                    Console.WriteLine("\n");

                    Console.Write("Selecione uma opção abaixo: ");

                    _resposta = int.Parse(Console.ReadLine() ?? "");

                    if (_resposta <= 0 || _resposta > 2)
                    {
                        throw new InvalidInputException();
                    }

                    switch (_resposta)
                    {
                        case 1://FuncionarioCLT

                            while (_inputOK)
                            {
                                try
                                {
                                    _nome = InputPessoa(_menuGenero, _regexLetras, ref _genero, ref _inputOK, ref _resposta);

                                    InputRegex(_regexDecimal, ref _salario, _inputOK, "informe o Salario: ");

                                    _inputOK = true;

                                    while (_inputOK)
                                    {
                                        try
                                        {
                                            Console.WriteLine(_menuConfianca);
                                            Console.WriteLine("\n");

                                            Console.Write("Possui cargo de confiança? ");
                                            _resposta = int.Parse(Console.ReadLine() ?? "");

                                            if (_resposta <= 0 || _resposta > 2)
                                            {
                                                throw new InvalidInputException();
                                            }
                                            _confianca = false;
                                            switch (_resposta)
                                            {
                                                case 1:
                                                    _confianca = true;
                                                    break;
                                                case 2:
                                                    _confianca = false;
                                                    break;
                                            }
                                            _inputOK = false;

                                        }
                                        catch (InvalidInputException)
                                        {
                                            Console.WriteLine("Input Invalido! \n");
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Input Invalido! \n");

                                        }

                                    }

                                    _inputOK = false;
                                }
                                catch (InvalidInputException)
                                {
                                    Console.WriteLine("Input Invalido! \n");
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Input Invalido! \n");

                                }
                            }
                            FuncionarioCLT funcionario = new FuncionarioCLT();

                            funcionario.Salario = decimal.Parse(_salario);
                            funcionario.Genero = _genero;
                            funcionario.Nome = _nome;
                            funcionario.CargoConfianca = _confianca;

                            _DaoClt.Save(funcionario);

                            Console.WriteLine("Funcionario cadastrado!!\n");
                            _inputOK = false;
                            break;//FuncionarioCLT

                        case 2://FuncionarioCNPJ

                            while (_inputOK)
                            {
                                try
                                {
                                    _nome = InputPessoa(_menuGenero, _regexLetras, ref _genero, ref _inputOK, ref _resposta);

                                    InputRegex(_regexDecimal, ref _valorHora, _inputOK, "Digite o valor Hora: ");

                                    _inputOK = true;

                                    InputRegex(_regexDecimal, ref _horasContratada, _inputOK, "Informe o total de horas contratadas: ");

                                    _inputOK = true;

                                    InputRegex(_regexCNPJ, ref _CPNJ, _inputOK, "Informe o CNPJ: ");

                                    _inputOK = false;
                                }
                                catch (InvalidInputException)
                                {
                                    Console.WriteLine("Input Invalido! \n");
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Input Invalido! \n");

                                }
                            }

                            FuncionarioCNPJ funcionarioCNPJ = new FuncionarioCNPJ();

                            funcionarioCNPJ.Nome = _nome;
                            funcionarioCNPJ.Genero = _genero;
                            funcionarioCNPJ.ValorHora = decimal.Parse(_valorHora);
                            funcionarioCNPJ.QuantHoraTrabalhada = int.Parse(_horasContratada);
                            funcionarioCNPJ.CNPJ = _CPNJ;

                            _DaoCnpj.Save(funcionarioCNPJ);

                            _inputOK = false;

                            Console.WriteLine("Funcionario cadastrado!!\n");

                            break;//FuncionarioCNPJ
                    }
                    _inputOK = false;
                }
                catch (InvalidInputException)
                {
                    Console.WriteLine("Input Invalido! \n");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input Invalido!\n");
                }


                _inputOK = true;
                break;
            }

            break;
        case 2:

            _DaoClt.ListAll();

            break;
        case 3:

            _DaoCnpj.ListAll();

            break;
        case 4:

            Console.WriteLine($"Total de funcionarios: {(_DaoClt.CountAll() + _DaoCnpj.CountAll())}");
            Console.WriteLine($"\nCusto Total: R${(_DaoClt.CalculoCustoTotal() + _DaoCnpj.CalculoCustoTotal()).ToString("F2")}\n");
            break;

        case 5:
            _input = "";
            _id = "";

            InputRegex(_regexPercentual, ref _input, _inputOK, "Digite o percentual: ");

            _inputOK = true;

            InputRegex(_regexUUID, ref _id, _inputOK, "Digite o id: ");

            _Clt = _DaoClt.GetById(Guid.Parse(_id));

            Console.WriteLine($"Salario Anterior: R${_Clt.Salario.ToString("F2")}");

            _Clt.AumentoSalario(_input);

            Console.WriteLine($"Salario Atual: R${_Clt.Salario.ToString("F2")}");

            break;
        case 6:

            _input = "";
            _id = "";

            InputRegex(_regexDecimal, ref _input, _inputOK, "Digite o quanto vai ser adicionado no valor/hora: ");

            _inputOK = true;

            InputRegex(_regexUUID, ref _id, _inputOK, "Digite o id: ");

            _Cnpj = _DaoCnpj.GetById(Guid.Parse(_id));

            Console.WriteLine($"Valor/hora Anterior: R${_Cnpj.ValorHora.ToString("F2")}");

            _Cnpj.AumentoValorHora(_input);

            Console.WriteLine($"Valor/hora Atual: R${_Cnpj.ValorHora.ToString("F2")}\n");

            break;

        case 7:
            _id = "";
            try
            {

                InputRegex(_regexUUID, ref _id, _inputOK, "Digite o id: ");

                _inputOK = true;

                Console.WriteLine(_DaoClt.GetById(Guid.Parse(_id)).ToString());

            }
            catch (FuncionarioNotFoundException)
            {
                try
                {
                    Console.WriteLine(_DaoCnpj.GetById(Guid.Parse(_id)).ToString());

                }
                catch (FuncionarioNotFoundException e)
                {

                    Console.WriteLine(e.Message + "\n");
                }
            }

            break;

        case 8:
            _id = "";
            try
            {

                InputRegex(_regexUUID, ref _id, _inputOK, "Digite o id: ");

                _inputOK = true;

                FuncionarioCLT _clt = _DaoClt.GetById(Guid.Parse(_id));

                Console.WriteLine($"\n{_clt.ToString()}\nCusto: R${_clt.CalculoCusto():0.00}");

            }
            catch (FuncionarioNotFoundException)
            {
                try
                {
                    _Cnpj = _DaoCnpj.GetById(Guid.Parse(_id));
                    _ = _DaoCnpj.GetById(Guid.Parse(_id));

                    Console.WriteLine($"\n{_Cnpj.ToString()}\nCusto: R${_Cnpj.CalculoCusto():0.00}");

                }
                catch (FuncionarioNotFoundException e)
                {

                    Console.WriteLine(e.Message + "\n");
                }
            }

            break;

        case 9:

            _input = "";
            _id = "";

            InputRegex(_regexNatural, ref _input, _inputOK, "Digite o Total de horas extras: ");

            _inputOK = true;

            InputRegex(_regexUUID, ref _id, _inputOK, "Digite o id: ");

            _Cnpj = _DaoCnpj.GetById(Guid.Parse(_id));

            Console.WriteLine($"Quantidade Hora Contratada: {_Cnpj.QuantHoraTrabalhada.ToString()}");
            Console.WriteLine($"Custo anterior: R${_Cnpj.CalculoCusto().ToString("F2")}\n");


            Console.WriteLine($"Quantidade Hora Trabalhada: {(_Cnpj.QuantHoraTrabalhada + int.Parse(_input)).ToString()}");
            Console.WriteLine($"Custo Atual: R${(_Cnpj.CalculoHoraExtra(_input)).ToString("F2")}\n");


            break;
        case 10:
            _inputOK = false;
            break;
    }
}

static string InputPessoa(string _menuGenero, Regex regexLetras, ref Genero _genero, ref bool _inputOK, ref int _resposta)
{
    string _nome;
    Console.Write("Digite o nome do funcionario: ");
    _nome = Console.ReadLine() ?? "";

    if (!regexLetras.IsMatch(_nome))
    {
        throw new InvalidInputException();
    }

    while (_inputOK)
    {
        try
        {
            Console.WriteLine(_menuGenero);
            Console.WriteLine("\n");

            Console.Write("Informe o genero: ");
            _resposta = int.Parse(Console.ReadLine() ?? "");

            if (_resposta <= 0 || _resposta > 3)
            {
                throw new InvalidInputException();
            }
            _genero = Genero.Indefinido;
            switch (_resposta)
            {
                case 1:
                    _genero = Genero.Masculino;
                    break;
                case 2:
                    _genero = Genero.Feminino;
                    break;
                case 3:
                    _genero = Genero.Indefinido;
                    break;

            }
            _inputOK = false;

        }
        catch (InvalidInputException)
        {
            Console.WriteLine("Input Invalido! \n");
        }
        catch (FormatException)
        {
            Console.WriteLine("Input Invalido! \n");

        }

    }
    _inputOK = true;
    return _nome;
}

static void InputRegex(Regex regex, ref string _input, bool _inputOK, string _placeholder)
{
    while (_inputOK)
    {
        try
        {
            Console.WriteLine("\n");

            Console.Write(_placeholder);
            _input = Console.ReadLine() ?? "";

            if (!regex.IsMatch(_input) || _input == ".")
            {
                throw new InvalidInputException();
            }

            _inputOK = false;

        }
        catch (InvalidInputException)
        {
            Console.WriteLine("Input Invalido! \n");
        }
        catch (FormatException)
        {
            Console.WriteLine("Input Invalido! \n");

        }

    }
}