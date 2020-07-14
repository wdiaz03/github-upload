using ReactiveUI;
using ReactiveUIDemo.Models;
using ReactiveUIDemo.Views;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ReactiveUIDemo.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        private string _firstName = "Tim";
        private string _lastName;
        private readonly ObservableAsPropertyHelper<string> _fullName;
        private List<PersonModel> _people = new List<PersonModel>();
        private PersonModel _selectedItem;

        public string FullName
        {
            get { return _fullName.Value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                this.RaiseAndSetIfChanged(ref _firstName, value);
                Console.WriteLine(_firstName);
                Console.WriteLine(FullName);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                this.RaiseAndSetIfChanged(ref _lastName, value);
                Console.WriteLine(_lastName);
                Console.WriteLine(FullName);
            }
        }

        public List<PersonModel> People
        {
            get { return _people; }
            set { _people = value; }
        }

        public PersonModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedItem, value);  // Notify WPF. control
                Console.WriteLine($"{_selectedItem.FirstName} { _selectedItem.LastName }");
            }
        }

        public ReactiveCommand<Unit, Unit> ClearNames { get; }
        public ReactiveCommand<Unit, Unit> LoadPageOne { get; }
        public ReactiveCommand<Unit, Unit> LoadPageTwo { get; }


        //ROUTING
        public RoutingState Router { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoToFirstChild { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoToSecondChild { get; }

        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack { get; }

        public MainWindowViewModel()
        {
            Router = new RoutingState();

            // Router uses Splat.Locator to resolve views for
            // view models, so we need to register our views
            // using Locator.CurrentMutable.Register* methods.
            //
            // Instead of registering views manually, you 
            // can use custom IViewLocator implementation,
            // see "View Location" section for details.
            Locator.CurrentMutable.Register(() => new FirstChildView(), typeof(IViewFor<FirstChildViewModel>));
            Locator.CurrentMutable.Register(() => new SecondChildView(), typeof(IViewFor<SecondChildViewModel>));

            // Manage the routing state. Use the Router.Navigate.Execute
            // command to navigate to different view models. 
            //
            // Note, that the Navigate.Execute method accepts an instance 
            // of a view model, this allows you to pass parameters to 
            // your view models, or to reuse existing view models.
            GoToFirstChild = ReactiveCommand.CreateFromObservable(() => Router.NavigateAndReset.Execute(new FirstChildViewModel()));

            GoToSecondChild = ReactiveCommand.CreateFromObservable(() => Router.NavigateAndReset.Execute(new SecondChildViewModel()));

            // You can also ask the router to go back. But the reactive commands Router must use Navigate insted of NavigateandReset
            GoBack = Router.NavigateBack;

            ClearNames = ReactiveCommand.Create(ClearTheNames, this
               .WhenAnyValue(x => x.FirstName, x => x.LastName, (firstName, lastName) => !string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName)));

            People.Add(new PersonModel { FirstName = "Tim", LastName = "Corey" });
            People.Add(new PersonModel { FirstName = "Bob", LastName = "Dole" });
            People.Add(new PersonModel { FirstName = "Sam", LastName = "Smith" });

            // property is now an Observable, it will yield a value each time firstname and lastname changes
            _fullName = this
                 .WhenAnyValue(x => x.FirstName, x => x.LastName, (firstName, lastName) => $"{firstName} {lastName}")
                .ToProperty(this, x => x.FullName);

        }

        private void ClearTheNames()
        {
            FirstName = "";
            LastName = "";
        }
    }
}

