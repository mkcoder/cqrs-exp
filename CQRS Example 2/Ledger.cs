using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS_Example2
{
    public class Ledger
    {
        private List<int> _ints = new List<int>();
        private object _lock = new object();

        public delegate void LedgerInsert(int? idx, int item);
        public delegate void LedgerUpdate(int idx, int item);
        public delegate void LedgerDelete(int idx);

        public event LedgerUpdate LedgerUpdated;
        public event LedgerInsert LedgerInserted;
        public event LedgerDelete LedgerDeleted;

        internal void Add(int num) => Do(() => _ints.Add(num), () => LedgerInserted?.Invoke(null, num));
        internal void AddAt(int num, int idx) => Do(() => _ints.Insert(idx, num), () => LedgerInserted?.Invoke(idx, num));
        internal void Update(int num, int idx) => Do(() => _ints[idx] = num,() => LedgerUpdated?.Invoke(idx, num));
        internal void Delete(int idx) => Do(() => _ints.RemoveAt(idx), () => LedgerDeleted?.Invoke(idx));

        private void Do(Action @lock, Action replication)
        {
            lock (_lock)
            {
                @lock();                
            }

            replication();
        }
    }
}
