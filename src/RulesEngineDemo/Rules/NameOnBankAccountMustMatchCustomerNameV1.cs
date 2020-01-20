using RulesEngineDemo.DataProviders;
using RulesEngineDemo.Framework;
using System;
using System.Text.Json;

namespace RulesEngineDemo.Rules
{
	public class NameOnBankAccountMustMatchCustomerNameV1 : RuleDefinition
	{
		[Serializable]
		public class Parameters : IParameters
		{
			public bool IsValid()
			{
				return true;
			}
		}

		[Serializable]
		public class Inputs : IInputs
		{
			public string BankAccountName { get; set; }

			public string CustomerName { get; set; }

			public bool IsValid()
			{
				return true;
			}
		}

		public override string Name => typeof(NameOnBankAccountMustMatchCustomerNameV1).Name;
		public override string Description => "yeah it does!";

		public NameOnBankAccountMustMatchCustomerNameV1(IBankAccountProvider bankAccountProvider, ICustomerProvider customerProvider)
			: base(bankAccountProvider, customerProvider)
		{
			// Map Inputs
			_inputs = new Inputs()
			{
				BankAccountName = bankAccountProvider.BankAccount.Name,
				CustomerName = customerProvider.Customer.Name
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

			// Rule Logic
			return inputs.BankAccountName == inputs.CustomerName;
		}
	}
}
