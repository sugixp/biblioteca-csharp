namespace BibliotecaConsole.Interfaces
{
    // Contrato que qualquer repositório precisa seguir.
    // T precisa implementar IEntidade (ver Models/IEntidade.cs).
    public interface IRepositorio<T>
    {
        void Adicionar(T item);
        bool Remover(int id);
        T? BuscarPorId(int id);
        List<T> ListarTodos();
    }
}
