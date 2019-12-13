using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Caledonia;
using R5T.Caledonia.Default;
using R5T.Nikaia;
using R5T.Nikaia.Configuration;
using R5T.Nikaia.Configuration.Suebia;
using R5T.Richmond;


namespace R5T.Glyfada.Default.Construction
{
    public class Startup : ApplicationStartupBase
    {
        public Startup(ILogger<Startup> logger)
            : base(logger)
        {
        }

        protected override void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder, IServiceProvider configurationServicesProvider)
        {
            configurationBuilder
                .AddGitConfigurationJsonFile(configurationServicesProvider)
                ;
        }

        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            services
                .AddSingleton<IGitOperator, GitOperator>()
                .AddSingleton<IGitOperatorCore, GitOperatorCore>()
                .AddGitConfiguration()
                .AddSingleton<IGitExecutableFilePathProvider, GitExecutableFilePathProvider>()
                .AddSingleton<ICommandLineInvocationOperator, DefaultCommandLineInvocationOperator>()
                ;
        }
    }
}
