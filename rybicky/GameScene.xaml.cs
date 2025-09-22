using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interakční logika pro GameScene.xaml
    /// </summary>
    public partial class GameScene : UserControl
    {
        public GameScene()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).SwitchToMenu();
        }
        public Brush FishBodyColor
        {
            get { return body.Fill; }
            set { body.Fill = value; }
        }
        public Brush FishTailColor
        {
            get { return tail.Fill; }
            set { tail.Fill = value; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
