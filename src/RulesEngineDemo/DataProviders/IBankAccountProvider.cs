using RulesEngineDemo.Models;
using System;

namespace RulesEngineDemo.DataProviders
{
    public interface IBankAccountProvider
    {
        BankAccount BankAccount { get; }

        void Setup(dynamic model, Func<dynamic, BankAccount> bankAccountFunc);
    }
}
