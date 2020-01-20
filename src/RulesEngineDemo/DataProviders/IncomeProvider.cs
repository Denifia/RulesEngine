using RulesEngineDemo.Framework;
using System;

namespace RulesEngineDemo.DataProviders
{
    public interface IIncomeProvider : IDataProvider<int>
    {
        int Income { get; }
    }

    public class IncomeProvider : DataProvider<int>, IIncomeProvider
    {
        public int Income { get; private set; }
        protected override void SetupInner(dynamic model, Func<dynamic, int> func) =>
            Income = func.Invoke(model);
    }
}
