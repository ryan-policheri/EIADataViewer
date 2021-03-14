using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliCommon.MVVM
{
    public class LazyTreeItemViewModel : ViewModelBase
    {
        private ILazyTreeItemBackingModel _model;

        public LazyTreeItemViewModel(ILazyTreeItemBackingModel model) : this(model, true)
        {
        }

        public LazyTreeItemViewModel(ILazyTreeItemBackingModel model, bool addPlaceholder)
        {
            _model = model;
            Children = new ObservableCollection<LazyTreeItemViewModel>();
            if(addPlaceholder) Children.Add(new LazyTreeItemViewModel(new LazyTreeItemPlaceHolder(), false));
        }

        public string Id => _model.GetId();

        public string Name => _model.GetItemName();

        public ObservableCollection<LazyTreeItemViewModel> Children { get; }

        public void AppendLoadedChildren(IEnumerable<LazyTreeItemViewModel> children)
        {
            Children.Clear();
            if (children == null) return;
            foreach(LazyTreeItemViewModel child in children)
            {
                Children.Add(child);
            }
        }
    }

    public class LazyTreeItemPlaceHolder : ILazyTreeItemBackingModel
    {
        public string GetItemName()
        {
            return "Loading...";
        }

        public string GetId()
        {
            throw new NotImplementedException();
        }
    }
}
