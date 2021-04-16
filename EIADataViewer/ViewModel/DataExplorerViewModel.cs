﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DotNetCommon.MVVM;
using EIADataViewer.Events;
using EIADataViewer.ViewModel.Base;

namespace EIADataViewer.ViewModel
{
    public class DataExplorerViewModel : RobustViewModelBase
    {
        private readonly DatasetFinderViewModel _datasetFinderViewModel;

        public DataExplorerViewModel(DatasetFinderViewModel datasetFinderViewModel, RobustViewModelDependencies facade) : base(facade)
        {
            _datasetFinderViewModel = datasetFinderViewModel;
            CurrentChild = datasetFinderViewModel;
            Children = new ObservableCollection<ViewModelBase>();

            MessageHub.Subscribe<OpenViewModelEvent>(OnOpenViewModel);
            MessageHub.Subscribe<CloseViewModelEvent>(OnCloseViewModel);
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