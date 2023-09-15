namespace MeuPonto;

public class TransactionContext
{
    public Guid UserId { get; }

    public DateTime DateTime { get; }

    public TransactionContext(Guid userId)
    {
        UserId = userId;

        DateTime = DateTime.Now;
    }
}
