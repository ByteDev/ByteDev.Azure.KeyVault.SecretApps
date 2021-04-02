using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Azure.Security.KeyVault.Secrets;
using ByteDev.Cmd;
using ByteDev.Cmd.Arguments;

namespace ByteDev.Azure.KeyVault.SecretApps.Core
{
    public static class OutputExtensions
    {
        public static void WriteHeader(this Output source, string appTitle)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            source.Write(new MessageBox($" {appTitle} {fvi.ProductVersion} ")
            {
                TextColor = new OutputColor(ConsoleColor.White, ConsoleColor.Blue),
                BorderColor = new OutputColor(ConsoleColor.White, ConsoleColor.Blue)
            });

            source.WriteLine();
        }

        public static void WriteWarning(this Output source, string message)
        {
            source.WriteLine(message, new OutputColor(ConsoleColor.Yellow));
        }

        public static void WriteError(this Output source, Exception ex)
        {
            source.WriteLine(ex.ToString(), new OutputColor(ConsoleColor.Red));
        }

        public static void WriteError(this Output source, CmdArgException ex, IList<CmdAllowedArg> cmdAllowedArgs)
        {
            // When creating CmdArgInfo if any invalid input a CmdArgException will be thrown
            source.WriteLine(ex.Message, new OutputColor(ConsoleColor.Red));
            source.WriteLine();
            source.WriteLine(cmdAllowedArgs.HelpText());
        }

        public static void Write(this Output source, KeyVaultSecret secret)
        {
            source.WriteLine($"{secret.Name} = {secret.Value}");
        }

        public static void Write(this Output source, IEnumerable<KeyVaultSecret> secrets)
        {
            foreach (var secret in secrets)
            {
                Write(source, secret);
            }
        }
    }
}