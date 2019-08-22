using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS_Example
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
        private readonly Ledger _ledger;

        public AdderQuery(Ledger ledger)
        {
            _ledger = ledger;
        }

        public int Sum() => _ledger.Result();
        public string Display() => _ledger.List();
    }
}
