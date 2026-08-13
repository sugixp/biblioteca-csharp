using BibliotecaConsole.Interfaces;
using BibliotecaConsole.Models;

namespace BibliotecaConsole.Services
{
    public class BibliotecaService
    {
        private const int LimiteEmprestimosPorMembro = 3;

        private readonly IRepositorio<Livro> _livros;
        private readonly IRepositorio<Membro> _membros;
        private readonly IRepositorio<Emprestimo> _emprestimos;

        private int _proximoIdLivro = 1;
        private int _proximoIdMembro = 1;
        private int _proximoIdEmprestimo = 1;

        public BibliotecaService(
            IRepositorio<Livro> livros,
            IRepositorio<Membro> membros,
            IRepositorio<Emprestimo> emprestimos)
        {
            _livros = livros;
            _membros = membros;
            _emprestimos = emprestimos;
        }

        public Livro CadastrarLivro(string titulo, string autor)
        {
            var livro = new Livro(_proximoIdLivro++, titulo, autor);
            _livros.Adicionar(livro);
            return livro;
        }

        public Membro CadastrarMembro(string nome, string email)
        {
            var membro = new Membro(_proximoIdMembro++, nome, email);
            _membros.Adicionar(membro);
            return membro;
        }

        public List<Livro> ListarLivros() => _livros.ListarTodos();

        public List<Membro> ListarMembros() => _membros.ListarTodos();

        public List<Emprestimo> ListarEmprestimos() => _emprestimos.ListarTodos();

        public (bool sucesso, string mensagem, Emprestimo? emprestimo) EmprestarLivro(int livroId, int membroId)
        {
            var livro = _livros.BuscarPorId(livroId);
            if (livro is null)
                return (false, "Livro não encontrado.", null);

            var membro = _membros.BuscarPorId(membroId);
            if (membro is null)
                return (false, "Membro não encontrado.", null);

            if (!livro.Disponivel)
                return (false, "Este livro já está emprestado.", null);

            int emprestimosAtivos = _emprestimos
                .ListarTodos()
                .Count(e => e.MembroId == membroId && !e.Devolvido);

            if (emprestimosAtivos >= LimiteEmprestimosPorMembro)
                return (false, $"Membro já atingiu o limite de {LimiteEmprestimosPorMembro} empréstimos ativos.", null);

            var emprestimo = new Emprestimo(_proximoIdEmprestimo++, livroId, membroId, DateTime.Now);
            _emprestimos.Adicionar(emprestimo);
            livro.Disponivel = false;

            return (true, "Empréstimo realizado com sucesso.", emprestimo);
        }

        public (bool sucesso, string mensagem, decimal multa) DevolverLivro(int emprestimoId)
        {
            var emprestimo = _emprestimos.BuscarPorId(emprestimoId);
            if (emprestimo is null)
                return (false, "Empréstimo não encontrado.", 0m);

            if (emprestimo.Devolvido)
                return (false, "Este empréstimo já foi devolvido.", 0m);

            decimal multa = emprestimo.RegistrarDevolucao(DateTime.Now);

            var livro = _livros.BuscarPorId(emprestimo.LivroId);
            if (livro is not null)
                livro.Disponivel = true;

            string mensagem = multa > 0
                ? $"Devolução registrada com atraso. Multa: R$ {multa:F2}"
                : "Devolução registrada dentro do prazo.";

            return (true, mensagem, multa);
        }
    }
}
