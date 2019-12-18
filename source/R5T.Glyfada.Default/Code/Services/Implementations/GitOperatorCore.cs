using System;

using R5T.Caledonia;
using R5T.Heraklion;
using R5T.Heraklion.Default;
using R5T.Heraklion.Extensions;
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

        private void Execute(ICommandBuilderContext command)
        {
            var gitExecutableFilePath = this.GitExecutableFilePathProvider.GetGitExecutableFilePath();

            this.CommandLineInvocationOperator.Execute(gitExecutableFilePath, command);
        }

        public void Add(string localPath)
        {
            var command = GitCommandLine.Start(localPath)
                .Add(localPath)
                ;

            this.Execute(command);
        }

        public void Clone(string repositoryURL, string localDiretoryPath)
        {
            var command = GitCommandLine.Start(localDiretoryPath)
                .Clone()
                .SetRepository(repositoryURL)
                .SetDirectory(localDiretoryPath)
                .SetProgress()
                ;

            this.Execute(command);
        }

        public void Commit(string localPath, string message)
        {
            var command = GitCommandLine.Start(localPath)
                .Commit()
                .SetMessage(message)
                ;

            this.Execute(command);
        }

        public void Init(string directoryPath, bool quiet = false)
        {
            var command = GitCommandLine.Start(directoryPath)
                .Init()
                .SetDirectory(directoryPath)
                .Condition(quiet, (context) =>
                {
                    context.SetQuiet();
                })
                ;

            this.Execute(command);
        }

        public void Push(string directoryPath)
        {
            var command = GitCommandLine.Start(directoryPath)
                .Push()
                ;

            this.Execute(command);
        }
    }
}
