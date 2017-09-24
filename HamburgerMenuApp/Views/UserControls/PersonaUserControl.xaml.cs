
using HamburgerMenuApp.ViewModels;
using System;

using System.Collections.Generic;

using System.IO;

using System.Linq;

using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;

using Windows.Foundation.Collections;

using Windows.UI.Xaml;

using Windows.UI.Xaml.Controls;

using Windows.UI.Xaml.Controls.Primitives;

using Windows.UI.Xaml.Data;

using Windows.UI.Xaml.Input;

using Windows.UI.Xaml.Media;

using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace HamburgerMenuApp.Views.UserControls
{
    public sealed partial class PersonaUserControl : UserControl
    {

         // Enumeracion de estados del formulario
    public enum AnimacionFormulario
        {
            Normal,
            Insertar,
            Editar
        }
       // public PersonasViewModel Persona { get { return this.DataContext as PersonasViewModel; } }
        private AnimacionFormulario _animacionFormulario;

        public PersonaUserControl()
        {
            this.InitializeComponent();
   
        }
        public AnimacionFormulario State
        {
            get { return _animacionFormulario; }

            set
            {
                if (_animacionFormulario != value && Enum.IsDefined(typeof(AnimacionFormulario), value))
                {
                    _animacionFormulario = value;

                    VisualStateManager.GoToState(this, value.ToString(), false);


                }
            }
            /*  
             *  Llame al método GoToState si está cambiando los Estados de un control que utiliza el VisualStateManager en su ControlTemplate. 
             *  Llame al método GoToElementState para cambiar Estados de un elemento fuera un ControlTemplate
             *  (por ejemplo, si utilizas un VisualStateManager en un UserControl o en un solo elemento).        
            
             *  he cambiado a false
             * public static bool GoToState(FrameworkElement control,	string stateName,	bool useTransitions)
             * control Type: System.Windows.FrameworkElement
             * The control to transition between states. 
             * stateName
             * Type: System.String The state to transition to.
             * useTransitions
             * Type: System.Boolean
             * true to use a VisualTransition object to transition between states; otherwise, false.
             * Return Value Type: System.Boolean
             * true if the control successfully transitioned to the new state; otherwise, false.
             */

        }
        //System.Windows.RoutedEventArgs e

        public void AnimacionFormPersona_click(object sender, RoutedEventArgs e)
        {
            AnimacionFormulario state;
            if (AnimacionFormulario.TryParse((sender as AppBarButton).Label.ToString(), out state))
            {
                State = state;
            }

        }
    }
}
