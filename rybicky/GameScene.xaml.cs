using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Threading;

namespace rybicky
{
    /// <summary>
    /// Interakční logika pro GameScene.xaml
    /// </summary>
    public partial class GameScene : UserControl
    {
        private DispatcherTimer gameTimer;

        public GameScene()
        {
            InitializeComponent();

            CreatePipe(800);

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }

        // changing fish color
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
        //                  *

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SceneContainer.Content = new Settings();
        }

        // Loading fish in the center of the screen
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PositionFish();
        }
        private void PositionFish()
        {
            if (GameCanvas.ActualHeight == 0) return;

            double scale = 0.3;
            double fishHeight = fish.Height * scale;

            double centerY = (GameCanvas.ActualHeight - fishHeight) / 2;

            Canvas.SetTop(fish, centerY);
            Canvas.SetLeft(fish, 20);
        }
        //                                       *  

        private void CreatePipe(double startX)
        {
            Rectangle wholePipe = new Rectangle
            {
                Width = 30,
                Height = 400,
                Fill = Brushes.RosyBrown
            };
            Grid.SetRow(wholePipe, 1);
            Canvas.SetLeft(wholePipe, startX);
            Canvas.SetTop(wholePipe, 0);
            
            if (GameCanvas.ActualHeight < 60) return;

            Random random = new Random();
            double gapY = random.Next(5, (int)(GameCanvas.ActualHeight - 5));
            Rectangle gap = new Rectangle
            {
                Width = 30,
                Height = 50,
                Fill = Brushes.FloralWhite,
            };
            Grid.SetRow(gap, 1);
            Canvas.SetLeft(gap, startX);
            Canvas.SetTop(gap, gapY);

            GameCanvas.Children.Add(wholePipe);
            GameCanvas.Children.Add(gap);

            pipes.Add((wholePipe, gap));
        }


        private double pipeSpeed = 2;
        private List<(Rectangle Top, Rectangle Gap)> pipes = new List<(Rectangle, Rectangle)>();

        private void GameLoop(object sender, EventArgs e)
        {
            List<(Rectangle Top, Rectangle Gap)> toRemove = new List<(Rectangle, Rectangle)>();

            // Move pipes
            foreach (var pipe in pipes)
            {
                double x = Canvas.GetLeft(pipe.Top);
                x -= pipeSpeed;

                Canvas.SetLeft(pipe.Top, x);
                Canvas.SetLeft(pipe.Gap, x);

                if (x + pipe.Top.Width < 0)
                {
                    toRemove.Add(pipe);
                }
            }

            // Remove off-screen pipes
            foreach (var pipe in toRemove)
            {
                (pipe.Top.Parent as Canvas).Children.Remove(pipe.Top);
                (pipe.Gap.Parent as Canvas).Children.Remove(pipe.Gap);
                pipes.Remove(pipe);
            }

            // Add new pipes
            if (pipes.Count == 0 || Canvas.GetLeft(pipes.Last().Top) < 500)
            {
                CreatePipe(800);
            }
        }

        public void SetPipeSpeed(double speed)
        {
            pipeSpeed = speed;
        }
    }
}
