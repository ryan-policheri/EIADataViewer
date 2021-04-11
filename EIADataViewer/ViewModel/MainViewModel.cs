using System.Threading.Tasks;
using DotNetCommon.EventAggregation;
using DotNetCommon.MVVM;
using EIADataViewer.Events;

namespace EIADataViewer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DatasetFinderViewModel _datasetFinderViewModel;
        private readonly SeriesViewModel _seriesViewModel;
        private readonly IMessageHub _messageHub;

        public MainViewModel(DatasetFinderViewModel datasetFinderViewModel, SeriesViewModel seriesViewModel, IMessageHub messageHub)
        {
            _datasetFinderViewModel = datasetFinderViewModel;
            _seriesViewModel = seriesViewModel;
            CurrentChild = datasetFinderViewModel;
            _messageHub = messageHub;
            _messageHub.Subscribe<ViewModelTransitionEvent>(OnViewModelTransition);
        }

        private ViewModelBase _currentChild;
        public ViewModelBase CurrentChild 
        {
            get { return _currentChild; }
            private set
            {
                _currentChild = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            CurrentChild = _datasetFinderViewModel;
            await _datasetFinderViewModel.LoadAsync();
        }

        private async void OnViewModelTransition(ViewModelTransitionEvent args)
        {
            if (args.SenderType == nameof(DatasetFinderViewModel))
            {
                CurrentChild = _seriesViewModel;
                await _seriesViewModel.LoadAsync(args.Id);
            }    
            else if (args.SenderType == nameof(SeriesViewModel))
            {
                CurrentChild = _datasetFinderViewModel;
            }
        }
    }
}
