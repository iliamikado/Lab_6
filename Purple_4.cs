using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_4
    {
        public struct Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool _timeAlreadySet;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
                _timeAlreadySet = false;
            }

            public void Run(double time)
            {
                if (_timeAlreadySet) return;
                _time = time;
                _timeAlreadySet = true;
            }
        }

        public struct Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return null;

                    var sportsmen = new Sportsman[_sportsmen.Length];
                    Array.Copy(_sportsmen, sportsmen, _sportsmen.Length);
                    return sportsmen;
                }
            }

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }

            public Group(Group g)
            {
                _name = g.Name;
                _sportsmen = new Sportsman[g.Sportsmen.Length];
                Array.Copy(g.Sportsmen, _sportsmen, g.Sportsmen.Length);
            }

            public void Add(Sportsman sm)
            {
                if (_sportsmen == null) return;
                var sportsmen = new Sportsman[_sportsmen.Length + 1];
                Array.Copy(_sportsmen, sportsmen, _sportsmen.Length);
                sportsmen[sportsmen.Length - 1] = sm;
                _sportsmen = sportsmen;
            }

            public void Add(Sportsman[] sms)
            {
                if (_sportsmen == null || sms == null) return;
                var sportsmen = new Sportsman[_sportsmen.Length + sms.Length];
                Array.Copy(_sportsmen, sportsmen, _sportsmen.Length);
                Array.ConstrainedCopy(sms, 0, sportsmen, _sportsmen.Length, sms.Length);
                _sportsmen = sportsmen;
            }

            public void Add(Group g)
            {
                if (_sportsmen == null || g.Sportsmen == null) return;
                Add(g.Sportsmen);
            }

            public void Sort()
            {
                Array.Sort(_sportsmen, (a, b) =>
                {
                    double x = a.Time - b.Time;
                    if (x < 0) return -1;
                    else if (x > 0) return 1;
                    else return 0;
                });
            }
        }
    }
}
