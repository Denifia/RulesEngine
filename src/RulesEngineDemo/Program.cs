using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RulesEngineDemo.DataProviders;
using RulesEngineDemo.Framework;
using RulesEngineDemo.Models;
using System;

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
            ageProvider.Setup(application, x => x.Age);
            var incomeProvider = serviceProvider.GetService<IIncomeProvider>();
            incomeProvider.Setup(application, x => x.Income);

            // Get all the rules
            var rulesProvider = serviceProvider.GetService<IRulesProvider>();
            var rules = rulesProvider.GetRules();

            // Run all the rules
            foreach (var rule in rules)
            {
                var outcome = rule.Execute();
                logger.LogInformation("{result}: {message}", outcome.Passed ? "Passed" : "Failed", outcome.Message);
            }
            logger.LogInformation("Done!");
            Console.ReadKey();

            // TODO: Add rule outcomes and "friendly messages".
            // TODO: Add rule tags to signify which rules to run based on the info we have... 
            // ...or just run all rules and use inconclusives
            // ...and query the internal application model to see what data we need to collect or not
            // TODO: How to run against partial applications?
            // TODO: Update the class diagram to match current implementation
        }
    }
}
