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

string _menu = "1-Cadastro de funcionario." + Environment.NewLine +
              "2-Exibir os dados de todos os funcionários CLT." + Environment.NewLine +
              "3-Exibir os dados de todos os funcionários CNPJ." + Environment.NewLine +
              "4-Exibir a soma do custo total mensal de todos os funcionários." + Environment.NewLine +
              "5-Aumentar o salário de um funcionário CLT." + Environment.NewLine +
              "6-Aumentar o salário de um funcionário PJ." + Environment.NewLine +
              "7-Pesquisar um funcionário e exibir todos os seus dados." + Environment.NewLine +
              "8-Pesquisar um funcionário e exibir o custo total mensal dele para a empresa." + Environment.NewLine +
              "9-Custo do funcionario PJ com Hora extra." + Environment.NewLine +
              "10-Sair";

string _menuContrato = "1-CLT." + Environment.NewLine +
                       "2-CNPJ." + Environment.NewLine;


string _menuGenero = "1-Masculino." + Environment.NewLine +
                     "2-Feminino." + Environment.NewLine +
                     "3-Indefinido." + Environment.NewLine;


string _menuConfianca = "1-Sim." + Environment.NewLine +
                        "2-Não." + Environment.NewLine;

Console.WriteLine("Bem vindo ao sistema de Funcionarios!!" + Environment.NewLine);

Regex _regexLetras = new Regex("^[A-Za-z\\s]+$");
Regex _regexDecimal = new Regex("^(0*[1-9]\\d*(\\.\\d+)?|0*1(\\.0+)?)$");
Regex _regexCNPJ = new Regex("^\\d{14}$");
Regex _regexPercentual = new Regex("^(?:100|[1-9]\\d|\\d)$");
Regex _regexUUID = new Regex("^[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}$");
Regex _regexNatural = new Regex("^[1-9]\\d*$");

string _nome = "";
bool _confianca = false;
Genero _genero = Genero.Indefinido;
string _salario = "";
string _valorHora = "";
string _horasContratada = "";
string _CPNJ = "";

string _input = "";
string _id = "";

string _linha = Environment.NewLine + "------------------------------------------------------------" + Environment.NewLine;


FuncionarioCLT _Clt = new FuncionarioCLT();
FuncionarioCNPJ _Cnpj = new FuncionarioCNPJ();


bool _inputOK = true;

int _resposta = 0;

