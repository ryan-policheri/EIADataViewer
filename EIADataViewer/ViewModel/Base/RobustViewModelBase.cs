using DotNetCommon.EventAggregation;
using DotNetCommon.MVVM;

namespace EIADataViewer.ViewModel.Base
{
    public class RobustViewModelBase : ViewModelBase
    {
        private readonly RobustViewModelDependencies _facade;

        public RobustViewModelBase(RobustViewModelDependencies facade)
        {
            _facade = facade;
        }

        protected IMessageHub MessageHub => _facade.MessageHub;

        protected T Resolve<T>()
        {
            return (T)_facade.ServiceProvider.GetService(typeof(T));
        }
    }
}
