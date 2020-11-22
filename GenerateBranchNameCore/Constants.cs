namespace GenerateBranchNameCore
{
    internal static class Constants
    {
        public const char HashSymbol = '#';
        public const char DotsSymbol = ':';
        public const char WhiteSpaceSymbol = ' ';
        public const char DashSymbol = '-';
        public const char SlashSymbol = '/';
    }

    public enum CommitTypes 
    {
        Bug,
        Feature,
        Task,
    }
}
