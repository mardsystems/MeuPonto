namespace MeuPonto;

public class TransactionContext
{
    public string UserName { get; }

    public DateTime DateTime { get; }

    public TransactionContext(string userName)
    {
        UserName = userName;

        DateTime = DateTime.Now;
    }
}
