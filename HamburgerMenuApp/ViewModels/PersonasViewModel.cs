using HamburgerMenuApp.Models;
using HamburgerMenuApp.Services.PersonaService;
using HamburgerMenuApp.Services.TallaService;
using HamburgerMenuApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using System.Data;
using Microsoft.Toolkit.Uwp;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI;
using HamburgerMenuApp.Views;
using Windows.UI.Xaml;
using static HamburgerMenuApp.Views.UserControls.PersonaUserControl;
using HamburgerMenuApp.Views.UserControls;

namespace HamburgerMenuApp.ViewModels
{
    public class PersonasViewModel : ViewModelBase
    {
        #region behaviors

        /*
         *Comportamiento behaviors 
         */
        //public VisualStateChangedEventHandler AdaptiveStates_OnCurrentStateChanged(Object sender, VisualStateChangedEventArgs e)
        //{
        //}
        /*
         *Step Three: Bind the Function with x:Bind
MainPage.xaml
    <VisualStateGroup x:Name="AdaptiveStates" CurrentStateChanged="{x:Bind MainPageViewModel.AdaptiveStatesOnCurrentStateChanged}">
I probably spent too much time figuring that out. I hope that this at least will help someone else who is stuck with the same problem 
         */
        //public enum ViewModelPersonasState
        //{
        //    Default,
        //    Insertar,
        //    Editar
        //}
        //private ViewModelPersonasState currentState;
        //public ViewModelPersonasState CurrentState
        //{
        //    get { return currentState; }
        //    set
        //    {
        //        currentState= value;
        //        RaisePropertyChanged();
        //    }
        //}
        //private void estadoVisual_click(object sender, RoutedEventArgs e)
        //{
        //    ViewModelPersonasState state;
        //    if (ViewModelPersonasState.TryParse((sender as Button).Content.ToString(), out state))
        //    {
        //        State = state;
        //    }

        //}

        //public RelayCommand GotoDetailsStateCommand { get; set; }
        //public RelayCommand GotoDefaultStateCommand { get; set; }
        #endregion
        //VAriable para hacer la navegacion
        private MainPageViewModel _mainViewModel;
        private PersonaUserControl _personaUserControl;

        #region Properties

        private IPersonaService personaService;
        private ITallaService tallaService;

        private b_Persona auxPersona = new b_Persona();
        public b_Persona AuxPersona
        {
            get { return auxPersona; }
            set
            {
                auxPersona = value;

                //RefreshListTalla();
                // CvsTallaListPorId = from talla in TallaList where talla.TallaPersonaId == auxPersona.PersonaId select talla;

                if (auxPersona != null)
                {
                    RefreshListTalla();
                    CvsTallaListPorId = from talla in TallaList where talla.TallaPersonaId == auxPersona.PersonaId select talla;
                }

                RaisePropertyChanged();
            }
        }
        private IEnumerable<bx_Talla> _cvsTallaListPorId;
        public IEnumerable<bx_Talla> CvsTallaListPorId
        {
            get { return _cvsTallaListPorId; }
            set
            {
                _cvsTallaListPorId = value;
                RaisePropertyChanged();
            }
        }
        //private IEnumerable<IGrouping<string, Customer>> _collectionViewSource;
        //public IEnumerable<IGrouping<string, Customer>> CollectionViewSource
        //{
        //    get { return _collectionViewSource; }
        //    set
        //    {
        //        _collectionViewSource = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private bx_Talla auxTalla = new bx_Talla();
        public bx_Talla AuxTalla
        {
            get { return auxTalla; }
            set
            {
                auxTalla = value;
              //  if (value != null) SelectedPersonaComboBox = PersonaList.Where(cus => cus.PersonaId == AuxTalla.TallaPersonaId).FirstOrDefault();
                RaisePropertyChanged();
            }
        }

        //private Customer selectedCustomerComboBox = new Customer();
        //public Customer SelectedCustomerComboBox
        //{
        //    get { return selectedCustomerComboBox; }
        //    set
        //    {
        //        selectedCustomerComboBox = value;
        //        if (value != null) auxProject.ProjectCustomerId = selectedCustomerComboBox.CustomerId;
        //        RaisePropertyChanged();
        //    }
        //}

        #endregion

        #region Collections
        /*
         * ObservableCollection: Representa una colección de datos dinámicos que proporciona notificaciones cuando artículos
         * Haz añadidos, eliminado, o cuando se actualiza la lista completa. 
         */
        private ObservableCollection<b_Persona> personaList;
        public ObservableCollection<b_Persona> PersonaList
        {
            get { return personaList; }
            set
            {
                personaList = value;
                RaisePropertyChanged();
            }
        }
        //para el semantic zoon para poder agrupar
        //private ObservableCollection<IGrouping<string, b_Persona>> personaCollectionnViewSource;
        //public ObservableCollection<IGrouping<string, b_Persona>> PersonaCollectionViewSource
        //{
        //    get { return personaCollectionnViewSource; }
        //    set
        //    {
        //        personaCollectionnViewSource = value;
        //        RaisePropertyChanged();
        //    }
        //}


