using ReactiveUI;
using ReactiveUIDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReactiveUIDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();

            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel,
                    viewModel => viewModel.FirstName,
                    view => view.firstNameTextBox.Text)
                   .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                    viewModel => viewModel.LastName,
                    view => view.lastNameTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    viewModel => viewModel.FullName,
                    view => view.fullNameTextBlock.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    vm => vm.People,
                    view => view.peopleComboBox.ItemsSource)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                    vm => vm.SelectedItem,
                    view => view.peopleComboBox.SelectedValue)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    vm => vm.SelectedItem.LastName,
                    view => view.cbLastNameTextBlock.Text)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                    viewModel => viewModel.ClearNames,
                    view => view.clearNamesBtn)
                    .DisposeWith(disposableRegistration);

                 this.BindCommand(ViewModel,
                     viewModel => viewModel.GoToSecondChild,
                     view => view.loadPageTwoBtn)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                    viewModel => viewModel.GoToFirstChild,
                    view => view.loadPageOneBtn)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                     viewModel => viewModel.GoBack,
                     view => view.goBackBtn)
                     .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                viewModel => viewModel.Router,
                view => view.RoutedViewHost.Router)
                .DisposeWith(disposableRegistration);
             });

        }
    }
}
