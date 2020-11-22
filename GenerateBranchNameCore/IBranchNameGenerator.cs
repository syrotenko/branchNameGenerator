using System;

namespace GenerateBranchNameCore
{
    /// <summary>
    /// Interface for branch name generator
    /// </summary>
    interface IBranchNameGenerator
    {
        /// <summary>
        /// Predicate to check whether commit name is valid for processing
        /// </summary>
        Predicate<string> IsCommitNameValidPredicate { get; set; }

        /// <summary>
        /// Predicate to check whether symbol is valid for branch name
        /// </summary>
        Predicate<char> IsSymbolValidPredicate { get; set; }

        /// <summary>
        /// Generate branch name from commit name
        /// </summary>
        /// <param name="commitName">Commit name</param>
        /// <returns></returns>
        IBranch GenerateBranchName (ICommit commit);
    }
}
