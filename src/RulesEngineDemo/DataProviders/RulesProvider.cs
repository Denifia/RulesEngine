using Microsoft.Extensions.DependencyInjection;
using RulesEngineDemo.Framework;
using System;
using System.Collections.Generic;

namespace RulesEngineDemo.DataProviders
{
    public interface IRulesProvider
    {
        public IEnumerable<RuleDefinition> GetRules();
    }

    public class RulesProvider : IRulesProvider
    {
		private readonly IServiceProvider _serviceProvider;

		public RulesProvider(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public IEnumerable<RuleDefinition> GetRules()
		{
			var ruleInstances = GetRuleInstances();

			var rules = new List<RuleDefinition>();
			var assembly = typeof(RuleInstance).Assembly;
			foreach (var ruleInstance in ruleInstances)
			{
				var ruleDefinitionType = assembly.GetType(ruleInstance.RuleDefinition);
				var ruleDefinition = ActivatorUtilities.CreateInstance(_serviceProvider, ruleDefinitionType) as RuleDefinition;
				ruleDefinition.Setup(ruleInstance);
				rules.Add(ruleDefinition);
			}
			return rules;
		}

		private IEnumerable<RuleInstance> GetRuleInstances()
        {
            return new RuleInstance[]
			{
				new RuleInstance
				{
					Name = "Must be of legal age.",
					Description = "Government regulation 16a.",
					OutcomeMessageFormat = "Customers age of {CustomerAge} must be {LegalAge} or over.",
					Parameters = "{\"LegalAge\":18}",
					Version = 1,
					Status = RuleStatus.Enabled,
					RuleDefinition = "RulesEngineDemo.Rules.AgeGreaterThanOrEqualToExpectedV1"
				}
				//,
				//new RuleInstance
				//{
				//	Name = "Bank acocunt name must match",
				//	Description = "mhmmm",
				//	Parameters = "{}",
				//	Version = 1,
				//	Status = RuleStatus.Enabled,
				//	RuleDefinition = "RulesEngineDemo.Rules.NameOnBankAccountMustMatchCustomerNameV1"
				//},
				//new RuleInstance
				//{
				//	Name = "Income must be over something per week",
				//	Description = "arstartsar",
				//	Parameters = "{\"ExpectedIncomePerWeek\":550}",
				//	Version = 1,
				//	Status = RuleStatus.Enabled,
				//	RuleDefinition = "RulesEngineDemo.Rules.WeeklyEmploymentIncomeGreaterOrEqualToV1_1"
				//},
			};
		}
    }
}