        private ObservableCollection<bx_Talla> tallaList;
        public ObservableCollection<bx_Talla> TallaList
        {
            get { return tallaList; }
            set
            {
                tallaList = value;

                RaisePropertyChanged();
            }
        }

        
        private ObservableCollection<TallaJoinPersona> tallaListJoinPersona;
        public ObservableCollection<TallaJoinPersona> TallaListJoinPersona
        {
            get { return tallaListJoinPersona; }
            set
            {
                tallaListJoinPersona = value;
     
                RaisePropertyChanged();
            }
        }
        //private ObservableCollection<IGrouping<string, bx_Talla>> tallacvs;
        //public ObservableCollection<IGrouping<string, bx_Talla>> Tallacvs
        //{
        //    get { return tallacvs; }
        //    set
        //    {
        //        tallacvs = value;
        //        RaisePropertyChanged();
        //    }
        //}

        #endregion
        #region FILTROS

        //private IOrderedEnumerable<IGrouping<string, bx_Talla>> tallaPorNombreTalla;
        //public IOrderedEnumerable<IGrouping<string,bx_Talla>> TallaPorNombreTalla
        //{
        //    get
        //    {
        //        if(this.tallaPorNombreTalla==null)
        //        {
        //            this.tallaPorNombreTalla = from bx_Talla in this.TallaList
        //                                       group bx_Talla by bx_Talla.NombreTalla into grp
        //                                       orderby grp.Key
        //                                       select grp;
        //        }
        //        return this.tallaPorNombreTalla;
        //    }
        //}

        //List<TallaJoinPersona> TallaLista;
        //void FiltroTallaPorId()
        //{
        //    ObservableCollection<TallaJoinPersona> TallaList = new ObservableCollection<TallaJoinPersona>();
        //    TallaLista filtroPersonaId = TallaList.Find(


        //    )
        //    ////for(var index = auxPersona.PersonaId; index >= 0; index--)
        //    ////{
        //    ////    if(TallaList.[]==)
        //    ////}
        //    ////Expresion linq a evaluar por el foreach
        //    //var sorted = from fTallaPorIdLinq in TallaJoinPersona
        //    //             where  TallaJoinPersona == auxPersona.PersonaId
        //    //             select fTallaPorIdLinq;

        //    //foreach (ObservableCollection item in sorted)
        //    //{
        //    //     fTallaPorId.Add(item);  
        //    //}
        //    var TallaPorPersonaId = new AdvancedCollectionView(TallaList);
        //    TallaPorPersonaId.Filter = x => x.TallaPersonaId = AuxPersona.PersonaId;
        //}
        /*The AdvancedCollectionView is a collection view implementation that support filtering,
      * sorting and incremental loading. It's meant to be used in a viewmodel.
      //*  
      //*/

        //                                     //where TallaList.TallaId == auxPersona.PersonaId
        //                                     //orderby TallaList.TallaId
        //                                     // select TallaList;


        //          //x => !int.TryParse(((tallaList)x).Name, out nul);
        //          //TallaPorPersonaId.SortDescriptions.Add(new SortDescription("Name", SortDirection.Ascending));


        //public ObservableCollection FiltroTalla()
        //{
        //    // Set up the AdvancedCollectionView to filter and sort the original list
        //    var acvTallaPorPersonaId = new AdvancedCollectionView(TallaList);
        //    // int nul;

        //    // TallaPorPersonaId.Filter = x => !int.TryParse(((tallaList)x)., out nul);

        //    acvTallaPorPersonaId.SortDescriptions.Add(new SortDescription("NombreTalla", SortDirection.Ascending));
        //    //RightList.ItemSource = acvTallaPorPersonaId;
        //}


        #endregion
        #region Methods
     



