namespace GenerateBranchNameCore
{
    public interface IBranch
    {
        string BranchName { get; }
        string NumberPart { get; set; }
        string MainTextPart { get; set; }
    }
}
