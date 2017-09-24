using HamburgerMenuApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HamburgerMenuApp.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private Frame appFrame;
        public Frame AppFrame
        {
            get { return appFrame; }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged();
            }
        }

        public abstract Task OnNavigatedFrom(NavigationEventArgs args);
        public abstract Task OnNavigatedTo(NavigationEventArgs args);


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var Handler = PropertyChanged;
            if (Handler != null)
                Handler(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void SetAppFrame(Frame viewFrame)
        {
            appFrame = viewFrame;
        }
    }
}
