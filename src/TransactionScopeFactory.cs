using HotChocolate.Resolvers;
using System;
using System.Transactions;

namespace TransactChoco
{
    class TransactionScopeFactory
        : ITransactionScopeFactory
    {
        static readonly TransactionOptions _transactionOptions =
            new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(1)
            };

        public System.Transactions.TransactionScope CreateScope(
            IMiddlewareContext context)
        {
            return new System.Transactions.TransactionScope(
                TransactionScopeOption.Required,
                _transactionOptions,
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}