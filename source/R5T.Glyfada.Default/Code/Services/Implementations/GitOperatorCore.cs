using System;

using R5T.Caledonia;
using R5T.Heraklion;
using R5T.Nikaia;

using R5T.Glyfada.Commands;


namespace R5T.Glyfada.Default
{
    public class GitOperatorCore : IGitOperatorCore
    {
        private ICommandLineInvocationOperator CommandLineInvocationOperator { get; }
        private IGitExecutableFilePathProvider GitExecutableFilePathProvider { get; }

        public GitOperatorCore(ICommandLineInvocationOperator commandLineInvocationOperator, IGitExecutableFilePathProvider gitExecutableFilePathProvider)
        {
            this.CommandLineInvocationOperator = commandLineInvocationOperator;
            this.GitExecutableFilePathProvider = gitExecutableFilePathProvider;
        }

        public void Init(string directoryPath, bool quiet = false)
        {
            var command = GitCommandLine.New().Init()
                .SetDirectory(directoryPath)
                .Condition(quiet, (context) =>
                {
                    context.SetQuiet();
                })
                .BuildCommand();

            var gitExecutableFilePath = this.GitExecutableFilePathProvider.GetGitExecutableFilePath();

            var invocation = CommandLineInvocation.New(gitExecutableFilePath, command);

            var result = this.CommandLineInvocationOperator.Run(invocation);

            if(result.AnyError)
            {
                throw new Exception($"Execution failed. Command:\n{command}");
            }
        }
    }
}
