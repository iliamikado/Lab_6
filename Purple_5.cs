﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;
            public string[] AnsArr
            {
                get
                {
                    return new string[] { _animal, _characterTrait, _concept };
                }
            }

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                questionNumber--;
                int count = 0;
                foreach (var r in responses)
                {
                    if (r.AnsArr[questionNumber] != "") count++;
                }
                return count;
            }

            public void Print()
            {
                Console.WriteLine(_animal + " " +  _characterTrait + " " + _concept);
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[] Responses
            {
                get
                {
                    if (_responses == null) return null;

                    var responses = new Response[_responses.Length];
                    Array.Copy(_responses, responses, responses.Length);
                    return responses;
                }
            }

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] answers)
            {
                if (answers == null) return;

                string[] ans = new string[] { "", "", "" };
                for (int i = 0; i < Math.Min(3, answers.Length); i++)
                    ans[i] = answers[i];
                var resp = new Response[_responses.Length + 1];
                Array.Copy(_responses, resp, resp.Length - 1);
                resp[resp.Length - 1] = new Response(ans[0], ans[1], ans[2]);
                _responses = resp;
            }

            public string[] GetTopResponses(int question)
            {
                if (_responses == null) return null;
                question--;
                int difAns = 0;
                for (int i = 0; i < _responses.Length; i++)
                {
                    bool newAns = true;
                    for (int j = 0; j < i; j++)
                    {
                        if (_responses[i].AnsArr[question] == _responses[j].AnsArr[question])
                        {
                            newAns = false;
                            break;
                        }
                    }
                    if (newAns)
                    {
                        difAns++;
                    }
                }
                var answers = new Answer[difAns];
                foreach (var r in _responses)
                {
                    for (int i = 0; i < difAns; i++)
                    {
                        if (answers[i].Count == 0)
                        {
                            answers[i] = new Answer(r.AnsArr[question]);
                            break;
                        }
                        if (answers[i].Value == r.AnsArr[question])
                        {
                            answers[i].Inc();
                            break;
                        }
                    }
                }
                Array.Sort(answers, (a, b) =>
                {
                    return b.Count - a.Count;
                });

                int n = difAns;
                foreach (var a in answers)
                    if (a.Value == "")
                    {
                        n--;
                        break;
                    }
                string[] ans = new string[Math.Min(n, 5)];
                int step = 0;
                for (int i = 0; i < ans.Length; i++)
                {
                    if (answers[i].Value == "") step = 1;
                    ans[i] = answers[i + step].Value;
                }
                return ans;
            }

            public void Print()
            {
                Console.WriteLine(_name);
                foreach (var r in _responses)
                    r.Print();
            }
        }

        private struct Answer
        {
            private string _value;
            private int _count;

            public string Value => _value;
            public int Count => _count;

            public Answer(string value)
            {
                _value = value;
                _count = 1;
            }

            public void Inc()
            {
                _count++;
            }
        }
    }
}
