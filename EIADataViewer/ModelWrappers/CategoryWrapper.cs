using EIA.Domain.Model;
using PoliCommon.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIADataViewer.ModelWrappers
{
    public class CategoryWrapper : Category, ILazyTreeItemBackingModel
    {
        private readonly Category _category;

        public CategoryWrapper(Category category)
        {
            _category = category;
            this.Children = new ObservableCollection<CategoryWrapper>();
            if(_category.ChildCategories != null)
            {
                foreach (Category child in _category.ChildCategories)
                {
                    Children.Add(new CategoryWrapper(child));
                }
            }
        }

        public string GetId()
        {
            return _category.CategoryId.ToString();
        }

        public string GetItemName()
        {
            return _category.CategoryName;
        }

        public ObservableCollection<CategoryWrapper> Children { get; set; }
    }
}
