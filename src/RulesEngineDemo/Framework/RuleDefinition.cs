using System.Collections.Generic;
using System.Text;

namespace RulesEngineDemo.Framework
{

	public abstract class RuleDefinition : IRuleDefinition
	{
		public abstract string Name { get; }
		public abstract string Description { get; }
		public abstract void Setup(RuleInstance ruleInstance);
		public abstract RuleOutcome Execute();
	}
}
