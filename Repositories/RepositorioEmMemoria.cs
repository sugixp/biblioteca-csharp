using BibliotecaConsole.Interfaces;
using BibliotecaConsole.Models;

namespace BibliotecaConsole.Repositories
{
    // Uma única implementação genérica serve para Livro, Membro e Emprestimo,
    // porque todos implementam IEntidade. É aqui que o "genérico" do C# brilha
    // comparado a reescrever a mesma lógica de lista em Python para cada entidade.
    public class RepositorioEmMemoria<T> : IRepositorio<T> where T : IEntidade
    {
        private readonly List<T> _itens = new();

        public void Adicionar(T item) => _itens.Add(item);

        public bool Remover(int id)
        {
            var item = BuscarPorId(id);
            if (item is null) return false;
            return _itens.Remove(item);
        }

        public T? BuscarPorId(int id) => _itens.FirstOrDefault(i => i.Id == id);

        public List<T> ListarTodos() => _itens;
    }
}
