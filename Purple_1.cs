﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_1
    {
        const int JUMPS_COUNT = 4;
        const int JUDGES_COUNT = 7;
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _currentJump;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return null;

                    double[] coefs = new double[_coefs.Length];
                    Array.Copy(_coefs, coefs, _coefs.Length);
                    return coefs;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;

                    int[,] marks = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, marks, _marks.Length);
                    return marks;
                }
            }

            public double TotalScore
            {
                get
                {
                    if (_marks == null || _coefs == null) return 0;
                    
                    double score = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        score += CountJumpScore(i);
                    }
                    return score;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[JUMPS_COUNT];
                _marks = new int[JUMPS_COUNT, JUDGES_COUNT];
                _currentJump = 0;
                for (int i = 0; i < JUMPS_COUNT; i++)
                    _coefs[i] = 2.5;
                for (int i = 0; i < JUMPS_COUNT; i++)
                    for (int j = 0; j < JUDGES_COUNT; j++)
                        _marks[i, j] = 0;
            }

            private double CountJumpScore(int ind)
            {
                if (_marks == null || _coefs == null) return 0;

                double score = 0;
                int[] marks = new int[_marks.GetLength(1)];
                for (int i = 0; i < _marks.GetLength(1); i++)
                {
                    marks[i] = _marks[ind, i];
                    score += marks[i];
                }
                int imin = 0, imax = 0;
                for (int i = 0; i < marks.Length; i++)
                {
                    if (marks[i] < marks[imin]) imin = i;
                    if (marks[i] > marks[imax]) imax = i;
                }
                score -= marks[imin] + marks[imax];
                score *= _coefs[ind];
                return score;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || _coefs == null || coefs.Length != _coefs.Length) return;

                for (int i = 0; i < coefs.Length; i++)
                    _coefs[i] = coefs[i];
            }

            public void Jump(int[] marks)
            {
                if (marks == null || _marks == null || _currentJump >= _marks.GetLength(0) || marks.Length != _marks.GetLength(1)) return;

                for (int i = 0; i < marks.Length; i++)
                    _marks[_currentJump, i] = marks[i];
                _currentJump++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                Array.Sort(array, (a, b) => {
                    double x = b.TotalScore - a.TotalScore;
                    if (x < 0) return -1;
                    else if (x > 0) return 1;
                    else return 0;
                });
            }

            public void Print()
            {

            }
        }
    }
}
