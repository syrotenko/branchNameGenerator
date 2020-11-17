using System;
using System.ComponentModel;
using GenerateBranchNameCore;

namespace GenerateBranchName
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string commitName = "#12345: Test commit for \"GenerateBranchName\" project";
        public string CommitName 
        {
            get => commitName;
            set 
            {
                commitName = value;
                OnPropertyChanged(nameof(CommitName));
            }
        }

        private string branchName;
        public string BranchName 
        {
            get => branchName;
            set 
            {
                branchName = value;
                OnPropertyChanged(nameof(BranchName));
            }
        }

        private WpfCommand generateCmd;
        public WpfCommand GenerateCmd => generateCmd ?? (generateCmd = new WpfCommand(new Action<object>(Generate), new Predicate<object>((dummyParam) => CanExecuteGenerate)));


        private BranchNameGenerator branchNameGeneratorInstance;
        private BranchNameGenerator BranchNameGeneratorInstance => branchNameGeneratorInstance ?? (branchNameGeneratorInstance = new BranchNameGenerator());


        private void Generate(object commitName) 
        {
            BranchName = BranchNameGeneratorInstance.GenerateBranchName(CommitName);
        }

        private bool CanExecuteGenerate => !string.IsNullOrEmpty(CommitName);


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
