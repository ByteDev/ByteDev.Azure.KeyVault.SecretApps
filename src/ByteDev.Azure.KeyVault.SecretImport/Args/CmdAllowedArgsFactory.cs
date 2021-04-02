using System.Collections.Generic;
using ByteDev.Cmd.Arguments;

namespace ByteDev.Azure.KeyVault.SecretImport.Args
{
    public class CmdAllowedArgsFactory
    {
        public IList<CmdAllowedArg> Create()
        {
            return new List<CmdAllowedArg>
            {
                new CmdAllowedArg('u', true) { IsRequired = true, Description = "Key Vault URI" },
                new CmdAllowedArg('p', true) { IsRequired = true, Description = "Path to name value pairs file" }
            };
        }
    }
}