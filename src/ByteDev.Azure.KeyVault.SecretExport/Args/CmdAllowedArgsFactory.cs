using System.Collections.Generic;
using ByteDev.Cmd.Arguments;

namespace ByteDev.Azure.KeyVault.SecretExport.Args
{
    public class CmdAllowedArgsFactory
    {
        public IList<CmdAllowedArg> Create()
        {
            return new List<CmdAllowedArg>
            {
                new CmdAllowedArg('u', true) { IsRequired = true, Description = "Key Vault URI" },
                new CmdAllowedArg('p', true) { IsRequired = true, Description = "Path to save secrets" },
                new CmdAllowedArg('s', true) { IsRequired = false, Description = "Secret section name" },
                new CmdAllowedArg('f', false) { IsRequired = false, Description = "Force overwrite of file if exists" }
            };
        }
    }
}