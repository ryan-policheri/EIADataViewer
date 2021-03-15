using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EIA.Domain.Constants;
using EIA.Domain.Model;
using EIA.Services.Clients;
using EIADataViewer.Core.ModelWrappers;
using MvvmCross.ViewModels;
using PoliCommon.MVVM;

namespace EIADataViewer.Core.ViewModels
{
    public class DatasetFinderViewModel : MvxViewModel
    {
        private EiaClient _client;

        public DatasetFinderViewModel(EiaClient client)
        {
            _client = client;
            _categories = new ObservableCollection<LazyTreeItemViewModel>();
        }

        private ObservableCollection<LazyTreeItemViewModel> _categories;
        public ObservableCollection<LazyTreeItemViewModel> Categories
        {
            get { return _categories; }
            private set { SetProperty(ref _categories, value); }
        }

        public override async Task Initialize()
        {
            Category root = await _client.GetCategoryByIdAsync(EiaCategories.ABSOLUTE_ROOT);
            CategorySeriesWrapper wrapper = new CategorySeriesWrapper(root);
            Categories.Add(new LazyTreeItemViewModel(wrapper));
        }

        public async Task LoadChildrenAsync(LazyTreeItemViewModel treeItem)
        {
            if (treeItem.ChildrenLoaded) return; //Children are already loaded. Do nothing
            Category itemToLoad = await _client.GetCategoryByIdAsync(int.Parse(treeItem.Id));
            CategorySeriesWrapper wrappedItem = new CategorySeriesWrapper(itemToLoad);
            treeItem.AppendLoadedChildren(wrappedItem.Children);
        }
    }
}
