namespace RulesEngineDemo.Framework
{
    public interface IRuleDefinition
	{
		public string Name { get; }
		public string Description { get; }
		public void Setup(RuleInstance ruleInstance);
		public RuleOutcome Execute();
	}
}
