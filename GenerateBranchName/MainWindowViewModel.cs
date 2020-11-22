using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GenerateBranchNameCore;

namespace GenerateBranchName
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private List<ICommit> commits = new List<ICommit>();
        public List<ICommit> Commits => commits;

        private List<IBranch> branches = new List<IBranch>();
        private List<IBranch> Branches 
        {
            get => branches;
            set 
            {
                branches = value;
                OnPropertyChanged(nameof(BranchNames));
            }
        }

        private string commitNames = "#12345: Test commit for \"GenerateBranchName\" project";
        public string CommitNames 
        {
            get => commitNames;
            set 
            {
                commitNames = value;
                OnPropertyChanged(nameof(CommitNames));
            }
        }

        private CommitTypes commitType;
        public CommitTypes CommitType 
        {
            get => commitType;
            set 
            {
                commitType = value;

                Generate(CommitNames);

                OnPropertyChanged(nameof(CommitType));
            }
        }

        public string BranchNames => string.Join(Symbols.NewLine.ToString(), Branches.Select(branch => branch.BranchName));

        private WpfCommand generateCmd;
        public WpfCommand GenerateCmd => generateCmd ?? (generateCmd = new WpfCommand(new Action<object>(Generate), new Predicate<object>((dummyParam) => CanExecuteGenerate)));


        private BranchNameGenerator branchNameGeneratorInstance;
        private BranchNameGenerator BranchNameGeneratorInstance => branchNameGeneratorInstance ?? (branchNameGeneratorInstance = new BranchNameGenerator());


        private void Generate(object commitNamesObj) 
        {
            if (commitNamesObj is string commitNames) 
            {
                Commits.Clear();
                Branches.Clear();

                foreach (string newCommitName in commitNames.Split(Symbols.NewLine))
                {
                    Commit newCommit = new Commit()
                    {
                        CommitName = newCommitName,
                        CommitType = CommitType
                    };


                    Commits.Add(newCommit);
                    Branches.Add(BranchNameGeneratorInstance.GenerateBranchName(newCommit));
                }

                OnPropertyChanged(nameof(BranchNames));
            }
        }

        private bool CanExecuteGenerate => !string.IsNullOrEmpty(CommitNames);


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
