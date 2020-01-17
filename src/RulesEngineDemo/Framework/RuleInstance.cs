using System;

namespace RulesEngineDemo.Framework
{
    public class RuleInstance
	{
		public int Version { get; set; }
		public string RuleDefinition { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string OutcomeMessageFormat { get; set; }
		public RuleStatus Status { get; set; }
		public string Parameters { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedAt { get; set; }
	}
}
