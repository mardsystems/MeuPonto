namespace System.Transactions;

public class TransactionScopeManager : IUnitOfWork
{
    private TransactionScope scope;

    public void BeginTransaction()
    {
        scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        //scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 2, 0));
    }

    public void Commit()
    {
        scope.Complete();

        scope.Dispose();

        scope = null;
    }

    public void Rollback()
    {
        scope.Dispose();

        scope = null;
    }
}
