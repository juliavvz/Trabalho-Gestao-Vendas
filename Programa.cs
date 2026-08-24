using System;

namespace GestaoVendas
{
    public class Venda
    {
        private int qtde;
        private double valor;

        public int Qtde { get { return qtde; } set { qtde = value; } }
        public double Valor { get { return valor; } set { valor = value; } }

        public Venda(int qtde = 0, double valor = 0)
        {
            this.qtde = qtde;
            this.valor = valor;
        }

        public double ValorMedio()
        {
            if (qtde == 0) return 0;
            return valor / qtde;
        }
    }

    public class Vendedor
    {
        private int id;
        private string nome;
        private double percComissao;
        private Venda[] asVendas;

        public int Id { get { return id; } set { id = value; } }
        public string Nome { get { return nome; } set { nome = value; } }
        public double PercComissao { get { return percComissao; } set { percComissao = value; } }
        public Venda[] AsVendas { get { return asVendas; } }

        public Vendedor(int id = 0, string nome = "", double percComissao = 0)
        {
            this.id = id;
            this.nome = nome;
            this.percComissao = percComissao;
            this.asVendas = new Venda[31];
            
            for (int i = 0; i < 31; i++)
            {
                asVendas[i] = new Venda();
            }
        }

        public void RegistrarVenda(int dia, Venda venda)
        {
            if (dia >= 1 && dia <= 31)
            {
                asVendas[dia - 1] = venda;
            }
        }

        public double ValorVendas()
        {
            double total = 0;
            foreach (Venda v in asVendas)
            {
                total += v.Valor;
            }
            return total;
        }

        public double ValorComissao()
        {
            return ValorVendas() * (percComissao / 100.0);
        }
    }

    public class Vendedores
    {
        private Vendedor[] osVendedores;
        private int max;
        private int qtde;

        public int Qtde { get { return qtde; } }
        public Vendedor[] OsVendedores { get { return osVendedores; } }

        public Vendedores(int max = 10)
        {
            this.max = max;
            this.osVendedores = new Vendedor[max];
            this.qtde = 0;
        }

        public bool AddVendedor(Vendedor v)
        {
            if (qtde >= max) return false;
            
            if (SearchVendedor(v) != null) return false;

            osVendedores[qtde] = v;
            qtde++;
            return true;
        }

        public bool DelVendedor(Vendedor v)
        {
            int posicao = -1;
            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Id == v.Id)
                {
                    posicao = i;
                    break;
                }
            }

            if (posicao == -1) return false;

            if (osVendedores[posicao].ValorVendas() > 0) return false;

            for (int i = posicao; i < qtde - 1; i++)
            {
                osVendedores[i] = osVendedores[i + 1];
            }
            osVendedores[qtde - 1] = null;
            qtde--;
            return true;
        }

        public Vendedor SearchVendedor(Vendedor v)
        {
            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Id == v.Id)
                    return osVendedores[i];
            }
            return null;
        }

        public double ValorVendas()
        {
            double total = 0;
            for (int i = 0; i < qtde; i++)
            {
                total += osVendedores[i].ValorVendas();
            }
            return total;
        }

        public double ValorComissao()
        {
            double total = 0;
            for (int i = 0; i < qtde; i++)
            {
                total += osVendedores[i].ValorComissao();
            }
            return total;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Vendedores empresa = new Vendedores(10);
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar vendedor");
                Console.WriteLine("2. Consultar vendedor");
                Console.WriteLine("3. Excluir vendedor");
                Console.WriteLine("4. Registrar venda");
                Console.WriteLine("5. Listar vendedores");
                Console.Write("Escolha uma opcao: ");
                
                if (!int.TryParse(Console.ReadLine(), out opcao)) continue;

                switch (opcao)
                {
                    case 1:
                        Console.Write("ID do vendedor: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();
                        Console.Write("Percentual de comissao (%): ");
                        double perc = double.Parse(Console.ReadLine());

                        Vendedor novoVendedor = new Vendedor(id, nome, perc);
                        if (empresa.AddVendedor(novoVendedor))
                            Console.WriteLine("Vendedor cadastrado com sucesso!");
                        else
                            Console.WriteLine("Erro! Limite atingido (10) ou ID já existe.");
                        break;

                    case 2:
                        Console.Write("ID do vendedor para consulta: ");
                        int idCons = int.Parse(Console.ReadLine());
                        Vendedor vCons = empresa.SearchVendedor(new Vendedor(idCons));
                        
                        if (vCons != null)
                        {
                            Console.WriteLine($"\nID: {vCons.Id} | Nome: {vCons.Nome}");
                            Console.WriteLine($"Total Vendas: R$ {vCons.ValorVendas():F2}");
                            Console.WriteLine($"Comissão: R$ {vCons.ValorComissao():F2}");
                            Console.WriteLine("Média de Vendas Diárias:");
                            
                            for (int i = 0; i < 31; i++)
                            {
                                if (vCons.AsVendas[i].Qtde > 0)
                                {
                                    Console.WriteLine($"- Dia {i + 1}: R$ {vCons.AsVendas[i].ValorMedio():F2}");
                                }
                            }
                        }
                        else Console.WriteLine("Vendedor não encontrado.");
                        break;

                    case 3:
                        Console.Write("ID do vendedor para exclusão: ");
                        int idExc = int.Parse(Console.ReadLine());
                        
                        if (empresa.DelVendedor(new Vendedor(idExc)))
                            Console.WriteLine("Vendedor excluído com sucesso!");
                        else
                            Console.WriteLine("Erro! Vendedor não existe ou possui vendas registradas.");
                        break;

                    case 4:
                        Console.Write("ID do vendedor: ");
                        int idVend = int.Parse(Console.ReadLine());
                        Vendedor vVenda = empresa.SearchVendedor(new Vendedor(idVend));
                        
                        if (vVenda != null)
                        {
                            Console.Write("Dia da venda (1 a 31): ");
                            int dia = int.Parse(Console.ReadLine());
                            Console.Write("Quantidade de itens: ");
                            int qtde = int.Parse(Console.ReadLine());
                            Console.Write("Valor total do dia: R$ ");
                            double valor = double.Parse(Console.ReadLine());

                            vVenda.RegistrarVenda(dia, new Venda(qtde, valor));
                            Console.WriteLine("Venda registrada!");
                        }
                        else Console.WriteLine("Vendedor não encontrado.");
                        break;

                    case 5:
                        Console.WriteLine("\n--- LISTA DE VENDEDORES ---");
                        for (int i = 0; i < empresa.Qtde; i++)
                        {
                            Vendedor v = empresa.OsVendedores[i];
                            Console.WriteLine($"ID: {v.Id} | Nome: {v.Nome} | Vendas: R$ {v.ValorVendas():F2} | Comissão: R$ {v.ValorComissao():F2}");
                        }
                        Console.WriteLine("---------------------------");
                        Console.WriteLine($"TOTAL DE VENDAS DA EMPRESA: R$ {empresa.ValorVendas():F2}");
                        Console.WriteLine($"TOTAL DE COMISSÕES A PAGAR: R$ {empresa.ValorComissao():F2}");
                        break;
                        
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                        
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
