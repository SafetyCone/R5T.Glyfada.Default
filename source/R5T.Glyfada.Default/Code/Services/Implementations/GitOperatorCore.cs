﻿using System;

using R5T.Caledonia;
using R5T.Heraklion;
using R5T.Heraklion.Default;
using R5T.Heraklion.Extensions;
using R5T.Magyar.IO;
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

        public void Add(string path)
        {
            var command = GitCommandLine.Start(path)
                .Add(path)
                ;

            this.Execute(command);
        }

        public void Clone(string repositoryURL, string localDirectoryPath)
        {
            DirectoryHelper.CreateDirectoryOkIfExists(localDirectoryPath);

            var command = GitCommandLine.Start(localDirectoryPath)
                .Clone()
                .SetRepository(repositoryURL)
                .SetDirectory(localDirectoryPath)
                //.SetProgress()
                ;

            this.Execute(command);
        }

        public void Commit(string path, string message)
        {
            var command = GitCommandLine.Start(path)
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

        public void Push(string path)
        {
            var command = GitCommandLine.Start(path)
                .Push()
                ;

            this.Execute(command);
        }

        public bool IsUnderSourceControl(string path)
        {
            throw new NotImplementedException();
        }

        public void Pull(string path)
        {
            // TODO: use an IFileSystemOperator when available.
            var isDirectory = DirectoryHelper.IsDirectory(path);

            var gitCurrentDirectoryPath = isDirectory ? path : FileHelper.GetParentDirectoryPath(path);

            var command = GitCommandLine.Start(gitCurrentDirectoryPath)
                .Pull()
                ;

            this.Execute(command);
        }
    }
}
