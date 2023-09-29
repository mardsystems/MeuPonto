namespace MeuPonto;

public class TransactionContext
{
    public Guid UserName { get; }

    public DateTime DateTime { get; }

    public TransactionContext(Guid userName)
    {
        UserName = userName;

        DateTime = DateTime.Now;
    }
}
