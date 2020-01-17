using RulesEngineDemo.DataProviders;
using RulesEngineDemo.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace RulesEngineDemo.Rules
{

	public class AgeGreaterThanOrEqualToExpectedV1 : RuleDefinition
    {
		[Serializable]
		public class Parameters
		{
			public int LegalAge { get; set; }

			public bool IsValid()
			{
				return LegalAge >= 18 && LegalAge <= 80;
			}
		}

		[Serializable]
		public class Inputs
		{
			public int CustomerAge { get; set; }

			public bool IsValid()
			{
				return true;
			}
		}

		public override string Name => typeof(AgeGreaterThanOrEqualToExpectedV1).Name;
		public override string Description => "Number greater than or equal to another number.";

		private Parameters _parameters;
		private readonly Inputs _inputs;
		private RuleInstance _ruleInstance;

		public AgeGreaterThanOrEqualToExpectedV1(IAgeProvider ageProvider)
		{
			_inputs = new Inputs() 
			{
				CustomerAge = ageProvider.Age 
			};
		}

		public override void Setup(RuleInstance ruleInstance)
		{
			_ruleInstance = ruleInstance;
			_parameters = JsonSerializer.Deserialize<Parameters>(ruleInstance.Parameters);
			if (!_parameters.IsValid())
			{
				throw new ApplicationException("Parameters are not valid");
			}
		}

		public override RuleOutcome Execute()
		{
			var passed = _inputs.CustomerAge >= _parameters.LegalAge;
			
			return CreateNewOutcome(passed);
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
			var parameters = typeof(Parameters).GetProperties();
			foreach (var property in parameters)
			{
				values.Add("{" + property.Name + "}", property.GetValue(_parameters).ToString());
			}
			var inputs = typeof(Inputs).GetProperties();
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
