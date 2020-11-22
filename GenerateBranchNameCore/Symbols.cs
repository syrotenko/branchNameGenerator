namespace GenerateBranchNameCore
{
    public static class Symbols
    {
        public const char Hash = '#';
        public const char Dots = ':';
        public const char WhiteSpace = ' ';
        public const char Dash = '-';
        public const char Slash = '/';
        public const char NewLine = '\n';
    }

    public enum CommitTypes 
    {
        Bug,
        Feature,
        Task,
    }
}
