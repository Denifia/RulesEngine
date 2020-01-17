using RulesEngineDemo.DataProviders;
using RulesEngineDemo.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RulesEngineDemo.Rules
{
	public class NameOnBankAccountMustMatchCustomerNameV1 : RuleDefinitionBase
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
			public string BankAccountName { get; private set; }

			public string CustomerName { get; private set; }

			public Inputs(string bankAccountName, string customerName)
			{
				BankAccountName = bankAccountName;
				CustomerName = customerName;
			}

			public bool IsValid()
			{
				return true;
			}
		}

		public override string Name => "Name must match";
		public override string Description => "yeah it does!";

		internal new Parameters _parameters { get; set; }
		internal new Inputs _inputs { get; set; }

		public NameOnBankAccountMustMatchCustomerNameV1(IBankAccountProvider bankAccountProvider, ICustomerProvider customerProvider)
		{
			_inputs = new Inputs(bankAccountProvider.BankAccount.Name, customerProvider.Customer.Name);
		}

		public override void Setup(RuleInstance ruleInstance)
		{
			_ruleInstance = ruleInstance;
			_parameters = JsonSerializer.Deserialize<Parameters>(ruleInstance.Parameters);
		}

		public override bool ExecuteInner()
		{
			return _inputs.BankAccountName == _inputs.CustomerName;
		}
	}
}
