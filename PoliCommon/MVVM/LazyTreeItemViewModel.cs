﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            if (addPlaceholder) Children.Add(new LazyTreeItemViewModel(new LazyTreeItemPlaceHolder(), false));
            ChildrenLoaded = model.IsKnownLeaf();
        }

        public string Id => _model.GetId();

        public string Name => _model.GetItemName();

        public ObservableCollection<LazyTreeItemViewModel> Children { get; }

        public bool ChildrenLoaded { get; private set; }

        public void AppendLoadedChildren(IEnumerable<ILazyTreeItemBackingModel> children)
        {
            Children.Clear();
            if (children == null) return;
            foreach (ILazyTreeItemBackingModel child in children)
            {
                Children.Add(new LazyTreeItemViewModel(child, !child.IsKnownLeaf()));
            }
            ChildrenLoaded = true;
        }

        public ILazyTreeItemBackingModel GetBackingModel()
        {
            return _model;
        }

        private class LazyTreeItemPlaceHolder : ILazyTreeItemBackingModel
        {
            public string GetId()
            {
                throw new NotImplementedException();
            }

            public string GetItemName()
            {
                return "Loading...";
            }

            public bool IsKnownLeaf()
            {
                return true;
            }

            public string GetMetadata()
            {
                throw new NotImplementedException();
            }
        }
    }
}
