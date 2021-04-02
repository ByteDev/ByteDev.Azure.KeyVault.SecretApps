using ByteDev.Cmd.Arguments;

namespace ByteDev.Azure.KeyVault.SecretShow.Args
{
    public class ArgSettings
    {
        public string SectionName { get; }

        public string KeyVaultUri { get; }

        public ArgSettings(CmdArgInfo cmdArgInfo)
        {
            SectionName = cmdArgInfo.GetArgument('s')?.Value;
            KeyVaultUri = cmdArgInfo.GetArgument('u')?.Value;
        }
    }
}