using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RulesEngineDemo.DataProviders;
using RulesEngineDemo.Framework;
using RulesEngineDemo.Models;
using System;
using System.Collections.Generic;

namespace RulesEngineDemo
{
    public class Program
    {
        public static void Main()
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole())
                .AddScoped<IBankAccountProvider, BankAccountProvider>()
                .AddScoped<ICustomerProvider, CustomerProvider>()
                .AddScoped<IAgeProvider, AgeProvider>()
                .AddScoped<IIncomeProvider, IncomeProvider>()
                .AddScoped<IRulesProvider>(x => new RulesProvider(x))
                .BuildServiceProvider();

            //services
            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation("starting...");

            // Get a payload
            var application = new Application()
            {
                Age = 15,
                BankAccount = new BankAccount { Name = "Luke" },
                Customer = new Customer { Name = "Luke" },
                Income = 600
            };

            // Map payload to providers
            var bankProvider = serviceProvider.GetService<IBankAccountProvider>();
            bankProvider.Setup(application, x => x.BankAccount);
            var customerProvider = serviceProvider.GetService<ICustomerProvider>();
            customerProvider.Setup(application, x => x.Customer);
            var ageProvider = serviceProvider.GetService<IAgeProvider>();
            //ageProvider.Setup(application, x => x.Age);
            var incomeProvider = serviceProvider.GetService<IIncomeProvider>();
            incomeProvider.Setup(application, x => x.Income);

            // Get all the rules
            var rulesProvider = serviceProvider.GetService<IRulesProvider>();
            
            // Run all the rules
            logger.LogInformation("Attempt 1...");
            var rules = rulesProvider.GetRules();
            RunRules(logger, rules);

            // Fetch more data...
            ageProvider.Setup(application, x => x.Age);

            // Run all the rules, again
            Console.WriteLine();
            logger.LogInformation("Attempt 2...");
            rules = rulesProvider.GetRules();
            RunRules(logger, rules);

            logger.LogInformation("Done!");
            Console.ReadKey();

            // TODO: Update the class diagram to match current implementation
        }

        private static void RunRules(ILogger<Program> logger, IEnumerable<RuleDefinition> rules)
        {
            foreach (var rule in rules)
            {
                var outcome = rule.Execute();
                logger.LogInformation("{RuleName} - {description}\n\t{result}: {message}",
                    rule.Name,
                    rule.Description,
                    outcome.Result.ToString(),
                    outcome.Message);
            }
        }
    }
}
