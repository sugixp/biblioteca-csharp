namespace BibliotecaConsole.Models
{
    // Toda entidade que quiser ser guardada em um RepositorioEmMemoria<T>
    // precisa ter um Id. É isso que permite o repositório ser genérico.
    public interface IEntidade
    {
        int Id { get; }
    }
}
