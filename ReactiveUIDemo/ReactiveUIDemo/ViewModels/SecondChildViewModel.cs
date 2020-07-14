using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUIDemo.ViewModels
{
    public class SecondChildViewModel : ReactiveObject, IRoutableViewModel
    {
        public SecondChildViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }

        public string UrlPathSegment => "second";

        public IScreen HostScreen { get; }
    }
}
