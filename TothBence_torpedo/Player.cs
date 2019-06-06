namespace TothBence_torpedo
{
    public class Player
    {
        private string _name;

        public string Name
        {
            get {return _name; }
            set {_name = value; }
        }
        
        private int _score;

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        private string[] _table;

        public string[] Table
        {
            get { return _table; }
            set { _table = value; }
        }

        public Player( string name)
        {
            _name = name;
            _score = 0;
            _table = new string[100];
        }
    }
}
