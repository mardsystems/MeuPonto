namespace MeuPonto;

public class TransactionContext
{
    public Guid Id { get; }

    public string UserName { get; }

    public DateTime DateTime { get; }

    public TransactionContext(string userName)
    {
        Id = Guid.NewGuid();

        UserName = userName;

        DateTime = DateTime.Now;
    }
}
