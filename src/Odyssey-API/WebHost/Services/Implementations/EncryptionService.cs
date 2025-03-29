using System.Security.Cryptography;
using WebHost.Services.Contracts;

namespace WebHost.Services.Implementations;

public class EncryptionService : IEncryptionService
{
    public byte[] Encrypt(byte[] data, byte[] key)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new System.IO.MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

        ms.Write(aes.IV, 0, aes.IV.Length); // Prepend IV to the encrypted data
        cs.Write(data, 0, data.Length);
        cs.FlushFinalBlock();

        return ms.ToArray();
    }

    public byte[] Decrypt(byte[] data, byte[] key)
    {
        using var ms = new System.IO.MemoryStream(data);
        using Aes aes = Aes.Create();
        aes.Key = key;

        byte[] iv = new byte[aes.BlockSize / 8];
        ms.Read(iv, 0, iv.Length); // Read the IV from the beginning of the data
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new MemoryStream();

        cs.CopyTo(reader);
        return reader.ToArray();
    }
}