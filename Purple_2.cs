using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_2
    {
        const int JUDGES_COUNT = 5;
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return null;

                    int[] marks = new int[_marks.Length];
                    Array.Copy(_marks, marks, marks.Length);
                    return marks;
                }
            }
            public int Result
            {
                get
                {
                    if (_marks == null) return 0;

                    int result = 0;
                    int imax = 0, imin = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        result += _marks[i];
                        if (_marks[i] > _marks[imax]) imax = i;
                        if (_marks[i] < _marks[imin]) imin = i;
                    }
                    result -= _marks[imax] + _marks[imin];
                    result += 60;
                    result += (_distance - 120) * 2;
                    return result;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[JUDGES_COUNT];
                for (int i = 0; i < JUDGES_COUNT; i++) _marks[i] = 0;
            }

            public void Jump(int distance, int[] marks)
            {
                if (marks == null || _marks == null || marks.Length != _marks.Length) return;

                _distance = distance;
                Array.Copy(marks, _marks, marks.Length);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                Array.Sort(array, (a, b) => {
                    return b.Result - a.Result;
                });
            }
        }
    }
}
