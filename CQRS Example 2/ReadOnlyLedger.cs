using System.Collections.Generic;

namespace CQRS_Example2
{
    public class ReadOnlyLedger
    {
        private readonly Ledger _ledger;
        private List<int> _ints = new List<int>();
        public IReadOnlyList<int> Items => _ints.AsReadOnly();

        public ReadOnlyLedger(Ledger ledger)
        {
            _ledger = ledger;
            _ledger.LedgerUpdated += LedgerUpdated;
            _ledger.LedgerInserted += LedgerInserted;
            _ledger.LedgerDeleted += LederDeleted;
        }

        private void LederDeleted(int idx)
            => _ints.RemoveAt(idx);
        private void LedgerUpdated(int idx, int item)
            => _ints[idx] = item;

        private void LedgerInserted(int? idx, int item)
        {
            if (idx == null)
            {
                _ints.Add(item);
            }
            else
            {
                _ints.Insert(idx.Value, item);
            }
        }
    }
}