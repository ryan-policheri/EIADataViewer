using DotNetCommon.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIADataViewer.ViewModel.Base
{
    public class RobustViewModelBase : ViewModelBase
    {
        private readonly RobustViewModelDependencies _facade;

        public RobustViewModelBase(RobustViewModelDependencies facade)
        {
            _facade = facade;
        }

        protected T Resolve<T>()
        {
            return (T)_facade.ServiceProvider.GetService(typeof(T));
        }
    }
}
