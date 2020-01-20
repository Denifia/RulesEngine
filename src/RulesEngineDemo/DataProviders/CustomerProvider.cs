using RulesEngineDemo.Framework;
using RulesEngineDemo.Models;
using System;

namespace RulesEngineDemo.DataProviders
{
    public interface ICustomerProvider : IDataProvider<Customer>
    {
        Customer Customer { get; }
    }

    public class CustomerProvider : DataProvider<Customer>, ICustomerProvider
    {
        public Customer Customer { get; private set; }
        protected override void SetupInner(dynamic model, Func<dynamic, Customer> func) =>
            Customer = func.Invoke(model);
    }
}
