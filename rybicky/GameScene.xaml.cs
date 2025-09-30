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
    public partial class GameScene : UserControl
    {

        public GameScene()
        {
            InitializeComponent();

            this.KeyDown += GameScene_KeyDown;
            this.KeyUp += GameScene_KeyUp;

            // Initial pipe
            CreatePipe(800);

            CompositionTarget.Rendering += GameLoop;
        }

        private double fishVelocityY = 0;
        // moving fish up and down
        private void GameScene_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                fishVelocityY += 15;
            }
            else if (e.Key == Key.W)
            {
                fishVelocityY -= 15;
            }
        }
        private void GameScene_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S || e.Key == Key.W)
            {
                fishVelocityY = 0;
            }
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

            // focus on the game scene to capture key events
            this.Focusable = true;
            this.Focus();
            Keyboard.Focus(this);
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
            double gapHeight = 55;
            double gapY = random.Next(5, (int)(GameCanvas.ActualHeight - gapHeight));
            Rectangle gap = new Rectangle
            {
                Width = 30,
                Height = gapHeight,
                Fill = Brushes.FloralWhite,
            };
            Grid.SetRow(gap, 1);
            Canvas.SetLeft(gap, startX);
            Canvas.SetTop(gap, gapY);

            GameCanvas.Children.Add(wholePipe);
            GameCanvas.Children.Add(gap);

            pipes.Add((wholePipe, gap));
        }


        
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

            double fishTop = Canvas.GetTop(fish);
            Canvas.SetTop(fish, fishTop + fishVelocityY);
            fishVelocityY = 0;
        }

        private double pipeSpeed = 0.5;

        public void SetPipeSpeed(double speed)
        {
            pipeSpeed = speed;
        }
    }
}
