using System;

namespace RulesEngineDemo.DataProviders
{
    public interface IIncomeProvider
    {
        int Income { get; }

        void Setup(dynamic model, Func<dynamic, int> func);
    }
}
