using BibliotecaConsole.Interfaces;
using BibliotecaConsole.Models;
using BibliotecaConsole.Repositories;
using BibliotecaConsole.Services;

// Injeção manual de dependência: o Program monta as peças e entrega pro Service.
// Isso facilita trocar RepositorioEmMemoria por um RepositorioSqlite depois,
// sem mexer em nenhuma regra de negócio.
IRepositorio<Livro> repositorioLivros = new RepositorioEmMemoria<Livro>();
IRepositorio<Membro> repositorioMembros = new RepositorioEmMemoria<Membro>();
IRepositorio<Emprestimo> repositorioEmprestimos = new RepositorioEmMemoria<Emprestimo>();

var service = new BibliotecaService(repositorioLivros, repositorioMembros, repositorioEmprestimos);

SeedDados(service);

bool executando = true;
while (executando)
{
    MostrarMenu();
    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CadastrarLivro(service);
            break;
        case "2":
            CadastrarMembro(service);
            break;
        case "3":
            ListarLivros(service);
            break;
        case "4":
            ListarMembros(service);
            break;
        case "5":
            EmprestarLivro(service);
            break;
        case "6":
            DevolverLivro(service);
            break;
        case "7":
            ListarEmprestimos(service);
            break;
        case "0":
            executando = false;
            break;
        default:
            Console.WriteLine("Opção inválida.\n");
            break;
    }
}

Console.WriteLine("Até mais!");

// ---- Funções auxiliares de UI (mantidas no Program.cs para simplicidade) ----

static void MostrarMenu()
{
    Console.WriteLine("===== Sistema de Biblioteca =====");
    Console.WriteLine("1. Cadastrar livro");
    Console.WriteLine("2. Cadastrar membro");
    Console.WriteLine("3. Listar livros");
    Console.WriteLine("4. Listar membros");
    Console.WriteLine("5. Emprestar livro");
    Console.WriteLine("6. Devolver livro");
    Console.WriteLine("7. Listar empréstimos");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");
}

static void CadastrarLivro(BibliotecaService service)
{
    Console.Write("Título: ");
    string titulo = Console.ReadLine() ?? "";
    Console.Write("Autor: ");
    string autor = Console.ReadLine() ?? "";

    var livro = service.CadastrarLivro(titulo, autor);
    Console.WriteLine($"Livro cadastrado: {livro}\n");
}

static void CadastrarMembro(BibliotecaService service)
{
    Console.Write("Nome: ");
    string nome = Console.ReadLine() ?? "";
    Console.Write("Email: ");
    string email = Console.ReadLine() ?? "";

    var membro = service.CadastrarMembro(nome, email);
    Console.WriteLine($"Membro cadastrado: {membro}\n");
}

static void ListarLivros(BibliotecaService service)
{
    var livros = service.ListarLivros();
    if (livros.Count == 0)
    {
        Console.WriteLine("Nenhum livro cadastrado.\n");
        return;
    }

    foreach (var livro in livros)
        Console.WriteLine(livro);
    Console.WriteLine();
}

static void ListarMembros(BibliotecaService service)
{
    var membros = service.ListarMembros();
    if (membros.Count == 0)
    {
        Console.WriteLine("Nenhum membro cadastrado.\n");
        return;
    }

    foreach (var membro in membros)
        Console.WriteLine(membro);
    Console.WriteLine();
}

static void EmprestarLivro(BibliotecaService service)
{
    Console.Write("ID do livro: ");
    int livroId = LerInt();
    Console.Write("ID do membro: ");
    int membroId = LerInt();

    var (sucesso, mensagem, _) = service.EmprestarLivro(livroId, membroId);
    Console.WriteLine(mensagem + "\n");
}

static void DevolverLivro(BibliotecaService service)
{
    Console.Write("ID do empréstimo: ");
    int emprestimoId = LerInt();

    var (sucesso, mensagem, _) = service.DevolverLivro(emprestimoId);
    Console.WriteLine(mensagem + "\n");
}

static void ListarEmprestimos(BibliotecaService service)
{
    var emprestimos = service.ListarEmprestimos();
    if (emprestimos.Count == 0)
    {
        Console.WriteLine("Nenhum empréstimo registrado.\n");
        return;
    }

    foreach (var emprestimo in emprestimos)
        Console.WriteLine(emprestimo);
    Console.WriteLine();
}

static int LerInt()
{
    int.TryParse(Console.ReadLine(), out int valor);
    return valor;
}

static void SeedDados(BibliotecaService service)
{
    // Alguns dados iniciais só pra você não começar do zero toda vez que testar.
    service.CadastrarLivro("Clean Code", "Robert C. Martin");
    service.CadastrarLivro("O Programador Pragmático", "David Thomas");
    service.CadastrarMembro("Henrique Sugi", "henrique@example.com");
}
