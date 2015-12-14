using Windows.UI.ViewManagement;

using DeepMiningInc.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace DeepMiningInc.App.Views
{
    /// <summary>
    /// The main game page
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GameViewModel ViewModel { get; }

        public GamePage()
        {
            InitializeComponent();
            ViewModel = new GameViewModel();
            DataContext = ViewModel;
            Engine.Engine.Current.Attach(AnimatedCanvas);
            Engine.Engine.Current.StartOrResume();
        }
    }
}
