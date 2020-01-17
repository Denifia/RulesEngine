using RulesEngineDemo.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RulesEngineDemo.Rules
{
    public interface IParameters
    {
        public bool IsValid();
    }

    public interface IInputs
    {
        public bool IsValid();
    }

    public class RuleDefinitionBase : RuleDefinition
    {
        public override string Name => typeof(RuleDefinitionBase).Name;
        public override string Description => "";

        internal virtual IParameters _parameters { get; set; }
        internal virtual IInputs _inputs { get; set; }
        internal RuleInstance _ruleInstance { get; set; }

        public override void Setup(RuleInstance ruleInstance)
        {
            _ruleInstance = ruleInstance;
            _parameters = JsonSerializer.Deserialize<IParameters>(ruleInstance.Parameters);
            if (!_parameters.IsValid())
            {
                throw new ApplicationException("Parameters are not valid");
            }
        }

        public override RuleOutcome Execute()
        {
            return CreateNewOutcome(ExecuteInner());
        }

        public virtual bool ExecuteInner()
        {
            return true;
        }

        private RuleOutcome CreateNewOutcome(bool passed) => new RuleOutcome()
        {
            ExecutedAt = DateTime.Now,
            Inputs = JsonSerializer.Serialize(_inputs),
            Parameters = JsonSerializer.Serialize(_parameters),
            RuleDescription = _ruleInstance.Description,
            RuleName = _ruleInstance.Name,
            RuleVersion = _ruleInstance.Version,
            Passed = passed,
            Message = GenerateOutcomeMessage()
        };

        private string GenerateOutcomeMessage()
        {
            var outcomeMessageFormat = _ruleInstance.OutcomeMessageFormat;
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
