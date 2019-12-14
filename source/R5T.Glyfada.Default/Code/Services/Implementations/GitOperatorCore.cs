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

        private void Execute(string arguments)
        {
            var gitExecutableFilePath = this.GitExecutableFilePathProvider.GetGitExecutableFilePath();

            var invocation = CommandLineInvocation.New(gitExecutableFilePath, arguments);

            var result = this.CommandLineInvocationOperator.Run(invocation);

            if (result.ExitCode != 0)
            {
                throw new Exception($"Execution failed. Error:\n{result.GetErrorText()}\nOutput:\n{result.GetOutputText()}\nArguments:\n{arguments}");
            }
            else
            {
                Console.WriteLine(result.GetOutputText());
                Console.WriteLine(result.GetErrorText());
            }
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

            this.Execute(command);
        }

        public void Clone(string repositoryURL, string localDiretoryPath)
        {
            var command = GitCommandLine.New().Clone()
                .SetRepository(repositoryURL)
                .SetDirectory(localDiretoryPath)
                .SetProgress()
                .BuildCommand();

            this.Execute(command);
        }
    }
}
