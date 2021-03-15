﻿using EIADataViewer.ViewModel;
using PoliCommon.MVVM;
using System.Windows;
using System.Windows.Controls;

namespace EIADataViewer.View
{
    public partial class DatasetFinderView : UserControl
    {
        private DatasetFinderViewModel _viewModel => this.DataContext as DatasetFinderViewModel;

        public DatasetFinderView()
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
