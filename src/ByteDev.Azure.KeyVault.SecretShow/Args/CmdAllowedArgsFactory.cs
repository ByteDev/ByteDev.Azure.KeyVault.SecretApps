using System.Collections.Generic;
using ByteDev.Cmd.Arguments;

namespace ByteDev.Azure.KeyVault.SecretShow.Args
{
    public class CmdAllowedArgsFactory
    {
        public IList<CmdAllowedArg> Create()
        {
            return new List<CmdAllowedArg>
            {
                new CmdAllowedArg('u', true) { IsRequired = true, Description = "Key Vault URI" },
                new CmdAllowedArg('s', true) { IsRequired = false, Description = "Secret section name" }
            };
        }
    }
}