namespace MeuPonto;

public class TransactionContext
{
    public string UserId { get; }

    public DateTime DateTime { get; }

    public TransactionContext(string userName)
    {
        UserId = userName;

        DateTime = DateTime.Now;
    }
}
