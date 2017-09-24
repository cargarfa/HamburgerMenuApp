using HamburgerMenuApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace HamburgerMenuApp.ViewModels
{
    public class Pagina4ViewModel : ViewModelBase
    {
        private MainPageViewModel _mainViewModel;

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            _mainViewModel = (MainPageViewModel)AppFrame.DataContext;
            return null;
        }
    }
}
