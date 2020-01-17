using System;

namespace RulesEngineDemo.DataProviders
{
    public class IncomeProvider : IIncomeProvider
    {
        public int Income { get; private set; }
        public void Setup(dynamic model, Func<dynamic, int> func)
        {
            Income = func.Invoke(model);
        }
    }
}
