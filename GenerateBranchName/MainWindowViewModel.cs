using System;
using System.ComponentModel;
using GenerateBranchNameCore;

namespace GenerateBranchName
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private ICommit commitInst = new Commit() { CommitName = "#12345: Test commit for \"GenerateBranchName\" project",
                                                    CommitType = CommitTypes.Bug };
        
        private IBranch branchInst = new Branch();
        public IBranch BranchInst 
        {
            get => branchInst;
            set 
            {
                branchInst = value;
                OnPropertyChanged(nameof(BranchName));
            }
        }

        public string CommitName 
        {
            get => commitInst.CommitName;
            set 
            {
                commitInst.CommitName = value;
                OnPropertyChanged(nameof(CommitName));
            }
        }

        public CommitTypes CommitType 
        {
            get => commitInst.CommitType;
            set 
            {
                commitInst.CommitType = value;
                Generate(CommitName);
                OnPropertyChanged(nameof(CommitType));
            }
        }

        public string BranchName => branchInst.BranchName;

        private WpfCommand generateCmd;
        public WpfCommand GenerateCmd => generateCmd ?? (generateCmd = new WpfCommand(new Action<object>(Generate), new Predicate<object>((dummyParam) => CanExecuteGenerate)));


        private BranchNameGenerator branchNameGeneratorInstance;
        private BranchNameGenerator BranchNameGeneratorInstance => branchNameGeneratorInstance ?? (branchNameGeneratorInstance = new BranchNameGenerator());


        private void Generate(object commitName) 
        {
            BranchInst = BranchNameGeneratorInstance.GenerateBranchName(commitInst);
        }

        private bool CanExecuteGenerate => !string.IsNullOrEmpty(CommitName);


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
