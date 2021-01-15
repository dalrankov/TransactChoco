using HotChocolate.Resolvers;

namespace TransactChoco
{
    public interface ITransactionScopeFactory
    {
        System.Transactions.TransactionScope CreateScope(IMiddlewareContext context);
    }
}