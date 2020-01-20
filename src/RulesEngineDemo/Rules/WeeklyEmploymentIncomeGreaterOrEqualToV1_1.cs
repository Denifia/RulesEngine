using RulesEngineDemo.DataProviders;
using RulesEngineDemo.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RulesEngineDemo.Rules
{
	public class WeeklyEmploymentIncomeGreaterOrEqualToV1_1 : RuleDefinition
	{
		[Serializable]
		public class Parameters : IParameters
		{
			public int ExpectedIncomePerWeek { get; set; }

			public bool IsValid()
			{
				return true;
			}
		}

		[Serializable]
		public class Inputs : IInputs
		{
			public int IncomePerWeek { get; set; }

			public bool IsValid()
			{
				return true;
			}
		}

		public override string Name => typeof(WeeklyEmploymentIncomeGreaterOrEqualToV1_1).Name;
		public override string Description => "arstart";

		public WeeklyEmploymentIncomeGreaterOrEqualToV1_1(IIncomeProvider incomeProvider)
			: base(incomeProvider)
		{
			// Map Inputs
			_inputs = new Inputs()
			{
				IncomePerWeek = incomeProvider.Income
			};
		}

		protected override void DeserializeParameters(string parameters)
		{
			// Map Parameters
			_parameters = JsonSerializer.Deserialize<Parameters>(parameters);
		}

		protected override bool ExecuteInner()
		{
			var inputs = (Inputs)_inputs;
			var parameters = (Parameters)_parameters;

			// Rule Logic
			return inputs.IncomePerWeek >= parameters.ExpectedIncomePerWeek;
		}
	}
}
