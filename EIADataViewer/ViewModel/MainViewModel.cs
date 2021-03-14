﻿using System.Threading.Tasks;

namespace EIADataViewer.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel(FoobarViewModel foobarViewModel)
        {
            FoobarViewModel = foobarViewModel;
        }

        public FoobarViewModel FoobarViewModel { get; }

        public async Task LoadAsync()
        {
            await FoobarViewModel.LoadAsync();
        }
    }
}