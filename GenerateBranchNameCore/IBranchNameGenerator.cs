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
        /// Default method to check whether commit name is valid for processing
        /// </summary>
        /// <param name="commitName">Commit name</param>
        /// <returns></returns>
        bool IsCommitNameValid (string commitName);

        /// <summary>
        /// Default method to check whether symbol is valid for branch name
        /// </summary>
        /// <param name="symbol">Symbol from commit name</param>
        /// <returns></returns>
        bool IsSymbolValid (char symbol);

        /// <summary>
        /// Generate branch name from commit name
        /// </summary>
        /// <param name="commitName">Commit name</param>
        /// <returns></returns>
        string GenerateBranchName (string commitName);
    }
}