        public PersonasViewModel(IPersonaService personaService, ITallaService tallaService)
        {
            this.personaService = personaService;
            this.tallaService = tallaService;
        }
        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }
        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            RefreshListPersona();
          //  RefreshListPersonaGrupo();
            RefreshListTalla();
            //RefreshListTallaJoinPersona();
           // filtroTallaPorPersona();
            //DAta context hace falta para binding, no para x:bind 
            _mainViewModel = (MainPageViewModel)AppFrame.DataContext;
            _personaUserControl = new PersonaUserControl();
            return null;
        }
        private void RefreshListPersona()
        {
            PersonaList = new ObservableCollection<b_Persona>(personaService.GetPersona());
            ClearPersona();
        }
        //private void RefreshListPersonaGrupo()
        //{
        //    PersonaList = new ObservableCollection<b_Persona>(personaService.GetPersona());
        //    PersonaCollectionViewSource = new ObservableCollection<IGrouping<string, b_Persona>>(from per in PersonaList group per by per.Apellido1 into g orderby g.Key select g);
        //   // ClearPersona();
        //}
        private void ClearPersona()
        {
            AuxPersona = null;
            AuxPersona = new b_Persona();
        }
        private void RefreshListTallaJoinPersona()
        {
            //GetTallaPorPersonaId(int personaId)
            //     TallaListJoinPersona = new ObservableCollection<TallaJoinPersona>(tallaService.GetTallaPorPersonaId(int personaId));
            TallaListJoinPersona = new ObservableCollection<TallaJoinPersona>(tallaService.GetTallaJoinPersona());
            ClearTalla();
        }
        //private void RefreshListTallaJoinPersona(int auxPersonaId)
        //{
        //    TallaListJoinPersona = new ObservableCollection<TallaJoinPersona>(tallaService.GetTallaJoinPersona());
        //    ClearTalla();
        //}
        private void RefreshListTalla()
        {
            TallaList = new ObservableCollection<bx_Talla>(tallaService.GetTalla());
            ClearTalla();
        }
        private void ClearTalla()
        {
            AuxTalla = null;
            AuxTalla = new bx_Talla();
           // SelectedPersonaComboBox = null;
           // SelectedPersonaComboBox = new b_Persona();
        }



        #endregion
          
        public PersonasViewModel()
        {

        }

        #region ACCIONES
   

        #endregion

        #region Commands
        //semantic zoom
        //private DelegateCommand<SemanticZoomViewChangedEventArgs> viewChangeStartedCommand;
        //public ICommand ViewChangeStartedCommand
        //{
        //    get { return viewChangeStartedCommand = viewChangeStartedCommand ?? new DelegateCommand<SemanticZoomViewChangedEventArgs>(ViewChangeStartedCommandExecute); }
        //}
        //private void ViewChangeStartedCommandExecute(SemanticZoomViewChangedEventArgs e)
        //{
        //    if (e.SourceItem.Item != null)
        //    {
        //        e.DestinationItem = new SemanticZoomLocation { Item = e.SourceItem.Item };
        //    }
        //}












        private DelegateCommand insertOrUpdatePersonaButtonCommand;
        private DelegateCommand newPersonaButtonCommand;
        private DelegateCommand deletePersonaButtonCommand;
        private DelegateCommand insertOrUpdateTallaButtonCommand;
        private DelegateCommand newTallaButtonCommand;
        private DelegateCommand deleteTallaButtonCommand;

        public ICommand InsertOrUpdatePersonaButtonCommand
        {
            get { return insertOrUpdatePersonaButtonCommand = insertOrUpdatePersonaButtonCommand ?? new DelegateCommand(insertOrUpdatePersonaButtonCommandExecute); }
        }
        public ICommand NewPersonaButtonCommand
        {
            get { return newPersonaButtonCommand = newPersonaButtonCommand ?? new DelegateCommand(newPersonaButtonCommandExecute); }
        }
        public ICommand DeletePersonaButtonCommand
        {
            get { return deletePersonaButtonCommand = deletePersonaButtonCommand ?? new DelegateCommand(deletePersonaButtonCommandExecute); }
        }
        public ICommand InsertOrUpdateTallaButtonCommand
        {
            get { return insertOrUpdateTallaButtonCommand = insertOrUpdateTallaButtonCommand ?? new DelegateCommand(insertOrUpdateTallaButtonCommandExecute); }
        }
        public ICommand NewTallaButtonCommand
        {
            get { return newTallaButtonCommand = newTallaButtonCommand ?? new DelegateCommand(newTallaButtonCommandExecute); }
        }
        public ICommand DeleteTallaButtonCommand
        {
            get { return deleteTallaButtonCommand = deleteTallaButtonCommand ?? new DelegateCommand(deleteTallaButtonCommandExecute); }
        }



        private void insertOrUpdatePersonaButtonCommandExecute()
        {
            /*
             *1º- Vaciar de contenido el HText del HeadreTextBox
             *2º-Mostrar el textBox
             */
            //  PersonasView.nombrePersona.Visibility = "true";
            //   personaService.InsertOrUpdatePersona(auxPersona);
            // RefreshListPersona();
        }
        //public void AnimacionFormPersona_click(object sender, RoutedEventArgs e)
        //{
            
        //    AnimacionFormulario state;
        //    if (AnimacionFormulario.TryParse((sender as AppBarButton).Label.ToString(), out state))
        //    {
        //      //  AnimacionFormulario = state;
        //    }

        //}
        private void newPersonaButtonCommandExecute()
        {
    //        _personaUserControl.AnimacionFormPersona_click();


            ClearPersona();

        }
        private void deletePersonaButtonCommandExecute()
        {
            personaService.DeletePersona(AuxPersona);
            RefreshListPersona();
        }
        private void insertOrUpdateTallaButtonCommandExecute()
        {
            tallaService.InsertOrUpdateTalla(AuxTalla);
            RefreshListTalla();
        }
        private void newTallaButtonCommandExecute()
        {
            ClearTalla();
        }
        private void deleteTallaButtonCommandExecute()
        {
            tallaService.DeleteTalla(AuxTalla);
            RefreshListTalla();
        }
        #endregion



    }

}
   
    


