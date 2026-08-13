# Sistema de Biblioteca (C# Console)

Projeto de portfólio para praticar fundamentos de C# e orientação a objetos:
classes, interfaces, generics e injeção de dependência manual.

## Conceitos praticados

- **Encapsulamento**: propriedades com `get`/`set` privados quando faz sentido
  (ex: `Emprestimo.DataDevolucaoReal` só muda via `RegistrarDevolucao`).
- **Interfaces**: `IEntidade` e `IRepositorio<T>` definem contratos que as
  classes concretas seguem.
- **Generics**: `RepositorioEmMemoria<T>` é uma única implementação reutilizada
  para `Livro`, `Membro` e `Emprestimo` — em vez de repetir a mesma lógica de
  lista três vezes.
- **Separação em camadas**: `Models` (entidades) → `Repositories` (acesso a
  dados) → `Services` (regras de negócio) → `Program.cs` (interface/console).
- **Tuplas nomeadas** como retorno (`(bool sucesso, string mensagem, ...)`)
  para sinalizar resultado de operações sem lançar exceções para fluxo normal.

## Regras de negócio

- Um membro pode ter no máximo **3 empréstimos ativos** simultâneos.
- Prazo de devolução: **14 dias**.
- Multa por atraso: **R$ 1,00 por dia**.

## Como rodar

```bash
cd BibliotecaConsole
dotnet run
```

Requer o [.NET SDK 10](https://dotnet.microsoft.com/download) instalado.

## Próximos passos possíveis

- Trocar `RepositorioEmMemoria<T>` por uma implementação com SQLite
  (via `Microsoft.Data.Sqlite` ou Entity Framework Core), sem alterar
  `BibliotecaService` nem `Program.cs` —
- Adicionar testes unitários com xUnit para `BibliotecaService`.
