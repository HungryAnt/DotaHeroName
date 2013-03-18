using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace GodsCharPinyin
{
    //** start by Ant Code Factory
    public class PinyinLibrary
    {
        static PinyinLibrary s_instance;
        static object s_objSync = new object();

        public static PinyinLibrary Instance
        {
            get
            {
                if (s_instance == null)
                {
                    lock (s_objSync)
                    {
                        if (s_instance == null)
                        {
                            s_instance = new PinyinLibrary();
                        }
                    }
                }
                return s_instance;
            }
        }

        PinyinLibrary()
        {
            LoadData();
        }

        /// <summary>
        /// 获取某个汉字的多个拼音
        /// </summary>
        public bool TryGetPinyin(char ch, out HashSet<string> pinyinTextList)
        {
            return _charToPinyinDict.TryGetValue(ch, out pinyinTextList);
//             switch (ch)
//             {
//                 case '哥':
//                     return "ge";
//                 case '布':
//                     return "bu";
//                 case '林':
//                     return "lin";
//                 case '工':
//                     return "gong";
//                 case '程':
//                     return "cheng";
//                 case '师':
//                     return "shi";
//                 case '麟':
//                     return "lin";
//             }
// 
//             return string.Empty;
        }

        #region 获取汉字对应的拼音，相关逻辑

        /// <summary>
        /// 由于存在多种读音，一个汉字可以匹配多个拼音
        /// </summary>
        Dictionary<char, HashSet<string>> _charToPinyinDict = new Dictionary<char, HashSet<string>>();

        void AddPinyin(char ch, string pinyinText)
        {
            HashSet<string> pinyinTexts;
            if (!_charToPinyinDict.TryGetValue(ch, out pinyinTexts))
            {
                pinyinTexts = new HashSet<string>();
                _charToPinyinDict.Add(ch, pinyinTexts);
            }
            pinyinTexts.Add(pinyinText);
        }

        const string PinyinResourcePath = "GodsCharPinyin.Pinyin.txt";

        void LoadData()
        {
            string pinyinResourceText = GetPinyinResourceAllText();

            string[] allTextLines = pinyinResourceText.Split(
                new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string lineText in allTextLines)
            {
                if (!string.IsNullOrEmpty(lineText))
                {
                    string[] textData = lineText.Split('|');
                    if (textData.Length != 3)
                    {
                        throw new Exception("拼音文件解析，行数据格式出错!");
                    }

                    char key;
                    if (char.TryParse(textData[0], out key))
                    {
                        string pinyinText = textData[1];
                        if (!string.IsNullOrEmpty(pinyinText))
                        {
                            AddPinyin(key, pinyinText);
                        }
                        else
                        {
                            throw new Exception("拼音文件解析，获取pinyin出错!");
                        }
                    }
                    else
                    {
                        throw new Exception("拼音文件解析，获取key出错!");
                    }
                }
            }
        }

        string GetPinyinResourceAllText()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            //return GodsResourcesUtility.GetEmbedResourceFileText(assembly, PinyinResourcePath);

            using (StreamReader reader = new StreamReader(
                assembly.GetManifestResourceStream(PinyinResourcePath)
                ))
            {
                return reader.ReadToEnd();
            }
        }

        #endregion

        /*
        /// <summary>
        /// 获取某个拼音的近似拼音
        /// </summary>
        public bool TryGetSimilarPinyin(string pinyin, out HashSet<string> similarPinyinSet)
        {
            if (string.IsNullOrEmpty(pinyin))
            {
                similarPinyinSet = null;
                return false;
            }

            if (pinyin.StartsWith(""))
            {
            }

        }

        // 声母 j/q/x, g/k/h, z/zh, c/ch, s/sh, n/l  
        // 韵母 en/eng
        */
    }
}
