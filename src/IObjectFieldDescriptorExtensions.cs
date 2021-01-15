using HotChocolate.Types;
using System;

namespace TransactChoco
{
    public static class IObjectFieldDescriptorExtensions
    {
        /// <summary>
        /// Adds TransactionScope field middleware.
        /// </summary>
        public static IObjectFieldDescriptor UseTransactionScope(
            this IObjectFieldDescriptor descriptor)
        {
            return descriptor.UseTransactionScope<TransactionScopeFactory>();
        }

        /// <summary>
        /// Adds TransactionScope field middleware.
        /// </summary>
        /// <typeparam name="TFactory">Custom TransactionScope factory class implementing <see cref="ITransactionScopeFactory"/>.</typeparam>
        public static IObjectFieldDescriptor UseTransactionScope<TFactory>(
            this IObjectFieldDescriptor descriptor) where TFactory : ITransactionScopeFactory
        {
            return descriptor.UseTransactionScope(typeof(TFactory));
        }

        /// <summary>
        /// Adds TransactionScope field middleware.
        /// </summary>
        /// <param name="factoryType">Custom TransactionScope factory class type implementing <see cref="ITransactionScopeFactory"/>.</param>
        public static IObjectFieldDescriptor UseTransactionScope(
            this IObjectFieldDescriptor descriptor,
            Type factoryType)
        {
            if (!(typeof(ITransactionScopeFactory).IsAssignableFrom(factoryType) && factoryType.IsClass))
            {
                throw new ArgumentException($"{factoryType.Name} is not a class implementing {nameof(ITransactionScopeFactory)}!");
            }

            return descriptor.Use((provider, next) =>
                new TransactionScopeMiddleware(next, factoryType));
        }
    }
}