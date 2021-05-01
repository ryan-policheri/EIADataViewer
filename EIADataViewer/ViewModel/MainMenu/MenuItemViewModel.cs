using DotNetCommon.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIADataViewer.ViewModel.MainMenu
{
    public class MenuItemViewModel : ViewModelBase
    {
        public MenuItemViewModel(string header, MenuItemViewModel parent)
        {
            Header = header;
            Parent = parent;
            Children = new ObservableCollection<MenuItemViewModel>();
        }

        public string Header { get; }

        public MenuItemViewModel Parent { get; }

        public ObservableCollection<MenuItemViewModel> Children { get; }
    }
}
