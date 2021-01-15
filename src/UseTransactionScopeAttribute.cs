using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;
using System.Reflection;

namespace TransactChoco
{
    /// <summary>
    /// Adds TransactionScope field middleware.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class UseTransactionScopeAttribute
        : ObjectFieldDescriptorAttribute
    {
        readonly Type _factoryType;

        public UseTransactionScopeAttribute()
        {
        }

        /// <param name="factoryType">Custom TransactionScope factory class type implementing <see cref="ITransactionScopeFactory"/>.</param>
        public UseTransactionScopeAttribute(
           Type factoryType)
        {
            if (!(typeof(ITransactionScopeFactory).IsAssignableFrom(factoryType) && factoryType.IsClass))
            {
                throw new ArgumentException($"{factoryType.Name} is not a class implementing {nameof(ITransactionScopeFactory)}!");
            }

            _factoryType = factoryType;
        }

        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            if (_factoryType != null)
            {
                descriptor.UseTransactionScope(_factoryType);
            }
            else
            {
                descriptor.UseTransactionScope();
            }
        }
    }
}