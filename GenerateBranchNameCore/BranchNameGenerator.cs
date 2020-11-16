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

        /// <inheritdoc/>
        public bool IsCommitNameValid (string commitName)
        {
            return commitName == null;
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


        private string ProcessNumberCommitText (string mainCommitText)
        {
            if (mainCommitText.StartsWith(Constants.HashSymbol.ToString()))
            {
                return mainCommitText.Remove(0, 1);
            }

            return mainCommitText;
        }

        private string ProcessMainCommitText (string mainCommitText)
        {
            string processedCommitText = mainCommitText.Trim()
                                                       .ToLower()
                                                       .Replace(Constants.WhiteSpaceSymbol, Constants.DashSymbol);

            return processedCommitText.Where(symbol => IsSymbolValidPredicate(symbol)).ToString();
        }
    }
}
