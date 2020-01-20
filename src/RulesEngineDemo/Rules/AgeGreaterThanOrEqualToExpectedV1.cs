using RulesEngineDemo.DataProviders;
using RulesEngineDemo.Framework;
using System;
using System.Text.Json;

namespace RulesEngineDemo.Rules
{
	public class AgeGreaterThanOrEqualToExpectedV1 : RuleDefinition
    {
		[Serializable]
		public class Parameters : IParameters
		{
			public int LegalAge { get; set; }

			public bool IsValid()
			{
				return LegalAge >= 18 && LegalAge <= 80;
			}
		}

		[Serializable]
		public class Inputs : IInputs
		{
			public int CustomerAge { get; set; }

			public bool IsValid()
			{
				return true;
			}
		}

		public override string Name => typeof(AgeGreaterThanOrEqualToExpectedV1).Name;
		public override string Description => "Number greater than or equal to another number.";

		public AgeGreaterThanOrEqualToExpectedV1(IAgeProvider ageProvider)
			: base(ageProvider)
		{
			// Map Inputs
			_inputs = new Inputs() 
			{
				CustomerAge = ageProvider.Age 
			};
		}

		protected override void DeserializeParameters(string parameters)
		{
			// Map Parameters
			_parameters = JsonSerializer.Deserialize<Parameters>(parameters);
		}

		protected override bool ExecuteInner()
		{
			var parameters = (Parameters)_parameters;
			var inputs = (Inputs)_inputs;

			// Rule Logic
			return inputs.CustomerAge >= parameters.LegalAge;
		}
	}
}
