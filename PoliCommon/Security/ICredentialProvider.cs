namespace PoliCommon.Security
{
    public interface ICredentialProvider
    {
        string DecryptValue(string value);
        string EncryptValue(string value);
    }
}