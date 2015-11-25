using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DeepMiningInc.App.Views;
using Template10.Mvvm;

namespace DeepMiningInc.App.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand GoToGamePageCommand { get; }

        public MainPageViewModel()
        {
            GoToGamePageCommand = new DelegateCommand(GoToGamePage);
        }

        private void GoToGamePage()
        {
            NavigationService.Navigate(typeof(GamePage));
        }
    }
}
