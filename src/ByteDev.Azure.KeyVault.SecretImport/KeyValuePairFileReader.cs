using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ByteDev.Strings;

namespace ByteDev.Azure.KeyVault.SecretImport
{
    public class KeyValuePairFileReader
    {
        private const string Delimiter = "=====>";

        public async Task<List<KeyValuePair<string, string>>> ReadFile(string filePath)
        {
            var list = new List<KeyValuePair<string, string>>();

            var lines = await File.ReadAllLinesAsync(filePath);

            foreach (var line in lines)
            {
                var pair = line.Trim().ToKeyValuePair(Delimiter);

                list.Add(pair);
            }

            return list;
        }
    }
}