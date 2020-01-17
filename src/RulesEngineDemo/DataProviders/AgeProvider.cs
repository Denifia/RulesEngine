using System;

namespace RulesEngineDemo.DataProviders
{
    public class AgeProvider : IAgeProvider
    {
        public int Age { get; private set; }
        public void Setup(dynamic model, Func<dynamic, int> ageFunc)
        {
            Age = ageFunc.Invoke(model);
        }
    }
}
