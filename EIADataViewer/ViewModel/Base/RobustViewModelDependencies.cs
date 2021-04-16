using System;
using DotNetCommon.EventAggregation;

namespace EIADataViewer.ViewModel.Base
{
    public class RobustViewModelDependencies
    {
        public RobustViewModelDependencies(IServiceProvider serviceProvider, IMessageHub messageHub)
        {
            ServiceProvider = serviceProvider;
            MessageHub = messageHub;
        }

        public IServiceProvider ServiceProvider { get; }

        public IMessageHub MessageHub { get; }
    }
}
