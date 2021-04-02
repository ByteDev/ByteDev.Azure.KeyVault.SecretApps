using System;
using System.Threading.Tasks;
using ByteDev.Azure.KeyVault.SecretApps.Core;
using ByteDev.Azure.KeyVault.Secrets;
using ByteDev.Azure.KeyVault.SecretShow.Args;
using ByteDev.Cmd;
using ByteDev.Cmd.Arguments;

namespace ByteDev.Azure.KeyVault.SecretShow
{
    internal class Program
    {
        private static readonly Output Output = new Output();

        private static KeyVaultSecretClient _secretClient;

        static async Task Main(string[] args)
        {
            var cmdAllowedArgs = new CmdAllowedArgsFactory().Create();

            try
            {
                Output.WriteHeader("Secret Show");

                var argSettings = new ArgSettings(new CmdArgInfo(args, cmdAllowedArgs));

                _secretClient = new KeyVaultSecretClient(argSettings.KeyVaultUri);

                Output.WriteLine($"KV URI: {argSettings.KeyVaultUri}");

                if (string.IsNullOrEmpty(argSettings.SectionName))
                    await WriteAllAsync();
                else
                    await WriteSectionAsync(argSettings.SectionName);

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

        private static async Task WriteAllAsync()
        {
            var secrets = await _secretClient.GetAllAsync();

            Output.WriteLine();
            Output.WriteLine($"{secrets.Count} secrets.");
            Output.WriteLine();
            Output.Write(secrets);
        }

        private static async Task WriteSectionAsync(string sectionName)
        {
            var secrets = await _secretClient.GetSectionAsync(sectionName);

            Output.WriteLine();
            Output.WriteLine($"Section: {sectionName}");
            Output.WriteLine();
            Output.WriteLine($"{secrets.Count} secrets.");
            Output.WriteLine();
            Output.Write(secrets);
        }
    }
}
