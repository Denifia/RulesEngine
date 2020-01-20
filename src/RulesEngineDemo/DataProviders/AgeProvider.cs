using RulesEngineDemo.Framework;
using System;

namespace RulesEngineDemo.DataProviders
{
    public interface IAgeProvider : IDataProvider<int>
    {
        int Age { get; }
    }

    public class AgeProvider : DataProvider<int>, IAgeProvider
    {
        public int Age { get; private set; }
        protected override void SetupInner(dynamic model, Func<dynamic, int> func) => 
            Age = func.Invoke(model);
    }
}
