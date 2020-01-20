using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace RulesEngineDemo.Framework
{
    public interface IRuleDefinition
    {
        public string Name { get; }
        public string Description { get; }
        public void Setup(RuleInstance ruleInstance);
        public RuleOutcome Execute();
    }

    public abstract class RuleDefinition : IRuleDefinition
    {
        public virtual string Name => typeof(RuleDefinition).Name;
        public virtual string Description => string.Empty;
        protected virtual IParameters _parameters { get; set; }
        protected virtual IInputs _inputs { get; set; }
        protected RuleInstance _ruleInstance { get; set; }
        protected List<IDataProvider> _dataProviders { get; private set; } = new List<IDataProvider>();

        public RuleDefinition(params IDataProvider[] dataProviders)
        {
            _dataProviders = dataProviders.ToList();
        }

        public void Setup(RuleInstance ruleInstance)
        {
            _ruleInstance = ruleInstance;
            DeserializeParameters(ruleInstance.Parameters);
            if (!_parameters.IsValid())
            {
                throw new ApplicationException("Parameters are not valid");
            }
        }

        public RuleOutcome Execute()
        {
            if (_dataProviders.Any(x => !x.IsSetup))
            {
                return DataNotAvailableOutcome();
            }
            return ExecutedOutcome(ExecuteInner());
        }

        protected abstract void DeserializeParameters(string parameters);

        protected abstract bool ExecuteInner();

        private RuleOutcome ExecutedOutcome(bool passed) => new RuleOutcome()
        {
            ExecutedAt = DateTime.Now,
            Inputs = JsonSerializer.Serialize(_inputs),
            Parameters = JsonSerializer.Serialize(_parameters),
            RuleDescription = _ruleInstance.Description,
            RuleName = _ruleInstance.Name,
            RuleVersion = _ruleInstance.Version,
            Result = passed ? RuleOutcomeResult.Passed : RuleOutcomeResult.Failed,
            Message = GenerateOutcomeMessage()
        };

        private RuleOutcome DataNotAvailableOutcome() => new RuleOutcome()
        {
            ExecutedAt = DateTime.Now,
            Inputs = JsonSerializer.Serialize(_inputs),
            Parameters = JsonSerializer.Serialize(_parameters),
            RuleDescription = _ruleInstance.Description,
            RuleName = _ruleInstance.Name,
            RuleVersion = _ruleInstance.Version,
            Result = RuleOutcomeResult.DataNotAvailable,
            Message = GenerateOutcomeMessage()
        };

        private string GenerateOutcomeMessage()
        {
            var outcomeMessageFormat = _ruleInstance.OutcomeMessageFormat ?? string.Empty;
            var values = new Dictionary<string, string>();
            var parameters = _parameters.GetType().GetProperties();
            foreach (var property in parameters)
            {
                values.Add("{" + property.Name + "}", property.GetValue(_parameters).ToString());
            }
            var inputs = _inputs.GetType().GetProperties();
            foreach (var property in inputs)
            {
                values.Add("{" + property.Name + "}", property.GetValue(_inputs).ToString());
            }
            foreach (var key in values.Keys)
            {
                outcomeMessageFormat = outcomeMessageFormat.Replace(key, values[key]);
            }
            return outcomeMessageFormat;
        }
    }
}
