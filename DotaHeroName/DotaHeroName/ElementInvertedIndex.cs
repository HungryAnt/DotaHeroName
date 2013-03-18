// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// 
// namespace DotaHeroName
// {
//     class DataInvertedIndex
//     {
//         public string Key { get; set; }
// 
//         List<string> _target = new List<string>();
// 
//         public List<string> Target
//         {
//             get
//             {
//                 return _target;
//             }
//         }
// 
//         public DataInvertedIndex(string key)
//         {
//             Key = key;
//         }
// 
//         public override bool Equals(object obj)
//         {
//             if (object.ReferenceEquals(this, obj))
//             {
//                 return true;
//             }
// 
//             DataInvertedIndex index = obj as DataInvertedIndex;
//             if (index == null)
//             {
//                 return false;
//             }
// 
//             return this.Key == index.Key;
//         }
// 
//         public override int GetHashCode()
//         {
//             return Key.GetHashCode();
//         }
//     }
// }
