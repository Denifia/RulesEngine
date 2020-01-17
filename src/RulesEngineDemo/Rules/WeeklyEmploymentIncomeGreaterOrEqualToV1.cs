using RulesEngineDemo.DataProviders;
using RulesEngineDemo.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RulesEngineDemo.Rules
{
 //   public class WeeklyEmploymentIncomeGreaterOrEqualToV1 : RuleDefinition
	//{
	//	public class Parameters
	//	{
	//		public int ExpectedIncomePerWeek { get; set; }
	//	}

	//	public class Inputs
	//	{
	//		public int IncomePerWeek { get; private set; }

	//		public Inputs(int incomePerWeek)
	//		{
	//			IncomePerWeek = incomePerWeek;
	//		}
	//	}

	//	public override string Name => "Weekly Employment Income Greater Or Equal To V1";
	//	public override string Description => "arstart";

	//	private Parameters _parameters;
	//	private Inputs _inputs;

	//	public WeeklyEmploymentIncomeGreaterOrEqualToV1(IIncomeProvider incomeProvider)
	//	{
	//		_inputs = new Inputs(incomeProvider.Income);
	//	}

	//	public override void Setup(string parameters)
	//	{
	//		_parameters = JsonSerializer.Deserialize<Parameters>(parameters);
	//	}

	//	public override bool Execute()
	//	{
	//		return _inputs.IncomePerWeek >= _parameters.ExpectedIncomePerWeek;
	//	}
	//}
}
