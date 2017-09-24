using HamburgerMenuApp.Services.PersonaService;
using HamburgerMenuApp.Services.TallaService;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerMenuApp.ViewModels.Base
{
    public class ViewModelLocator
    {
        /*
         *Intro
In MVVM the usual practice is to have the Views find their ViewModels by resolving them from a dependency injection (DI) container. This happens automatically when the container is asked to provide (resolve) an instance of the View class. The container injects the ViewModel into the View by calling a constructor of the View which accepts a ViewModel parameter; this scheme is called inversion of control (IoC).
Benefits of DI
The main benefit here is that the container can be configured at run time with instructions on how to resolve the types that we request from it. This allows for greater testability by instructing it to resolve the types (Views and ViewModels) we use when our application actually runs, but instructing it differently when running the unit tests for the application. In the latter case the application will not even have a UI (it's not running; just the tests are) so the container will resolve mocks in place of the "normal" types used when the application runs.
Problems stemming from DI
So far we have seen that the DI approach allows easy testability for the application by adding an abstraction layer over the creation of application components. There is one problem with this approach: it doesn't play well with visual designers such as Microsoft Expression Blend.
The problem is that in both normal application runs and unit test runs, someone has to set up the container with instructions on what types to resolve; additionally, someone has to ask the container to resolve the Views so that the ViewModels can be injected into them.
However, in design time there is no code of ours running. The designer attempts to use reflection to create instances of our Views, which means that:
If the View constructor requires a ViewModel instance the designer won't be able to instantiate the View at all -- it will error out in some controlled manner
If the View has a parameterless constructor the View will be instantiated, but its DataContext will be null so we 'll get an "empty" view in the designer -- which is not very useful
Enter ViewModelLocator
The ViewModelLocator is an additional abstraction used like this:
The View itself instantiates a ViewModelLocator as part of its resources and databinds its DataContext to the ViewModel property of the locator
The locator somehow detects if we are in design mode
If not in design mode, the locator returns a ViewModel that it resolves from the DI container, as explained above
If in design mode, the locator returns a fixed "dummy" ViewModel using its own logic (remember: there is no container in design time!); this ViewModel typically comes prepopulated with dummy data
Of course this means that the View must have a parameterless constructor to begin with (otherwise the designer won't be able to instantiate it).
Summary
ViewModelLocator is an idiom that lets you keep the benefits of DI in your MVVM application while also allowing your code to play well with visual designers. This is sometimes called the "blendability" of your application (referring to Expression Blend).
After digesting the above, see a practical example here.
Finally, using data templates is not an alternative to using ViewModelLocator, but an alternative to using explicit View/ViewModel pairs for parts of your UI. Often you may find that there's no need to define a View for a ViewModel because you can use a data template instead. 
         */
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<MainPageViewModel>();
            _container.RegisterType<Pagina1ViewModel>();
            _container.RegisterType<Pagina2ViewModel>();
            _container.RegisterType<Pagina3ViewModel>();
            _container.RegisterType<Pagina4ViewModel>();
            _container.RegisterType<PersonasViewModel>();
            //Esto es necesario para los viewmodel que dependen de interfaz, ni puta idea de porque, sacado ejemplo pero va
            _container.RegisterType<IPersonaService, b_PersonaService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ITallaService, bx_TallaService>(new ContainerControlledLifetimeManager());
        }

        public MainPageViewModel MainPageViewModel
        {
            get { return _container.Resolve<MainPageViewModel>(); }
        }
        public Pagina1ViewModel Pagina1ViewModel
        {
            get { return _container.Resolve<Pagina1ViewModel>(); }
        }
        public Pagina2ViewModel Pagina2ViewModel
        {
            get { return _container.Resolve<Pagina2ViewModel>(); }
        }
        public Pagina3ViewModel Pagina3ViewModel
        {
            get { return _container.Resolve<Pagina3ViewModel>(); }
        }
        public Pagina4ViewModel Pagina4ViewModel
        {
            get { return _container.Resolve<Pagina4ViewModel>(); }
        }

        public PersonasViewModel PersonasViewModel
        {
            get { return _container.Resolve<PersonasViewModel>(); }
        }
    }
}
