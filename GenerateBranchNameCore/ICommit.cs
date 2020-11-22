using System;

namespace GenerateBranchNameCore
{
    public interface ICommit
    {
        string CommitName { get; set; }
        string NumberPart { get; }
        string MainTextPart { get; }
        CommitTypes CommitType { get; set; }

        Func<string, string> NumberPartExtractor { get; set; }
        Func<string, string> MainTextPartExtractor { get; set; }
    }
}
