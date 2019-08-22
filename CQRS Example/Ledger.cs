using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS_Example
{
    public class Ledger
    {
        private List<int> _ints = new List<int>();
        private object _lock = new object();

        public void Add(int num) => Lock(() => _ints.Add(num));
        public void AddAt(int num, int idx) => Lock(() => _ints.Insert(idx, num));
        public void Update(int num, int idx) => Lock(() => _ints[idx] = num);
        public void Delete(int idx) => Lock(() => _ints.RemoveAt(idx));
        public int Result() => Lock(_ints.Sum);
        public string List() => Lock(() => string.Join("+", _ints));

        private void Lock(Action action)
        {
            lock (_lock)
            {
                action();
            }
        }

        private T Lock<T>(Func<T> action)
        {
            T result;
            lock (_lock)
            {
                result = action();
            }
            return result;
        }
    }
}
