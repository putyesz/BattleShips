using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TothBence_torpedo
{
    class Ship
    {
        private int _length { get; set; }
        private string _name { get; set; }

        public Ship(int length, string name)
        {
            _length = length;
            _name = name;
        }
    }
}
