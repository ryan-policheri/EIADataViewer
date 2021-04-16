using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DotNetCommon.EventAggregation;
using DotNetCommon.MVVM;
using EIADataViewer.Events;
using EIADataViewer.ViewModel.Base;

namespace EIADataViewer.ViewModel
{
    public class MainViewModel : RobustViewModelBase
    {
        private readonly DatasetFinderViewModel _datasetFinderViewModel;
        private readonly SeriesViewModel _seriesViewModel;
        private readonly IMessageHub _messageHub;

        public MainViewModel(DatasetFinderViewModel datasetFinderViewModel, SeriesViewModel seriesViewModel, IMessageHub messageHub, RobustViewModelDependencies facade) : base(facade)
        {
            _datasetFinderViewModel = datasetFinderViewModel;
            _seriesViewModel = seriesViewModel;
            CurrentChild = datasetFinderViewModel;
            Children = new ObservableCollection<ViewModelBase>();

            _messageHub = messageHub;
            _messageHub.Subscribe<OpenViewModelEvent>(OnOpenViewModel);
            _messageHub.Subscribe<CloseViewModelEvent>(OnCloseViewModel);
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

        public ObservableCollection<ViewModelBase> Children { get; }

        public async Task LoadAsync()
        {
            Children.Add(_datasetFinderViewModel);
            CurrentChild = _datasetFinderViewModel;
            await _datasetFinderViewModel.LoadAsync();
        }

        private async void OnViewModelTransition(ViewModelTransitionEvent args)
        {
            if (args.SenderType == nameof(DatasetFinderViewModel))
            {
                SeriesViewModel vm = this.Resolve<SeriesViewModel>();
                Children.Add(vm);
                CurrentChild = vm;
                await vm.LoadAsync(args.Id);
            }    
            else if (args.SenderType == nameof(SeriesViewModel))
            {
                CurrentChild = _datasetFinderViewModel;
            }
        }

        private void OnCloseViewModel(CloseViewModelEvent args)
        {
            if (args.SenderTypeName == nameof(SeriesViewModel))
            {
                CurrentChild = _datasetFinderViewModel;
                this.Children.Remove(args.Sender as ViewModelBase);
            }
        }

        private async void OnOpenViewModel(OpenViewModelEvent args)
        {
            if (args.SenderTypeName == nameof(DatasetFinderViewModel))
            {
                SeriesViewModel vm = this.Resolve<SeriesViewModel>();
                Children.Add(vm);
                CurrentChild = vm;
                await vm.LoadAsync(args.Id);
            }
        }
    }
}
