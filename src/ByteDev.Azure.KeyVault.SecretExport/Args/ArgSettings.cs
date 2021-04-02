using ByteDev.Cmd.Arguments;

namespace ByteDev.Azure.KeyVault.SecretExport.Args
{
    public class ArgSettings
    {
        public string KeyVaultUri { get; }

        public string FilePath { get; }

        public string SectionName { get; }

        public bool UseFileOverwrite { get; }

        public ArgSettings(CmdArgInfo cmdArgInfo)
        {
            KeyVaultUri = cmdArgInfo.GetArgument('u')?.Value;
            FilePath = cmdArgInfo.GetArgument('p')?.Value;
            SectionName = cmdArgInfo.GetArgument('s')?.Value;

            if (cmdArgInfo. GetArgument('f') != null)
                UseFileOverwrite = true;
        }
    }
}