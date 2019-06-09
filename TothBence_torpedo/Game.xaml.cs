using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TothBence_torpedo
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        //összesen 17 mező
        List<Ship> ships = new List<Ship> {
            new Ship( 5, "Carrier" ),
            new Ship(4, "Battleship" ),
            new Ship( 3, "Cruiser" ),
            new Ship( 3, "Submarine" ),
            new Ship( 2, "Destroyer" )
        };

        private int _next;
        private int _turn = 0;
        private int _p1Point = 0;
        private int _p2Point = 0;
        private int _p1Hits = 0;
        private int _p2Hits = 0;
        private int _shootx, _shooty;

        private Grid[,] _p1Shots;
        private Grid[,] _p2Shots;

        private bool _isStartPressed = false;

        private Player _player1;
        private Player _player2;

        public Game(Player player1, Player player2)
        {
            InitializeComponent();
            _player1 = new Player(player1.Name);
            _player2 = new Player(player2.Name);

            UpdateLabels();

            _p1Shots = new Grid[,]
            {
                { P2GA1, P2GB1, P2GC1, P2GD1, P2GE1, P2GF1, P2GG1, P2GH1, P2GI1, P2GJ1 },
                { P2GA2, P2GB2, P2GC2, P2GD2, P2GE2, P2GF2, P2GG2, P2GH2, P2GI2, P2GJ2 },
                { P2GA3, P2GB3, P2GC3, P2GD3, P2GE3, P2GF3, P2GG3, P2GH3, P2GI3, P2GJ3 },
                { P2GA4, P2GB4, P2GC4, P2GD4, P2GE4, P2GF4, P2GG4, P2GH4, P2GI4, P2GJ4 },
                { P2GA5, P2GB5, P2GC5, P2GD5, P2GE5, P2GF5, P2GG5, P2GH5, P2GI5, P2GJ5 },
                { P2GA6, P2GB6, P2GC6, P2GD6, P2GE6, P2GF6, P2GG6, P2GH6, P2GI6, P2GJ6 },
                { P2GA7, P2GB7, P2GC7, P2GD7, P2GE7, P2GF7, P2GG7, P2GH7, P2GI7, P2GJ7 },
                { P2GA8, P2GB8, P2GC8, P2GD8, P2GE8, P2GF8, P2GG8, P2GH8, P2GI8, P2GJ8 },
                { P2GA9, P2GB9, P2GC9, P2GD9, P2GE9, P2GF9, P2GG9, P2GH9, P2GI9, P2GJ9 },
                { P2GA10, P2GB10, P2GC10, P2GD10, P2GE10, P2GF10, P2GG10, P2GH10, P2GI10, P2GJ10 }
            };
            _p2Shots = new Grid[,]
            {
                { P1GA1, P1GB1, P1GC1, P1GD1, P1GE1, P1GF1, P1GG1, P1GH1, P1GI1, P1GJ1 },
                { P1GA2, P1GB2, P1GC2, P1GD2, P1GE2, P1GF2, P1GG2, P1GH2, P1GI2, P1GJ2 },
                { P1GA3, P1GB3, P1GC3, P1GD3, P1GE3, P1GF3, P1GG3, P1GH3, P1GI3, P1GJ3 },
                { P1GA4, P1GB4, P1GC4, P1GD4, P1GE4, P1GF4, P1GG4, P1GH4, P1GI4, P1GJ4 },
                { P1GA5, P1GB5, P1GC5, P1GD5, P1GE5, P1GF5, P1GG5, P1GH5, P1GI5, P1GJ5 },
                { P1GA6, P1GB6, P1GC6, P1GD6, P1GE6, P1GF6, P1GG6, P1GH6, P1GI6, P1GJ6 },
                { P1GA7, P1GB7, P1GC7, P1GD7, P1GE7, P1GF7, P1GG7, P1GH7, P1GI7, P1GJ7 },
                { P1GA8, P1GB8, P1GC8, P1GD8, P1GE8, P1GF8, P1GG8, P1GH8, P1GI8, P1GJ8 },
                { P1GA9, P1GB9, P1GC9, P1GD9, P1GE9, P1GF9, P1GG9, P1GH9, P1GI9, P1GJ9 },
                { P1GA10, P1GB10, P1GC10, P1GD10, P1GE10, P1GF10, P1GG10, P1GH10, P1GI10, P1GJ10 }
            };
        }

        private void UpdateLabels()
        {
            var labelText = _player1.Name + " Score:";
            P1Label.Content = labelText;
            labelText = "Hits:" + _p1Hits;
            turns.Content = labelText;

            labelText = _player2.Name + " Score:";
            P2Label.Content = labelText;
            labelText = "Hits:" + _p2Hits;
            turns.Content = labelText;

            labelText = "Turn:" + _turn;
            turns.Content = labelText;

            P1Points.Content = _p1Point.ToString();
            P2Points.Content = _p2Point.ToString();
        }

        private void Round()
        {
            if (!_player2.Name.Equals("AI"))
            {
                if (_next == 0)
                {
                    if (_p1Shots[_shootx, _shooty].Tag.ToString().Equals("Hit") || _p1Shots[_shootx, _shooty].Tag.ToString().Equals("-1"))
                    {
                        return;
                    }
                    if (!_p1Shots[_shootx, _shooty].Tag.ToString().Equals("0"))
                    {
                        //talált
                        _p1Shots[_shootx, _shooty].Tag = "Hit";
                        _p1Shots[_shootx, _shooty].Background = new SolidColorBrush(Colors.Red);
                        _p1Point += 10;
                    }
                    else
                    {
                        //nem talált
                        _p1Shots[_shootx, _shooty].Tag = "-1";
                        _p1Shots[_shootx, _shooty].Background = new SolidColorBrush(Colors.Gray);
                    }
                }
                else
                {
                    if (_p2Shots[_shootx, _shooty].Tag.ToString().Equals("Hit") || _p2Shots[_shootx, _shooty].Tag.ToString().Equals("-1"))
                    {
                        return;
                    }
                    if (!_p2Shots[_shootx, _shooty].Tag.ToString().Equals("0"))
                    {
                        //talált
                        _p2Shots[_shootx, _shooty].Tag = "Hit";
                        _p2Shots[_shootx, _shooty].Background = new SolidColorBrush(Colors.Red);
                        _p2Point += 10;
                    }
                    else
                    {
                        //nem talált
                        _p2Shots[_shootx, _shooty].Background = new SolidColorBrush(Colors.Gray);
                        _p2Shots[_shootx, _shooty].Tag = "-1";
                    }
                }
            }
            else
            {
                if (_p1Shots[_shootx, _shooty].Tag.ToString().Equals("Hit") || _p1Shots[_shootx, _shooty].Tag.ToString().Equals("-1"))
                {
                    return;
                }
                if (!_p1Shots[_shootx, _shooty].Tag.ToString().Equals("0"))
                {
                    //talált
                    _p1Shots[_shootx, _shooty].Tag = "Hit";
                    _p1Shots[_shootx, _shooty].Background = new SolidColorBrush(Colors.Red);
                    _p1Point += 10;
                }
                else
                {
                    //nem talált
                    _p1Shots[_shootx, _shooty].Tag = "-1";
                    _p1Shots[_shootx, _shooty].Background = new SolidColorBrush(Colors.Gray);
                }
            }
            UpdateLabels();
        }

        private void AIRound()
        {
            do
            {
                _shootx = new Random().Next(0, 11);
                _shooty = new Random().Next(0, 11);
            } while (_p2Shots[_shootx, _shooty].Tag.ToString().Equals("-1") || _p2Shots[_shootx, _shooty].Tag.ToString().Equals("Hit"));

            if (!_p2Shots[_shootx, _shooty].Tag.ToString().Equals("0"))
            {
                //talált
                _p2Shots[_shootx, _shooty].Tag = "Hit";
                _p2Shots[_shootx, _shooty].Background = new SolidColorBrush(Colors.Red);
                _p2Point += 10;
                Floodfill(_shootx, _shooty);
            }
            else
            {
                //nem talált
                _p2Shots[_shootx, _shooty].Background = new SolidColorBrush(Colors.Gray);
                _p2Shots[_shootx, _shooty].Tag = "-1";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_isStartPressed)
            {
                _isStartPressed = true;

                StartButton.Opacity = 0;
                NextButton.Opacity = 1;

                _next = new Random().Next(0, 2);
                string _starterPlayer = _player1.Name;
                if (_next == 1)
                {
                    _starterPlayer = _player2.Name;
                    MessageBox.Show(_starterPlayer + " kezd");
                    AIRound();
                }
                else
                {
                    MessageBox.Show(_starterPlayer + " kezd");
                    Round();
                }
            }
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            _next = 1 - _next;
            turns.Content = "Turn:" + _turn;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid senderGrid = (Grid)sender;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (_player2.Name == "AI" && e.Key == Key.L)
            {
                ShowAI();
            }
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (_player2.Name == "AI" && e.Key == Key.L)
            {
                HideAI();
            }
        }

        private void ShowAI()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    if (!_p1Shots[i, j].Tag.ToString().Equals("0") && !_p1Shots[i, j].Tag.ToString().Equals("-1"))
                    {
                        _p1Shots[i, j].Background = new SolidColorBrush(Colors.Green);
                    }
                }
        }

        private void HideAI()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    if (_p1Shots[i, j].Tag.ToString().Equals("Hit"))
                    {
                        _p1Shots[i, j].Background = new SolidColorBrush(Colors.Red);
                    }
                    else if (!_p1Shots[i, j].Tag.ToString().Equals("0") && !_p1Shots[i, j].Tag.ToString().Equals("-1"))
                    {
                        _p1Shots[i, j].Background = new SolidColorBrush(Colors.White);
                    }
                }
        }

        private void GameOver(string _winner)
        {
            MessageBox.Show(_winner + " won!");
        }
    }
}
