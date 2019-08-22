using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.String;

namespace CQS_Example
{
    public class Adder
    {
        private List<int> _ints = new List<int>();

        public void Add(int number) => _ints.Add(number);
        public void AddAt(int number, int index) => _ints.Insert(index, number);
        public void Update(int number, int index) => _ints[index] = number;
        public void Delete(int index) => _ints.RemoveAt(index);
        public int Sum() => _ints.Sum();
        public string List() => Join("+", _ints);
    }
}
