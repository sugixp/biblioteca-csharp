namespace BibliotecaConsole.Models
{
    public class Membro : IEntidade
    {
        public int Id { get; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public Membro(int id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }

        public override string ToString()
        {
            return $"[{Id}] {Nome} ({Email})";
        }
    }
}
