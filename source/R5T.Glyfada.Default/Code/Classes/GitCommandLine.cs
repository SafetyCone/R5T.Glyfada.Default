using System;

using R5T.Heraklion;

using R5T.Glyfada.Commands;
using R5T.Volos;


namespace R5T.Glyfada.Default
{
    public static class GitCommandLine
    {
        public static ICommandBuilderContext<GitContext> New()
        {
            var commandBuilderContext = CommandLine.New<GitContext>();
            return commandBuilderContext;
        }
    }
}
