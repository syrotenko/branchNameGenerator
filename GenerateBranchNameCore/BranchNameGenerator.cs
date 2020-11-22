using System;
using System.Linq;

namespace GenerateBranchNameCore
{
    /// <summary>
    /// Branch name generator implementation
    /// </summary>
    public class BranchNameGenerator : IBranchNameGenerator
    {
        private Predicate<string> isCommitNameValidPredicate;
        /// <inheritdoc/>
        public Predicate<string> IsCommitNameValidPredicate
        {
            get => isCommitNameValidPredicate ?? (isCommitNameValidPredicate = IsCommitNameValid);
            set => isCommitNameValidPredicate = value;
        }

        private Predicate<char> isSymbolValidPredicate;
        /// <inheritdoc/>
        public Predicate<char> IsSymbolValidPredicate 
        { 
            get => isSymbolValidPredicate ?? (isSymbolValidPredicate = IsSymbolValid);
            set => isSymbolValidPredicate = value;
        }

        /// <inheritdoc/>
        public IBranch GenerateBranchName (ICommit commit)
        {
            if (!IsCommitNameValidPredicate(commit.CommitName)) 
            {
                throw new ArgumentException(Resources.InvalidCommitNameError);
            }

            return new Branch()
            {
                NumberPart = ProcessNumberCommitText(commit.NumberPart, commit.CommitType),
                MainTextPart = ProcessMainCommitText(commit.MainTextPart)
            };
        }


        /// <summary>
        /// Default method to check whether commit name is valid for processing
        /// </summary>
        /// <param name="commitName">Commit name</param>
        /// <returns></returns>
        private bool IsCommitNameValid (string commitName)
        {
            return commitName != null;
        }

        /// <summary>
        /// Default method to check whether symbol is valid for branch name
        /// </summary>
        /// <param name="symbol">Symbol from commit name</param>
        /// <returns></returns>
        private bool IsSymbolValid (char symbol)
        {
            // Valid symbols
            //[a-z]
            //[0-9]
            //'-'
            return char.IsLetter(symbol) || char.IsNumber(symbol) || symbol.Equals(Symbols.Dash);
        }

        private string ProcessNumberCommitText (string numberCommitText, CommitTypes commitType)
        {
            return string.Join(Symbols.Slash.ToString(), 
                               ConvertCommitTypesToString(commitType),
                               numberCommitText.StartsWith(Symbols.Hash.ToString()) ? numberCommitText.Remove(0, 1) : numberCommitText);
        }

        private string ProcessMainCommitText (string mainCommitText)
        {
            string processedCommitText = mainCommitText.Trim()
                                                       .ToLower()
                                                       .Replace(Symbols.WhiteSpace, Symbols.Dash);

            return new string(processedCommitText.Where(symbol => IsSymbolValidPredicate(symbol)).ToArray());
        }

        private string ConvertCommitTypesToString (CommitTypes? commitType) 
        {
            if (commitType == null) 
            {
                return string.Empty;
            }

            switch (commitType) 
            {
                case CommitTypes.Bug:
                    return Resources.CommitTypeBug;

                case CommitTypes.Feature:
                    return Resources.CommitTypeFeature;

                case CommitTypes.Task:
                    return Resources.CommitTypeTask;

                default:
                    return string.Empty;
            }
        }
    }
}
