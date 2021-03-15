using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace EIADataViewer.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        public MainViewModel(DatasetFinderViewModel datasetFinderViewModel)
        {
            DatasetFinderViewModel = datasetFinderViewModel;
        }

        public DatasetFinderViewModel DatasetFinderViewModel { get; }

        public override async Task Initialize()
        {
            await base.Initialize();
            DatasetFinderViewModel.Initialize();
        }
    }
}
