using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotaHeroName
{
    class DataIndicsLibrary
    {
        //HashSet<DataInvertedIndex> _dataIndics = new HashSet<DataInvertedIndex>();

        Dictionary<string, HashSet<string>> _indexedData = new Dictionary<string, HashSet<string>>();

        public void AddData(string key, string target)
        {
            HashSet<string> targets;
            if (!_indexedData.TryGetValue(key, out targets))
            {
                targets = new HashSet<string>();
                _indexedData.Add(key, targets);
            }
            targets.Add(target);
        }

        public bool TryGetData(string key, out HashSet<string> targets)
        {
            return _indexedData.TryGetValue(key, out targets);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pair in _indexedData)
            {
                sb.Append(pair.Key).Append(" --> [")
                    .Append(string.Join(",", pair.Value.ToArray()))
                    .Append("]").Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
