using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;
using ByteDev.Azure.KeyVault.SecretApps.Core;
using ByteDev.Azure.KeyVault.SecretExport.Args;
using ByteDev.Azure.KeyVault.Secrets;
using ByteDev.Cmd;
using ByteDev.Cmd.Arguments;

namespace ByteDev.Azure.KeyVault.SecretExport
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
                Output.WriteHeader("Secret Import");

                var argSettings = new ArgSettings(new CmdArgInfo(args, cmdAllowedArgs));

                _secretClient = new KeyVaultSecretClient(argSettings.KeyVaultUri);

                WriteSubHeader(argSettings);

                CheckFileExists(argSettings);

                var secrets = await GetSecretsAsync(argSettings);

                SaveSecrets(argSettings.FilePath, secrets);
                
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

        private static async Task<IList<KeyVaultSecret>> GetSecretsAsync(ArgSettings argSettings)
        {
            if (!string.IsNullOrEmpty(argSettings.SectionName))
                return await _secretClient.GetSectionAsync(argSettings.SectionName);
                
            return await _secretClient.GetAllAsync();
        }

        private static void CheckFileExists(ArgSettings argSettings)
        {
            if (File.Exists(argSettings.FilePath))
            {
                if (!argSettings.UseFileOverwrite)
                {
                    Output.WriteWarning($"File exists at: {argSettings.FilePath}.");
                    Environment.Exit(0);
                }
            }
        }

        private static void SaveSecrets(string filePath, IList<KeyVaultSecret> secrets)
        {
            const string delimiter = "=====>";

            Output.WriteLine($"{secrets.Count} secrets found.");

            using (var sw = new StreamWriter(filePath))
            {
                foreach (var secret in secrets)
                {
                    sw.WriteLine(secret.Name + delimiter + secret.Value);
                }
            }
        }

        private static void WriteSubHeader(ArgSettings argSettings)
        {
            Output.WriteLine($"KV URI: {argSettings.KeyVaultUri}");
            Output.WriteLine($"File path: {argSettings.FilePath}");
            
            if (!string.IsNullOrEmpty(argSettings.SectionName))
                Output.WriteLine($"Section name: {argSettings.SectionName}");
            
            Output.WriteLine();
        }
    }
}
