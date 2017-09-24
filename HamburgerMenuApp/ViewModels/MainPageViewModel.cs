using HamburgerMenuApp.Models;
using HamburgerMenuApp.ViewModels.Base;
using HamburgerMenuApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HamburgerMenuApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _pageName;
        public string PageName
        {
            get { return _pageName; }
            set
            {
                _pageName = value;
                RaisePropertyChanged();
            }
        }

        private bool _isOpen = false;
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                _isOpen = value;
                RaisePropertyChanged();
            }
        }

        private Type _currentPage = typeof(Pagina1);
        public Type CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PageType> _menuList;
        public ObservableCollection<PageType> MenuList
        {
            get { return _menuList; }
            set
            {
                _menuList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PageType> _secondMenuList;
        public ObservableCollection<PageType> SecondMenuList
        {
            get { return _secondMenuList; }
            set
            {
                _secondMenuList = value;
                RaisePropertyChanged();
            }
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            LoadMenu();
            return null;
        }

        private void LoadMenu()
        {
            /*
             *      Los tag de los radiobutton son caracteres de la fuente Segoe MDL2 Assets que 
                    se agregaron con la llegada del SDK de UWP para facilitar las cosas. E
                    sta fuente provee caracteres que nos permiten agregar distintos iconos dentro 
                    de nuestra aplicación. Los podemos encontrar abriendo Mapa de caracteres y 
                    seleccionando la fuente Segoe MDL2 Assets. 
                    Tambien los icon
             */

            MenuList = new ObservableCollection<PageType>();
            //icon casa
            MenuList.Add(new PageType() { Name = "Datos Generales", Type = typeof(Pagina1), Icon = "" });
            //icon personas
            MenuList.Add(new PageType() { Name = "Personas", Type = typeof(PersonasView), Icon = "" });
            //icon centros de trabajo
            MenuList.Add(new PageType() { Name = "Centros de trabajo", Type = typeof(Pagina3), Icon = "" });

            SecondMenuList = new ObservableCollection<PageType>();
            SecondMenuList.Add(new PageType() { Name = "Pagina 4", Type = typeof(Pagina4), Icon = "" });
        }

        private DelegateCommand<PageType> _navigatePageCommand;
        public ICommand NavigatePageCommand
        {
            get { return _navigatePageCommand = _navigatePageCommand ?? new DelegateCommand<PageType>(NavigatePageCommandExecute); }
        }
        private void NavigatePageCommandExecute(PageType pagetype)
        {
            CurrentPage = pagetype.Type;
            PageName = pagetype.Name;
            IsOpen = false;
        }


        private DelegateCommand _checkedRadioButtonPaneCommand;
        public ICommand CheckedRadioButtonPaneCommand
        {
            get { return _checkedRadioButtonPaneCommand = _checkedRadioButtonPaneCommand ?? new DelegateCommand(CheckedRadioButtonPaneCommandExecute); }
        }
        private void CheckedRadioButtonPaneCommandExecute()
        {
            IsOpen = !IsOpen;
        }
        
    }
}
