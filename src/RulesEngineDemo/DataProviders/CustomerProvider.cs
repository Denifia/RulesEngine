using RulesEngineDemo.Models;
using System;

namespace RulesEngineDemo.DataProviders
{
    public class CustomerProvider : ICustomerProvider
    {
        public Customer Customer { get; private set; }
        public void Setup(dynamic model, Func<dynamic, Customer> customerFunc)
        {
            Customer = customerFunc.Invoke(model);
        }
    }
}
