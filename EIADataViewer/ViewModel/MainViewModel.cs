using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DotNetCommon.MVVM;
using EIADataViewer.ViewModel.Base;

namespace EIADataViewer.ViewModel
{
    public class MainViewModel : RobustViewModelBase
    {
        private readonly DataExplorerViewModel _dataExplorer;

        public MainViewModel(DataExplorerViewModel dataExplorer, RobustViewModelDependencies facade) : base(facade)
        {
            _dataExplorer = dataExplorer;
        }

        private ViewModelBase _currentChild;
        public ViewModelBase CurrentChild 
        {
            get { return _currentChild; }
            set
            {
                _currentChild = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            CurrentChild = _dataExplorer;
            await _dataExplorer.LoadAsync();
        }
    }
}
