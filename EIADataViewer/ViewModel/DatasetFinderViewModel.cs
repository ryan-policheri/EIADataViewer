using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EIA.Domain.Constants;
using EIA.Domain.Model;
using EIA.Services.Clients;
using EIADataViewer.ModelWrappers;
using PoliCommon.MVVM;

namespace EIADataViewer.ViewModel
{
    public class DatasetFinderViewModel : ViewModelBase
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
            private set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            Category root = await _client.GetCategoryByIdAsync(EiaCategories.ABSOLUTE_ROOT);
            CategoryWrapper wrapper = new CategoryWrapper(root);
            Categories.Add(new LazyTreeItemViewModel(wrapper));
            //Category fullTree = await _client.GetCategoryTreeAsync(EiaCategories.ABSOLUTE_ROOT);
            //await _client.GetSeriesByIdAsync("ELEC.GEN.WND-IA-1.M");
        }

        public async Task LoadChildrenAsync(LazyTreeItemViewModel treeItem)
        {
            if (treeItem.ChildrenLoaded) return; //Children are already loaded. Do nothing
            Category itemToLoad = await _client.GetCategoryByIdAsync(int.Parse(treeItem.Id));
            if (itemToLoad.ChildCategories == null || itemToLoad.ChildCategories.Count == 0) treeItem.AppendLoadedChildren(null); //Pass in null to mark as a leaf
            else
            {
                ICollection<ILazyTreeItemBackingModel> loadedChildren = new List<ILazyTreeItemBackingModel>();
                foreach(Category children in itemToLoad.ChildCategories)
                {
                    loadedChildren.Add(new CategoryWrapper(children));
                }
                treeItem.AppendLoadedChildren(loadedChildren);
            }
        }

        private void ICollection<T>()
        {
            throw new NotImplementedException();
        }
    }
}
