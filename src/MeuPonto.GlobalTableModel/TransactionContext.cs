namespace MeuPonto;

public class TransactionContext
{
    public string UserId { get; }

    public DateTime DateTime { get; }

    public TransactionContext(string userId)
    {
        UserId = userId;

        DateTime = DateTime.Now;
    }
}
