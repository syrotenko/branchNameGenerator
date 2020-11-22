namespace GenerateBranchNameCore
{
    public class Branch : IBranch
    {
        public string BranchName => string.IsNullOrEmpty(NumberPart) && string.IsNullOrEmpty(MainTextPart) ? string.Empty : string.Join(Symbols.Dash.ToString(), NumberPart, MainTextPart);

        public string NumberPart { get; set; }
        public string MainTextPart { get; set; }
    }
}
