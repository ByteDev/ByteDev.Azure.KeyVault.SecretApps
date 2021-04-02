using ByteDev.Cmd.Arguments;

namespace ByteDev.Azure.KeyVault.SecretImport.Args
{
    public class ArgSettings
    {
        public string KeyVaultUri { get; }

        public string FilePath { get; }

        public ArgSettings(CmdArgInfo cmdArgInfo)
        {
            KeyVaultUri = cmdArgInfo.GetArgument('u')?.Value;
            FilePath = cmdArgInfo.GetArgument('p')?.Value;
        }
    }
}