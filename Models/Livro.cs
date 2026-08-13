namespace BibliotecaConsole.Models
{
    public class Livro : IEntidade
    {
        public int Id { get; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public bool Disponivel { get; set; } = true;

        public Livro(int id, string titulo, string autor)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
        }

        public override string ToString()
        {
            string status = Disponivel ? "Disponível" : "Emprestado";
            return $"[{Id}] \"{Titulo}\" - {Autor} ({status})";
        }
    }
}
