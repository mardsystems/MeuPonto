namespace MeuPonto;

public class TransactionContext
{
    public Guid UserId { get; }

    public string UserName { get; }

    public DateTime DateTime { get; }

    public TransactionContext(Guid userId, string userName)
    {
        UserId = userId;

        UserName = userName;

        DateTime = DateTime.Now;
    }
}