while (_inputOK)
{
    while (_inputOK)
    {
        try
        {
            Console.WriteLine(_menu);
            Console.WriteLine(_linha);

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
            Console.WriteLine("Input Invalido!" + Environment.NewLine);
        }
        catch (FormatException)
        {
            Console.WriteLine("Input Invalido!" + Environment.NewLine);

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
                    Console.WriteLine(_linha);

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
                                    _nome = InputPessoa(_menuGenero, _regexLetras, ref _genero, ref _inputOK, ref _resposta, _linha);

                                    InputRegex(_regexDecimal, ref _salario, _inputOK, "informe o Salario: ", _linha);

                                    _inputOK = true;

                                    while (_inputOK)
                                    {
                                        try
                                        {
                                            Console.WriteLine(_menuConfianca);
                                            Console.WriteLine(_linha);

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
                                            Console.WriteLine("Input Invalido!" + Environment.NewLine);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Input Invalido!" + Environment.NewLine);

                                        }

                                    }

                                    _inputOK = false;
                                }
                                catch (InvalidInputException)
                                {
                                    Console.WriteLine("Input Invalido!" + Environment.NewLine);
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Input Invalido!" + Environment.NewLine);

                                }
                            }
                            FuncionarioCLT funcionario = new FuncionarioCLT();

                            funcionario.Salario = decimal.Parse(_salario);
                            funcionario.Genero = _genero;
                            funcionario.Nome = _nome;
                            funcionario.CargoConfianca = _confianca;

                            _DaoClt.Save(funcionario);

                            Console.WriteLine("Funcionario cadastrado!" + Environment.NewLine);

                            LimpaConsole();

                            Console.WriteLine(_linha);
                            _inputOK = false;
                            break;//FuncionarioCLT

                        case 2://FuncionarioCNPJ

                            while (_inputOK)
                            {
                                try
                                {
                                    _nome = InputPessoa(_menuGenero, _regexLetras, ref _genero, ref _inputOK, ref _resposta, _linha);

                                    InputRegex(_regexDecimal, ref _valorHora, _inputOK, "Digite o valor Hora: ", _linha);

                                    _inputOK = true;

                                    InputRegex(_regexDecimal, ref _horasContratada, _inputOK, "Informe o total de horas contratadas: ", _linha);

                                    _inputOK = true;

                                    InputRegex(_regexCNPJ, ref _CPNJ, _inputOK, "Informe o CNPJ: ", _linha);

                                    _inputOK = false;
                                }
                                catch (InvalidInputException)
                                {
                                    Console.WriteLine("Input Invalido!" + Environment.NewLine);
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Input Invalido!" + Environment.NewLine);

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

                            Console.WriteLine("Funcionario cadastrado!" + Environment.NewLine);

                            Console.WriteLine(_linha);

                            LimpaConsole();

                            break;//FuncionarioCNPJ
                    }
                    _inputOK = false;
                }
                catch (InvalidInputException)
                {
                    Console.WriteLine("Input Invalido!" + Environment.NewLine);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input Invalido!" + Environment.NewLine);
                }

                _inputOK = true;
                break;
            }

            break;
        case 2:
            try
            {
                _DaoClt.ListAll();
                LimpaConsole();
            }
            catch (FuncionarioNotFoundException e)
            {
                Console.WriteLine(e.Message);
                LimpaConsole();
            }
            

           
            break;
        case 3:

           
            try
            {
                _DaoCnpj.ListAll();

                LimpaConsole();
            }
            catch (FuncionarioNotFoundException e)
            {
                Console.WriteLine(e.Message);
                LimpaConsole();
            }



            break;
        case 4:

            Console.WriteLine($"Total de funcionarios: {(_DaoClt.CountAll() + _DaoCnpj.CountAll())}");
            Console.WriteLine($"\nCusto Total: R${(_DaoClt.CalculoCustoTotal() + _DaoCnpj.CalculoCustoTotal()).ToString("F2")}\n");

            Console.WriteLine(_linha);

            LimpaConsole();
            break;

        case 5:

            try
            {
                _DaoClt.GetNameWithRegister();

                _input = "";
                _id = "";

                InputRegex(_regexUUID, ref _id, _inputOK, "Digite o id: ", _linha);

                _inputOK = true;

                InputRegex(_regexPercentual, ref _input, _inputOK, "Digite o percentual: ", _linha);

                _Clt = _DaoClt.GetById(Guid.Parse(_id));

                Console.WriteLine($"Salario Anterior: R${_Clt.Salario.ToString("F2")}");

                _Clt.AumentoSalario(_input);

                Console.WriteLine($"Salario Atual: R${_Clt.Salario.ToString("F2")}");

                Console.WriteLine(_linha);

                LimpaConsole();

            }
            catch (FuncionarioNotFoundException e)
            {
                Console.WriteLine(e.Message);
                LimpaConsole();
            }

            break;
        case 6:

            try
            {
                _input = "";
                _id = "";

                _DaoCnpj.GetNameWithRegister();

                InputRegex(_regexUUID, ref _id, _inputOK, "Digite o id: ", _linha);

                _inputOK = true;

                InputRegex(_regexDecimal, ref _input, _inputOK, "Digite o quanto vai ser adicionado no valor/hora: ", _linha);

                _Cnpj = _DaoCnpj.GetById(Guid.Parse(_id));

                Console.WriteLine($"Valor/hora Anterior: R${_Cnpj.ValorHora.ToString("F2")}");

                _Cnpj.AumentoValorHora(_input);

                Console.WriteLine($"Valor/hora Atual: R${_Cnpj.ValorHora.ToString("F2")}\n");

                Console.WriteLine(_linha);

                LimpaConsole();
            }
            catch (FuncionarioNotFoundException e)
            {
                Console.WriteLine(e.Message);
                LimpaConsole();
            }


            break;

        case 7:
            _id = "";
            try
            {
                InputRegex(_regexUUID, ref _id, _inputOK, "Digite o id: ", _linha);

                _inputOK = true;

                Console.WriteLine(_DaoClt.GetById(Guid.Parse(_id)).ToString());

                Console.WriteLine(_linha);

            }
            catch (FuncionarioNotFoundException)
            {
                try
                {
                    Console.WriteLine(_DaoCnpj.GetById(Guid.Parse(_id)).ToString());

                    Console.WriteLine(_linha);

                    LimpaConsole();

                }
                catch (FuncionarioNotFoundException e)
                {

                    Console.WriteLine(e.Message);

                    LimpaConsole();
                }
            }

            break;

        case 8:
            _id = "";
            try
            {
                InputRegex(_regexUUID, ref _id, _inputOK, "Digite o id: ", _linha);

                _inputOK = true;

                FuncionarioCLT _clt = _DaoClt.GetById(Guid.Parse(_id));

                Console.WriteLine($"\n{_clt.ToString()}\nCusto: R${_clt.CalculoCusto():0.00}");

                Console.WriteLine(_linha);

                LimpaConsole();

            }
            catch (FuncionarioNotFoundException)
            {
                try
                {
                    _Cnpj = _DaoCnpj.GetById(Guid.Parse(_id));
                    _ = _DaoCnpj.GetById(Guid.Parse(_id));

                    Console.WriteLine($"\n{_Cnpj.ToString()}\nCusto: R${_Cnpj.CalculoCusto():0.00}");

                    Console.WriteLine(_linha);

                    LimpaConsole();


                }
                catch (FuncionarioNotFoundException e)
                {
                    Console.WriteLine(e.Message);

                    Console.WriteLine(_linha);

                    LimpaConsole();


                }
            }

            break;

        case 9:
            try
            {
                _input = "";
                _id = "";

                _DaoCnpj.GetNameWithRegister();

                InputRegex(_regexUUID, ref _id, _inputOK, "Digite o id: ", _linha);

                _inputOK = true;

                InputRegex(_regexNatural, ref _input, _inputOK, "Digite o Total de horas extras: ", _linha);

                _Cnpj = _DaoCnpj.GetById(Guid.Parse(_id));

                Console.WriteLine($"Quantidade Hora Contratada: {_Cnpj.QuantHoraTrabalhada.ToString()}");
                Console.WriteLine($"Custo anterior: R${_Cnpj.CalculoCusto().ToString("F2")}\n");


                Console.WriteLine($"Quantidade Hora Trabalhada: {(_Cnpj.QuantHoraTrabalhada + int.Parse(_input)).ToString()}");
                Console.WriteLine($"Custo Atual: R${(_Cnpj.CalculoHoraExtra(_input)).ToString("F2")}\n");

                Console.WriteLine(_linha);

            }
            catch (FuncionarioNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            LimpaConsole();

            break;
        case 10:
            _inputOK = false;
            break;
    }
}

static string InputPessoa(string _menuGenero, Regex regexLetras, ref Genero _genero, ref bool _inputOK, ref int _resposta, string _linha)
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
            Console.WriteLine(_linha);
            Console.WriteLine(_menuGenero);


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
            Console.WriteLine("Input Invalido!" + Environment.NewLine);
        }
        catch (FormatException)
        {
            Console.WriteLine("Input Invalido!" + Environment.NewLine);

        }

    }
    _inputOK = true;
    return _nome;
}

static void InputRegex(Regex regex, ref string _input, bool _inputOK, string _placeholder, string _linha)
{
    while (_inputOK)
    {
        try
        {
            Console.WriteLine(_linha);

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
            Console.WriteLine("Input Invalido!" + Environment.NewLine);
        }
        catch (FormatException)
        {
            Console.WriteLine("Input Invalido!" + Environment.NewLine);

        }

    }
}

static void LimpaConsole()
{
    Console.WriteLine("Pressione Enter para prosseguir...");
    Console.ReadLine();
    Console.Clear();
}