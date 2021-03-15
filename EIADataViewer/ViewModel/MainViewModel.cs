using System.Threading.Tasks;

namespace EIADataViewer.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel(DatasetFinderViewModel foobarViewModel)
        {
            DatasetFinderViewModel = foobarViewModel;
        }

        public DatasetFinderViewModel DatasetFinderViewModel { get; }

        public async Task LoadAsync()
        {
            await DatasetFinderViewModel.LoadAsync();
        }
    }
}
