using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Richmond;
using R5T.Derby;


namespace R5T.Glyfada.Default.Construction
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.TryGitInitDirectory();
        }

        private static void TryGitInitDirectory()
        {
            var directoryPath = @"C:\Temp\Git\Test2";

            var serviceProvider = Program.GetServiceProvider();

            var gitOperator = serviceProvider.GetRequiredService<IGitOperator>();

            gitOperator.Init(directoryPath);
        }

        private static IServiceProvider GetServiceProvider()
        {
            var serviceProvider = ApplicationBuilder.New().UseStartupWithDerbyConfigurationStartup<Startup>();
            return serviceProvider;
        }
    }
}
