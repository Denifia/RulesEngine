using RulesEngineDemo.Framework;
using RulesEngineDemo.Models;
using System;

namespace RulesEngineDemo.DataProviders
{
    public interface IBankAccountProvider : IDataProvider<BankAccount>
    {
        BankAccount BankAccount { get; }
    }

    public class BankAccountProvider : DataProvider<BankAccount>, IBankAccountProvider
    {
        public BankAccount BankAccount { get; private set; }
        protected override void SetupInner(dynamic model, Func<dynamic, BankAccount> func) => 
            BankAccount = func.Invoke(model);
    }
}
