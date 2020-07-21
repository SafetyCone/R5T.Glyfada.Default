using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Caledonia;
using R5T.Dacia;
using R5T.Nikaia;


namespace R5T.Glyfada.Default
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="GitOperator"/> implementation of <see cref="IGitOperator"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddGitOperator(this IServiceCollection services,
            IServiceAction<ICommandLineInvocationOperator> commandLineInvocationOperatorAction,
            IServiceAction<IGitExecutableFilePathProvider> gitExecutableFilePathProviderAction)
        {
            services
                .AddSingleton<IGitOperator, GitOperator>()
                .Run(commandLineInvocationOperatorAction)
                .Run(gitExecutableFilePathProviderAction)
                ;

            return services;
        }

        /// <summary>
        /// Adds the <see cref="GitOperator"/> implementation of <see cref="IGitOperator"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IGitOperator> AddGitOperatorAction(this IServiceCollection services,
            IServiceAction<ICommandLineInvocationOperator> commandLineInvocationOperatorAction,
            IServiceAction<IGitExecutableFilePathProvider> gitExecutableFilePathProviderAction)
        {
            var serviceAction = ServiceAction.New<IGitOperator>(() => services.AddGitOperator(
                commandLineInvocationOperatorAction,
                gitExecutableFilePathProviderAction));

            return serviceAction;
        }
    }
}
