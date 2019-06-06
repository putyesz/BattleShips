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

        private Grid[] _playerGrid;

        private bool _p1IsSet = false;

        public ShipSetup(string p1Name, string p2Name)
        {
            InitializeComponent();
            
            _p1 = new Player(p1Name);
            _p2 = new Player(p2Name);

            _playerGrid = new Grid[]
            {
                GA1, GB1, GC1, GD1, GE1, GF1, GG1, GH1, GI1, GJ1,
                GA2, GB2, GC2, GD2, GE2, GF2, GG2, GH2, GI2, GJ2,
                GA3, GB3, GC3, GD3, GE3, GF3, GG3, GH3, GI3, GJ3,
                GA4, GB4, GC4, GD4, GE4, GF4, GG4, GH4, GI4, GJ4,
                GA5, GB5, GC5, GD5, GE5, GF5, GG5, GH5, GI5, GJ5,
                GA6, GB6, GC6, GD6, GE6, GF6, GG6, GH6, GI6, GJ6,
                GA7, GB7, GC7, GD7, GE7, GF7, GG7, GH7, GI7, GJ7,
                GA8, GB8, GC8, GD8, GE8, GF8, GG8, GH8, GI8, GJ8,
                GA9, GB9, GC9, GD9, GE9, GF9, GG9, GH9, GI9, GJ9,
                GA10, GB10, GC10, GD10, GE10, GF10, GG10, GH10, GI10, GJ10
            };
            _ships = new Path[] { Carrier, Battleship, Cruiser, Submarine, Destroyer };

            Reset();
        }

        private void Reset()
        {
            _placedShips = 0;
            _selectedShip = null;

            for (int i = 0; i < 100; i++)
            {
                _playerGrid[i].Tag = "0";
                _playerGrid[i].Background = new SolidColorBrush(Colors.White);
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
                int i = new Random().Next(0, 101);
                if (new Random().Next(0, 2) == 0 && CanPlaceShip(i, Orientation.Horizontal))
                {
                    PlaceShip(i, Orientation.Horizontal);
                    _placedShips++;
                }
                else if (CanPlaceShip(i, Orientation.Vertical))
                {
                    PlaceShip(i, Orientation.Vertical);
                    _placedShips++;
                }
            }

            for (int i = 0; i < 100; i++)
            {
                _p2.Table[i] = _playerGrid[i].Tag.ToString();
            }
        }

        private void PlaceShip(int index, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                //sorvége és eleje
                if (((_length - 1) > ((index + _length - 1) % 10)))
                {
                    index -= (index + _length) % 10;
                }

                for (int i = 0; i < _length; i++)
                {
                    _playerGrid[index + i].Tag = _ship;
                    _playerGrid[index + i].Background = new SolidColorBrush(Colors.Green);
                }
            }
            else
            {
                //oszlopvége és eleje
                if (((_length - 1) > (index + ((_length - 1) * 10))))
                {
                    index -= ((index + _length) * 10);
                }

                for (int i = 0; i < _length; i++)
                {
                    _playerGrid[index + (i * 10)].Tag = _ship;
                    _playerGrid[index + (i * 10)].Background = new SolidColorBrush(Colors.Green);
                }       
            }
        }

        private bool CanPlaceShip(int index, Orientation orientation)
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
                        if (index + i < 100 && (_playerGrid[index + i].Tag.Equals("0")))
                        {
                            forwardCounter++;
                            continue;
                        }
                        else if ((_playerGrid[index - i].Tag.Equals("0")))
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

                    index -= backwardCounter;

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
                        if (index + (i * 10) < 100 && (_playerGrid[index + (i * 10)].Tag.Equals("0")))
                        {
                            forwardCounter++;
                            continue;
                        }
                        else if ((_playerGrid[index - (i * 10)].Tag.Equals("0")))
                        {
                            backwardCounter++;
                        }
                    }

                    //van-e elég hely
                    if (forwardCounter + backwardCounter < _length)
                    {
                        throw new IndexOutOfRangeException("Invalid placement!");
                    }

                    index -= (backwardCounter * 10);

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
                    for (int i = 0; i < 100; i++)
                    {
                        _p1.Table[i] = _playerGrid[i].Tag.ToString();
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
                for (int i = 0; i < 100; i++)
                {
                    _p1.Table[i] = _playerGrid[i].Tag.ToString();
                }
                Reset();
                _p1IsSet = true;
                return;
            }
            else if (_placedShips == 5 && _p1IsSet)
            {
                for (int i = 0; i < 100; i++)
                {
                    _p2.Table[i] = _playerGrid[i].Tag.ToString();

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

            int index = Array.IndexOf(_playerGrid, senderGrid);

            Console.WriteLine(index + "\n");

            int backwardCounter, forwardCounter;

            if (OrientationHorizonatal.IsChecked == true)
            {
                try
                {
                    backwardCounter = 0;
                    forwardCounter = 0;

                    for (int i = 0; (backwardCounter + forwardCounter) < _length; i++)
                    {
                        if (index + i < 100 && (_playerGrid[index + i].Tag.Equals("0")))
                        {
                            forwardCounter++;
                            continue;
                        }
                        else if ((_playerGrid[index - i].Tag.Equals("0")))
                        {
                            backwardCounter++;
                        }
                    }

                    Console.WriteLine(backwardCounter + "  " + forwardCounter);

                    //van-e elég hely
                    if (forwardCounter + backwardCounter < _length)
                    {
                        throw new IndexOutOfRangeException("Invalid placement!");
                    }

                    index -= backwardCounter;
                    
                    //sorvége és eleje
                    if (((_length - 1) > ((index + _length - 1) % 10)))
                    {
                        index -= (index + _length ) % 10;
                    }

                    for (int i = 0; i < _length; i++)
                    {
                        _playerGrid[index + i].Tag = _ship;
                        _playerGrid[index + i].Background = new SolidColorBrush(Colors.Green);
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
                    backwardCounter = 0;
                    forwardCounter = 0;

                    for (int i = 0; (backwardCounter + forwardCounter) < _length; i++)
                    {
                        if (index + (i * 10) < 100 && (_playerGrid[index + (i * 10)].Tag.Equals("0")))
                        {
                            forwardCounter++;
                            continue;
                        }
                        else if ((_playerGrid[index - (i * 10)].Tag.Equals("0")))
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

                    index -= (backwardCounter * 10);

                    //sorvége és eleje
                    if (((_length - 1) > (index + ((_length - 1) * 10))))
                    {
                        index -= ((index + _length) * 10);
                    }

                    for (int i = 0; i < _length; i++)
                    {
                        _playerGrid[index + (i * 10)].Tag = _ship;
                        _playerGrid[index + (i * 10)].Background = new SolidColorBrush(Colors.Green);
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
