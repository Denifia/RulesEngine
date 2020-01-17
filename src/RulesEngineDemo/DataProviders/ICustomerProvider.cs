using RulesEngineDemo.Models;
using System;

namespace RulesEngineDemo.DataProviders
{
    public interface ICustomerProvider
    {
        Customer Customer { get; }

        void Setup(dynamic model, Func<dynamic, Customer> customerFunc);
    }
}
