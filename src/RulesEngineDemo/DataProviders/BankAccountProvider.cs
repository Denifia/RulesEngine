using RulesEngineDemo.Models;
using System;

namespace RulesEngineDemo.DataProviders
{

    public class BankAccountProvider : IBankAccountProvider
    {
        public BankAccount BankAccount { get; private set; }
        public void Setup(dynamic model, Func<dynamic, BankAccount> bankAccountFunc)
        {
            BankAccount = bankAccountFunc.Invoke(model);
        }
    }
}
