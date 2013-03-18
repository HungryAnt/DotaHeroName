using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace GodsUtility
{
    public static class GodsResourcesUtility
    {
        public static string GetEmbedResourceFileText(Assembly assembly, string resourceFilePath)
        {
            using (StreamReader reader = new StreamReader(
                assembly.GetManifestResourceStream(resourceFilePath)
                ))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
