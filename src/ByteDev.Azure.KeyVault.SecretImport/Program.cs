using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ByteDev.Azure.KeyVault.SecretApps.Core;
using ByteDev.Azure.KeyVault.SecretImport.Args;
using ByteDev.Azure.KeyVault.Secrets;
using ByteDev.Cmd;
using ByteDev.Cmd.Arguments;

namespace ByteDev.Azure.KeyVault.SecretImport
{
    internal class Program
    {
        private static readonly Output Output = new Output();

        private static KeyVaultSecretClient _secretClient;

        private static ArgSettings _argSettings;

        static async Task Main(string[] args)
        {
            var cmdAllowedArgs = new CmdAllowedArgsFactory().Create();
            
            try
            {
                Output.WriteHeader("Secret Import");

                _argSettings = new ArgSettings(new CmdArgInfo(args, cmdAllowedArgs));

                _secretClient = new KeyVaultSecretClient(_argSettings.KeyVaultUri);

                WriteSubHeader(_argSettings);

                var pairs = await new KeyValuePairFileReader().ReadFile(_argSettings.FilePath);

                await SetSecretsAsync(pairs);

                Output.WriteLine();
                Output.WriteLine("Done.");
            }
            catch (CmdArgException ex)
            {
                Output.WriteError(ex, cmdAllowedArgs);
            }
            catch (Exception ex)
            {
                Output.WriteError(ex);
            }
        }

        private static async Task SetSecretsAsync(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            foreach (var pair in pairs)
            {
                if (string.IsNullOrEmpty(pair.Key))
                {
                    Output.WriteWarning("Skipping - Key was null or empty");
                }
                else if (string.IsNullOrEmpty(pair.Value))
                {
                    Output.WriteWarning($"Skipping - Key: '{pair.Key}' value was null or empty");
                }
                else
                {
                    await SetSecretAsync(pair);
                }
            }
        }

        private static async Task SetSecretAsync(KeyValuePair<string, string> pair)
        {
            var isSet = await _secretClient.SafeSetValueAsync(pair.Key, pair.Value);

            if (isSet)
                Output.WriteLine($"Set - {pair.Key} = {pair.Value}");
            else
                Output.WriteWarning($"Skipping - Key: {pair.Key} - Not set as has same value");
        }

        private static void WriteSubHeader(ArgSettings argSettings)
        {
            Output.WriteLine($"KV URI: {argSettings.KeyVaultUri}");
            Output.WriteLine($"File path: {argSettings.FilePath}");
            Output.WriteLine();
        }
    }
}
