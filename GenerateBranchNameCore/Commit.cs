using System;
using System.Collections.Generic;

namespace GenerateBranchNameCore
{
    public class Commit : ICommit
    {
        public string CommitName { get; set; }

        public string NumberPart => NumberPartExtractor(CommitName);

        public string MainTextPart => MainTextPartExtractor(CommitName);

        private Func<string, string> numberPartExtractor;
        public Func<string, string> NumberPartExtractor 
        { 
            get => numberPartExtractor ?? (numberPartExtractor = GetNumberPart); 
            set => numberPartExtractor = value; 
        }

        private Func<string, string> mainTextPartExtractor;
        public Func<string, string> MainTextPartExtractor 
        { 
            get => mainTextPartExtractor ?? (mainTextPartExtractor = GetMainTextPart); 
            set => mainTextPartExtractor = value; 
        }

        public CommitTypes CommitType { get; set; }


        private string GetNumberPart (string commitName) 
        {
            string[] splittedCommitName = commitName.Split(new char[] { Constants.DotsSymbol }, 2);
            
            if (splittedCommitName.Length > 0) 
            {
                return commitName.Split(new char[] { Constants.DotsSymbol }, 2)[0];
            }

            return string.Empty;
        }

        private string GetMainTextPart (string commitName)
        {
            string[] splittedCommitName = commitName.Split(new char[] { Constants.DotsSymbol }, 2);

            if (splittedCommitName.Length > 1)
            {
                return commitName.Split(new char[] { Constants.DotsSymbol }, 2)[1];
            }

            return string.Empty;
        }
    }
}
