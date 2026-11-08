namespace Projeto360.Dominio.Entidades;

public class Tarefa
{
    // Identificador único da tarefa.
    public int Id { get; private set; }

    // Texto que descreve o que precisa ser feito.
    public string Titulo { get; private set; } = string.Empty;

    // Indica se a tarefa já foi finalizada.
    public bool Concluida { get; private set; }

    // Construtor utilizado pelo Entity Framework Core.
    private Tarefa() { }

    public Tarefa(string titulo)
    {
        AlterarTitulo(titulo);
        Concluida = false;
    }

    public void AlterarTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título da tarefa é obrigatório.");

        if (titulo.Trim().Length > 150)
            throw new ArgumentException("O título deve ter no máximo 150 caracteres.");

        Titulo = titulo.Trim();
    }

    public void MarcarComoConcluida()
    {
        Concluida = true;
    }

    public void Reabrir()
    {
        Concluida = false;
    }
}
