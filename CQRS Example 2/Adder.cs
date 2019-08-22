using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace CQRS_Example2
{
    public class Adder 
    {
        private readonly Ledger _ledger;

        public Adder(Ledger ledger)
        {
            _ledger = ledger;
        }

        public void Add(int num) => _ledger.Add(num);
        public void AddAt(int num, int idx) => _ledger.AddAt(idx, num);
        public void Update(int num, int idx) => _ledger.Update(num, idx);
        public void Delete(int idx) => _ledger.Delete(idx);
    }

    public class AdderQuery
    {
        private readonly ReadOnlyLedger _onlyLedger;

        public AdderQuery(Ledger ledger)
        {
            _onlyLedger = new ReadOnlyLedger(ledger);
        }

        public int Sum() => _onlyLedger.Items.Sum();
        public string Display() => string.Join("+", _onlyLedger.Items);
    }


    public class MultiplierQuery
    {
        private readonly ReadOnlyLedger _onlyLedger;

        public MultiplierQuery(Ledger ledger)
        {
            _onlyLedger = new ReadOnlyLedger(ledger);
        }

        public int Multiply() => _onlyLedger.Items.Aggregate(1, (i, r) => r*i);
        public string Display() => string.Join("*", _onlyLedger.Items);
    }
}
