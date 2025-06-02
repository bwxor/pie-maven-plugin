using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieMavenPlugin
{
    public class ResourceReader
    {
        public static string ReadResource(string resourceName)
        {
            var assembly = typeof(ResourceReader).Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ArgumentException($"Resource '{resourceName}' not found in assembly '{assembly.FullName}'.");
                }
                using (var reader = new System.IO.StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
