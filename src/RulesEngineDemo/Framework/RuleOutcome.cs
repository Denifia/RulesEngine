using System;

namespace RulesEngineDemo.Framework
{
    public class RuleOutcome
	{
		public int RuleVersion { get; set; }
		public string RuleName { get; set; }
		public string RuleDescription { get; set; }
		public string Parameters { get; set; }
		public string Inputs { get; set; }
		public string ExecutedBy { get; set; }
		public DateTime ExecutedAt { get; set; }
		public bool Passed { get; set; }
		public string Message { get; set; }
	}
}
