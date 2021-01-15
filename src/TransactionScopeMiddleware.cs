using HotChocolate.Resolvers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TransactChoco
{
    class TransactionScopeMiddleware
    {
        readonly FieldDelegate _next;
        readonly Type _factoryType;

        public TransactionScopeMiddleware(
            FieldDelegate next,
            Type factoryType)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _factoryType = factoryType ?? throw new ArgumentNullException(nameof(factoryType));
        }

        public async Task Invoke(
            IMiddlewareContext context)
        {
            var factory = ActivatorUtilities.CreateInstance(
                context.Service<IServiceProvider>(), _factoryType) as ITransactionScopeFactory;

            using System.Transactions.TransactionScope scope = factory.CreateScope(context);

            await _next(context).ConfigureAwait(false);

            scope.Complete();
        }
    }
}