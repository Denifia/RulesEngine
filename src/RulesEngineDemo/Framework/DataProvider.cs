using System;

namespace RulesEngineDemo.Framework
{
    public interface IDataProvider
    {
        public bool IsSetup { get; }
    }

    public interface IDataProvider<T> : IDataProvider where T : new()
    {
        public void Setup(dynamic model, Func<dynamic, T> func);
    }

    public abstract class DataProvider<T> : IDataProvider<T> where T : new()
    {
        public bool IsSetup { get; private set; }

        public void Setup(dynamic model, Func<dynamic, T> func)
        {
            IsSetup = true;
            SetupInner(model, func);
        }

        protected abstract void SetupInner(dynamic model, Func<dynamic, T> func);
    }
}
