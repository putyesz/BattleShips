using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TothBence_torpedo
{
    /// <summary>
    /// Interaction logic for ShipSetup.xaml
    /// </summary>
    public partial class ShipSetup : Window
    {
        private int _length;
        private int _placedShips;

        private string _ship;

        private Player _p1;
        private Player _p2;

        private Path _selectedShip;
        private Path[] _ships;

        private Grid[,] _playerGrid;

        private bool _p1IsSet = false;

        public ShipSetup(string p1Name, string p2Name)
        {
            InitializeComponent();

            _p1 = new Player(p1Name);
            _p2 = new Player(p2Name);

            _playerGrid = new Grid[,]
            {
                { GA1, GB1, GC1, GD1, GE1, GF1, GG1, GH1, GI1, GJ1 },
                { GA2, GB2, GC2, GD2, GE2, GF2, GG2, GH2, GI2, GJ2 },
                { GA3, GB3, GC3, GD3, GE3, GF3, GG3, GH3, GI3, GJ3 },
                { GA4, GB4, GC4, GD4, GE4, GF4, GG4, GH4, GI4, GJ4 },
                { GA5, GB5, GC5, GD5, GE5, GF5, GG5, GH5, GI5, GJ5 },
                { GA6, GB6, GC6, GD6, GE6, GF6, GG6, GH6, GI6, GJ6 },
                { GA7, GB7, GC7, GD7, GE7, GF7, GG7, GH7, GI7, GJ7 },
                { GA8, GB8, GC8, GD8, GE8, GF8, GG8, GH8, GI8, GJ8 },
                { GA9, GB9, GC9, GD9, GE9, GF9, GG9, GH9, GI9, GJ9 },
                { GA10, GB10, GC10, GD10, GE10, GF10, GG10, GH10, GI10, GJ10 }
            };
            _ships = new Path[] { Carrier, Battleship, Cruiser, Submarine, Destroyer };

            Reset();
        }

        private void Reset()
        {
            _placedShips = 0;
            _selectedShip = null;

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    _playerGrid[i, j].Tag = "0";
                    _playerGrid[i, j].Background = new SolidColorBrush(Colors.White);
                }

            foreach (var item in _ships)
            {
                item.IsEnabled = true;
                if (item.Stroke != new SolidColorBrush(Colors.Black))
                {
                    item.Stroke = new SolidColorBrush(Colors.Black);
                }
            }
        }

        private void RandomAIAllas()
        {
            Reset();

            while (_placedShips != 5)
            {
                //horizontal vagy vertical
                int i = new Random().Next(0, 11);
                int j = new Random().Next(0, 11);
                if (new Random().Next(0, 2) == 0 && CanPlaceShip(i, j, Orientation.Horizontal))
                {
                    PlaceShip(i, j, Orientation.Horizontal);
                    _placedShips++;
                }
                else if (CanPlaceShip(i, j, Orientation.Vertical))
                {
                    PlaceShip(i, j, Orientation.Vertical);
                    _placedShips++;
                }
            }

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    _p2.Table[i, j] = _playerGrid[i, j].Tag.ToString();
                }
        }

        private void PlaceShip(int indexx, int indexy, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                //sorvége és eleje
                if (((_length - 1) > ((indexx + _length - 1) % 10)))
                {
                    indexx -= (indexx + _length) % 10;
                }

                for (int i = 0; i < _length; i++)
                {
                    _playerGrid[indexx + i, indexy].Tag = _ship;
                    _playerGrid[indexx + i, indexy].Background = new SolidColorBrush(Colors.Green);
                }
            }
            else
            {
                //oszlopvége és eleje
                if (((_length - 1) > (indexy + _length - 1 % 10)))
                {
                    indexy -= (indexy + _length) % 10;
                }

                for (int i = 0; i < _length; i++)
                {
                    _playerGrid[indexx, indexy + i].Tag = _ship;
                    _playerGrid[indexx, indexy + i].Background = new SolidColorBrush(Colors.Green);
                }
            }
        }

        private bool CanPlaceShip(int indexx, int indexy, Orientation orientation)
        {
            int backwardCounter, forwardCounter;
            if (orientation == Orientation.Horizontal)
            {
                try
                {
                    backwardCounter = 0;
                    forwardCounter = 0;

                    for (int i = 0; (backwardCounter + forwardCounter) < _length; i++)
                    {
                        if (_playerGrid[indexx + i, indexy].Tag.Equals("0"))
                        {
                            forwardCounter++;
                            continue;
                        }
                        else if ((_playerGrid[indexx - i, indexy].Tag.Equals("0")))
                        {
                            backwardCounter++;
                        }
                    }

                    //Console.WriteLine(backwardCounter + "  " + forwardCounter);

                    //van-e elég hely
                    if (forwardCounter + backwardCounter < _length)
                    {
                        throw new IndexOutOfRangeException("Invalid placement!");
                    }

                    indexx -= backwardCounter;

                    return true;

                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot place ship here");
                    return false;
                }
            }
            else
            {
                try
                {
                    backwardCounter = 0;
                    forwardCounter = 0;

                    for (int i = 0; (backwardCounter + forwardCounter) < _length; i++)
                    {
                        if (_playerGrid[indexx, indexy + i].Tag.Equals("0"))
                        {
                            forwardCounter++;
                            continue;
                        }
                        else if ((_playerGrid[indexx, indexy - i].Tag.Equals("0")))
                        {
                            backwardCounter++;
                        }
                    }

                    //van-e elég hely
                    if (forwardCounter + backwardCounter < _length)
                    {
                        throw new IndexOutOfRangeException("Invalid placement!");
                    }

                    indexy -= (backwardCounter * 10);

                    return true;

                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot place ship here");
                    return false;
                }
            }
        }

        private void Done_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_p2.Name == "AI")
            {
                if (_placedShips == 5)
                {
                    for (int i = 0; i < 10; i++)
                        for (int j = 0; j < 10; j++)
                        {
                            _p1.Table[i, j] = _playerGrid[i, j].Tag.ToString();
                        }
                    RandomAIAllas();
                }
                else
                {
                    MessageBox.Show("Not all ship is placed");
                    return;
                }
            }
            else if (_placedShips == 5 && !_p1IsSet)
            {
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                    {
                        _p1.Table[i, j] = _playerGrid[i, j].Tag.ToString();
                    }
                Reset();
                _p1IsSet = true;
                return;
            }
            else if (_placedShips == 5 && _p1IsSet)
            {
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                    {
                        _p2.Table[i, j] = _playerGrid[i, j].Tag.ToString();
                    }
            }
            else
            {
                MessageBox.Show("Not all ship is placed");
                return;
            }
            Game game = new Game(_p1, _p2);
            game.Show();
            this.Close();
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid senderGrid = (Grid)sender;

            if (_selectedShip == null)
            {
                MessageBox.Show("Please select a ship!");
                return;
            }

            if (senderGrid.Tag.Equals("ship"))
            {
                MessageBox.Show("Cannot place ship here");
                return;
            }

            int indexx = Array.IndexOf(_playerGrid, senderGrid) % 10;
            int indexy = Array.IndexOf(_playerGrid, senderGrid) - indexx;

            Console.WriteLine(indexx + "  " + "\n");

            int/* backwardCounter, */forwardCounter;

            if (OrientationHorizonatal.IsChecked == true)
            {
                try
                {
                    //backwardCounter = 0;
                    forwardCounter = 0;

                    for (int i = 0; (/*backwardCounter +*/ forwardCounter) < _length; i++)
                    {
                        if (_playerGrid[indexx + i, indexy].Tag.Equals("0"))
                        {
                            forwardCounter++;
                            //index++;
                            continue;
                        }
                        if ((_playerGrid[indexx - i, indexy].Tag.Equals("0")))
                        {
                            //index--;
                            //backwardCounter++;
                            continue;
                        }
                        throw new IndexOutOfRangeException("Invalid placement!");
                    }

                    Console.WriteLine(forwardCounter);

                    //van-e elég hely
                    if (forwardCounter < _length)
                    {
                        throw new IndexOutOfRangeException("Invalid placement!");
                    }

                    //index -= backwardCounter;

                    //sorvége és eleje
                    if (((_length - 1) > ((indexx + _length - 1) % 10)))
                    {
                        indexx -= (indexx + _length) % 10;
                    }

                    for (int i = 0; i < _length; i++)
                    {
                        _playerGrid[indexx + i, indexy].Tag = _ship;
                        _playerGrid[indexx + i, indexy].Background = new SolidColorBrush(Colors.Green);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot place ship here");
                    return;
                }
            }
            else
            {
                try
                {
                    //backwardCounter = 0;
                    forwardCounter = 0;

                    for (int i = 0; (/*backwardCounter + */forwardCounter) < _length; i++)
                    {
                        if (_playerGrid[indexx, indexy + i].Tag.Equals("0"))
                        {
                            forwardCounter++;
                            //index += 10;
                            continue;
                        }
                        if ((_playerGrid[indexx, indexy - i].Tag.Equals("0")))
                        {
                            //index -= 10;
                            //i--;
                            //backwardCounter++;
                            continue;
                        }
                        throw new IndexOutOfRangeException("Invalid placement!");
                    }

                    Console.WriteLine(/*backwardCounter + "  " +*/ forwardCounter);

                    //van-e elég hely
                    if (forwardCounter/* + backwardCounter*/ < _length)
                    {
                        throw new IndexOutOfRangeException("Invalid placement!");
                    }

                    //index -= ((backwardCounter) * 10);

                    //sorvége és eleje
                    if ((_length - 1) > (indexy + _length - 1 ))
                    {
                        indexy -= (indexy + _length) % 10;
                    }

                    for (int i = 0; i < _length; i++)
                    {
                        _playerGrid[indexx, indexy + i].Tag = _ship;
                        _playerGrid[indexx, indexy + i].Background = new SolidColorBrush(Colors.Green);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot place ship here");
                    return;
                }
            }

            _selectedShip.IsEnabled = false;
            _selectedShip.Stroke = new SolidColorBrush(Colors.Gray);
            _selectedShip = null;
            _placedShips++;
        }

        private void Ship_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path senderShip = (Path)sender;

            if (!senderShip.IsEnabled)
            {
                return;
            }
            else if (_selectedShip != null)
            {
                _selectedShip.Stroke = new SolidColorBrush(Colors.Black);
            }

            senderShip.Stroke = new SolidColorBrush(Colors.Azure);
            _selectedShip = senderShip;
            _ship = senderShip.Name;

            switch (_ship)
            {
                case "Carrier":
                    _length = 5;
                    break;
                case "Battleship":
                    _length = 4;
                    break;
                case "Cruiser":
                case "Submarine":
                    _length = 3;
                    break;
                case "Destroyer":
                    _length = 2;
                    break;
            }
        }
    }
}
