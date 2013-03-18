using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GodsCharPinyin;
using GodsUtility;
using System.Reflection;

namespace DotaHeroName
{
    class DataProcesser
    {
        DataIndicsLibrary _dataIndicsLib = new DataIndicsLibrary();

        public DataIndicsLibrary DataIndicsLibrary
        {
            get
            {
                return _dataIndicsLib;
            }
        }

        const string DOTA_HERO_NAMES_FILE_PATH = "DotaHeroName.DataResource.DotaHeroNames.txt";


        string GetDotaHeroNamesFileText()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return GodsResourcesUtility.GetEmbedResourceFileText(assembly, DOTA_HERO_NAMES_FILE_PATH);
        }

        /// <summary>
        /// 获取Dota英雄名称
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllDotaHeroNames()
        {
            string fileText = GetDotaHeroNamesFileText();

            string[] allTextLines = fileText.Split(
                new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            return allTextLines.Where(line => !string.IsNullOrEmpty(line));
        }

        /// <summary>
        /// 建立索引
        /// </summary>
        public void CreateInvertedIndex()
        {
            IEnumerable<string> heroNames = GetAllDotaHeroNames();

            foreach (string source in heroNames)
            {
                foreach (char ch in source)
                {
                    _dataIndicsLib.AddData(ch.ToString(), source);

                    HashSet<string> pinyinTexts;
                    if (PinyinLibrary.Instance.TryGetPinyin(ch, out pinyinTexts))
                    {
                        foreach (string pinyinText in pinyinTexts)
                        {
                            _dataIndicsLib.AddData(pinyinText, source);
                        }
                    }
                }
            }
        }

        const int MATCH_CH_WEIGHT = 70;
        const int MATCH_PINYIN_WIGHT = 30;

        public List<string> FindSimilarResult(string source)
        {
            Dictionary<string, int> targesWithWeight = new Dictionary<string, int>();
            foreach (char ch in source)
            {
                HashSet<string> targets;

                if (_dataIndicsLib.TryGetData(ch.ToString(), out targets))
                {
                    foreach (string target in targets)
                    {
                        AddMultiTargetsWithWeight(targesWithWeight, targets, MATCH_CH_WEIGHT);
                    }
                }

                HashSet<string> pinyinTexts;
                if (PinyinLibrary.Instance.TryGetPinyin(ch, out pinyinTexts))
                {
                    foreach (string pinyinText in pinyinTexts)
                    {
                        if (_dataIndicsLib.TryGetData(pinyinText, out targets))
                        {
                            AddMultiTargetsWithWeight(targesWithWeight, targets, MATCH_PINYIN_WIGHT);
                        }
                    }
                }
            }

            return targesWithWeight.ToArray()
                .OrderByDescending(pair => pair.Value)
                .Select(pair => pair.Key).ToList();
        }

        static void AddMultiTargetsWithWeight(Dictionary<string, int> dictWithWeight, IEnumerable<string> keys, int weight)
        {
            foreach (var key in keys)
            {
                AddOneTargetWithWeight(dictWithWeight, key, weight);
            }
        }

        static void AddOneTargetWithWeight(Dictionary<string, int> dictWithWeight, string key, int weight)
        {
            int baseWeight;
            if (dictWithWeight.TryGetValue(key, out baseWeight))
            {
                dictWithWeight[key] = baseWeight + weight;
            }
            else
            {
                dictWithWeight.Add(key, weight);
            }
        }
    }
}
