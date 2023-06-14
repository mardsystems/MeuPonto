namespace System.Transactions;

public interface IUnitOfWork
{
    void BeginTransaction();

    void Commit();

    void Rollback();
}
