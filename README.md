# ByteDev.Azure.KeyVault.SecretApps

Set of .NET Core console apps for doing bulk operations on Azure Key Vault secrets.

Apps include:

- Import => Import a set of key value pairs from a file as secrets into Azure Key Vault.
- Export => Export a set of secrets from Azure Key Vault to a file.
- Show => Display different sets of Azure Key Vault secrets in the console.

## Import

Run the console app (you will need to be logged into the Azure account already):

```
cd C:\ByteDev.Azure.KeyVault.SecretApps\src\ByteDev.Azure.KeyVault.SecretImport

dotnet run -- -u <KEY_VAULT_URL> -p <FILE_PATH>
```

**-u**

Required. This is the URL to your Key Vault instance in Azure. Usually: `https://<INSTANCE_NAME>.vault.azure.net/`.

**-p**

Required. This is the path to the text file containing the secrets. The format should be:

```
Name1=====>Value1
Name2=====>Value2
Name3=====>Value3
```

Where `=====>` is the name value delimiter.

---

## Export

Run the console app (you will need to be logged into the Azure account already):

```
cd C:\ByteDev.Azure.KeyVault.SecretApps\src\ByteDev.Azure.KeyVault.SecretExport

dotnet run -- -u <KEY_VAULT_URL> -p <FILE_PATH> -s <SECTION_NAME> -f
```

**-u**

Required. This is the URL to your Key Vault instance in Azure. Usually: `https://<INSTANCE_NAME>.vault.azure.net/`.

**-p**

Required. Path to the text file where the secrets will be written.

**-s**

Optional. If provided will save all secrets within a particular section. If not provided all secerts will be saved.

**-f**

Optional. No value required. If `-f` is provided then if the file exists it will be overwritten. If not provided then the application will not overwrite the file and will exit instead.

---

## Show

Run the console app (you will need to be logged into the Azure account already):

```
cd C:\ByteDev.Azure.KeyVault.SecretApps\src\ByteDev.Azure.KeyVault.SecretShow

dotnet run -- -u <KEY_VAULT_URL> -s <SECTION_NAME>
```

**-u**

Required. This is the URL to your Key Vault instance in Azure. Usually: `https://<INSTANCE_NAME>.vault.azure.net/`.

**-s**

Optional. If provided will display all secrets within a particular section. If not provided all secerts will be displayed.

---

## Appendix - Section Names

Secret names can have optional section name prefixes

For example in this set of secrets the section name is `MyFuncApp`:

```
MyFuncApp--Name1=====>Value1
MyFuncApp--Name2=====>Value2
MyFuncApp--Name3=====>Value3
```

Section names can also be nested like section name `MyFuncApp--LoggingSettings` for such secrets like:

```
MyFuncApp--LoggingSettings--Level=====>Debug
```
