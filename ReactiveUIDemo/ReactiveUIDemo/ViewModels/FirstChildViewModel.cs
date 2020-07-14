using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUIDemo.ViewModels
{
    public class FirstChildViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "first";

        public IScreen HostScreen { get; }

        public FirstChildViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
