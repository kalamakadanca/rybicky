using System;
using System.Collections.Generic;
using System.Linq;
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

namespace rybicky
{
    /// <summary>
    /// Interakční logika pro PickAFish.xaml
    /// </summary>
    public partial class PickAFish : UserControl
    {
        public PickAFish()
        {
            InitializeComponent();
        }
        

        private void start_game_Click(object sender, RoutedEventArgs e)
        {
            var gamescene = new GameScene();
            gamescene.FishBodyColor = selectedBodyColor;
            gamescene.FishTailColor = selectedTailColor;
            (Application.Current.MainWindow as MainWindow).SwitchToGame(gamescene);
        }
        private void back_to_menu_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).SwitchToMenu();
        }

        private Brush selectedBodyColor;
        private Brush selectedTailColor;

        private void PinkFish_Click(object sender, RoutedEventArgs e)
        {
            selectedBodyColor = Brushes.Orchid;
            selectedTailColor = Brushes.Orchid;

            start_game.Visibility = Visibility.Visible;

            pink_border.BorderBrush = Brushes.DeepPink;
            orange_border.BorderBrush = Brushes.Transparent;
            green_border.BorderBrush = Brushes.Transparent;
        }
        private void OrangeFish_Click(object sender, RoutedEventArgs e)
        {
            selectedBodyColor = Brushes.DarkOrange;
            selectedTailColor = Brushes.DarkOrange;

            start_game.Visibility = Visibility.Visible;

            orange_border.BorderBrush = Brushes.DarkOrange;
            green_border.BorderBrush = Brushes.Transparent;
            pink_border.BorderBrush = Brushes.Transparent;
        }
        private void GreenFish_Click(object sender, RoutedEventArgs e)
        {
            selectedBodyColor = Brushes.DarkOliveGreen;
            selectedTailColor = Brushes.DarkOliveGreen;

            start_game.Visibility = Visibility.Visible;

            green_border.BorderBrush = Brushes.DarkOliveGreen;
            orange_border.BorderBrush = Brushes.Transparent;
            pink_border.BorderBrush = Brushes.Transparent;

        }
    }
}
