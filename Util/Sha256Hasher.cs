
using System.Security.Cryptography;
using System.Text;

public static class Sha256Hasher
{
    public static string HashString(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = SHA256.HashData(inputBytes);
        return Convert.ToHexString(hashBytes);
    }
}
