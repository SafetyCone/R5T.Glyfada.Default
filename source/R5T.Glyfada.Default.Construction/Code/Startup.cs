using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Glyfada.Standard;
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
                .AddGitConfiguration(configurationServicesProvider)
                ;
        }

        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            services
                .AddGitOperator()
                ;
        }
    }
}
