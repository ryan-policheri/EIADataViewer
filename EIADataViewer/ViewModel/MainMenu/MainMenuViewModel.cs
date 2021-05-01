using EIADataViewer.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIADataViewer.ViewModel.MainMenu
{
    public class MainMenuViewModel : RobustViewModelBase
    {
        public MainMenuViewModel(RobustViewModelDependencies facade) : base(facade)
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>();
            BuildMenuItems();
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        private void BuildMenuItems()
        {
            MenuItemViewModel file = new MenuItemViewModel("File", null);
            file.Children.Add(new MenuItemViewModel("Foo", file));
            MenuItemViewModel edit = new MenuItemViewModel("Edit", null);
            MenuItemViewModel settings = new MenuItemViewModel("Settings", null);
            MenuItems.Add(file);
            MenuItems.Add(edit);
            MenuItems.Add(settings);
        }
    }
}
