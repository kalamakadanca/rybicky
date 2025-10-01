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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rybicky
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SwitchToMenu();
        }
        
        public void SwitchToGame(GameScene gamescene)
        {
            MainContent.Content = gamescene;
        }
        public void SwitchToMenu()
        {
            MainContent.Content = new MenuScene();
        }
        public void SwitchToPickAFish()
        {
            MainContent.Content = new PickAFish();
        }
    }
}
