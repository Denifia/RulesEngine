using System;

namespace RulesEngineDemo.DataProviders
{
    public interface IAgeProvider
    {
        int Age { get; }

        void Setup(dynamic model, Func<dynamic, int> ageFunc);
    }
}
