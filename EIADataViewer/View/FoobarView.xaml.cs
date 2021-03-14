using EIADataViewer.ViewModel;
using PoliCommon.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EIADataViewer.View
{
    /// <summary>
    /// Interaction logic for FoobarView.xaml
    /// </summary>
    public partial class FoobarView : UserControl
    {
        private FoobarViewModel _viewModel => this.DataContext as FoobarViewModel;

        public FoobarView()
        {
            InitializeComponent();
        }

        private async void LazyLoadedTree_Expanded(object sender, RoutedEventArgs args)
        {
            if(sender != null && args != null && args.OriginalSource != null && args.OriginalSource is TreeViewItem)
            {
                TreeViewItem treeItem = args.OriginalSource as TreeViewItem;
                if(treeItem != null)
                {
                    LazyTreeItemViewModel treeItemViewModel = treeItem.DataContext as LazyTreeItemViewModel;
                    if (treeItemViewModel != null)
                    {
                        await _viewModel.LoadChildrenAsync(treeItemViewModel);
                    }
                }
            }
        }
    }
}
