using System;

using R5T.Heraklion;

using R5T.Glyfada.Commands;
using R5T.Volos;


namespace R5T.Glyfada.Default
{
    public static class GitCommandLine
    {
        public static ICommandBuilderContext<GitContext> Start()
        {
            var commandBuilderContext = CommandLine.Start<GitContext>();
            return commandBuilderContext;
        }

        public static ICommandBuilderContext<GitContext> Start(string directoryPath)
        {
            var gitContext = GitCommandLine.Start()
                .SetCurrentDirectory(directoryPath)
                ;

            return gitContext;
        }
    }
}
