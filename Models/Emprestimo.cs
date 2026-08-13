namespace BibliotecaConsole.Models
{
    public class Emprestimo : IEntidade
    {
        private const int PrazoDias = 14;
        private const decimal MultaPorDiaAtraso = 1.00m;

        public int Id { get; }
        public int LivroId { get; }
        public int MembroId { get; }
        public DateTime DataEmprestimo { get; }
        public DateTime DataDevolucaoPrevista { get; }
        public DateTime? DataDevolucaoReal { get; private set; }

        public bool Devolvido => DataDevolucaoReal.HasValue;

        public Emprestimo(int id, int livroId, int membroId, DateTime dataEmprestimo)
        {
            Id = id;
            LivroId = livroId;
            MembroId = membroId;
            DataEmprestimo = dataEmprestimo;
            DataDevolucaoPrevista = dataEmprestimo.AddDays(PrazoDias);
        }

        public decimal RegistrarDevolucao(DateTime dataDevolucao)
        {
            DataDevolucaoReal = dataDevolucao;
            return CalcularMulta(dataDevolucao);
        }

        public decimal CalcularMulta(DateTime dataReferencia)
        {
            if (dataReferencia <= DataDevolucaoPrevista)
                return 0m;

            int diasAtraso = (dataReferencia.Date - DataDevolucaoPrevista.Date).Days;
            return diasAtraso * MultaPorDiaAtraso;
        }

        public override string ToString()
        {
            string status = Devolvido
                ? $"devolvido em {DataDevolucaoReal:dd/MM/yyyy}"
                : $"previsto para {DataDevolucaoPrevista:dd/MM/yyyy}";
            return $"[{Id}] Livro {LivroId} -> Membro {MembroId} ({status})";
        }
    }
}
