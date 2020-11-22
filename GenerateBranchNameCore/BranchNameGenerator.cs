using System;
using System.Collections.Generic;
using System.Linq;

namespace GenerateBranchNameCore
{
    /// <summary>
    /// Branch name generator implementation
    /// </summary>
    public class BranchNameGenerator : IBranchNameGenerator
    {
        private Predicate<char> isSymbolValidPredicate;
        /// <inheritdoc/>
        public Predicate<char> IsSymbolValidPredicate 
        { 
            get => isSymbolValidPredicate ?? (isSymbolValidPredicate = IsSymbolValid);
            set => isSymbolValidPredicate = value;
        }

        private Predicate<string> isCommitNameValidPredicate;
        /// <inheritdoc/>
        public Predicate<string> IsCommitNameValidPredicate 
        { 
            get => isCommitNameValidPredicate ?? (isCommitNameValidPredicate = IsCommitNameValid);
            set => isCommitNameValidPredicate = value;
        }

        public CommitTypes? CommitType { get; set; }


        /// <inheritdoc/>
        public bool IsCommitNameValid (string commitName)
        {
            return commitName != null;
        }

        /// <inheritdoc/>
        public bool IsSymbolValid (char symbol)
        {
            // Valid symbols
            //[a-z]
            //[0-9]
            //'-'
            return char.IsLetter(symbol) || char.IsNumber(symbol) || symbol.Equals(Constants.DashSymbol);
        }

        /// <inheritdoc/>
        public string GenerateBranchName (string commitName)
        {
            if (!IsCommitNameValidPredicate(commitName)) 
            {
                throw new ArgumentException(Resources.InvalidCommitNameError);
            }

            string branchName = commitName.Trim();
            
            if (branchName.Any())
            {
                List<string> splittedStrings = branchName.Split(new char[] { Constants.DotsSymbol }, 2).ToList();

                if (splittedStrings.Count >= 2)
                {
                    splittedStrings[0] = ProcessNumberCommitText(splittedStrings[0]);
                    splittedStrings[1] = ProcessMainCommitText(splittedStrings[1]);
                }

                branchName = string.Join(Constants.DashSymbol.ToString(), splittedStrings);
            }

            return branchName;
        }


        private string ProcessNumberCommitText (string numberCommitText)
        {
            return string.Join(Constants.SlashSymbol.ToString(), 
                               ConvertCommitTypesToString(CommitType),
                               numberCommitText.StartsWith(Constants.HashSymbol.ToString()) ? numberCommitText.Remove(0, 1) : numberCommitText);
        }

        private string ProcessMainCommitText (string mainCommitText)
        {
            string processedCommitText = mainCommitText.Trim()
                                                       .ToLower()
                                                       .Replace(Constants.WhiteSpaceSymbol, Constants.DashSymbol);

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
