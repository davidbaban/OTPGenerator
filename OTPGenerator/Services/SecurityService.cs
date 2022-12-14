using System.Security.Cryptography;
using System.Text;

namespace OTPGenerator.Services;

public class SecurityService
{
    public string GenerateOneTimePassword(string userId, DateTime dateTime)
    {
        return GenerateCode(userId, dateTime.ToString("MM/dd/yyyy hh:mm:ss"))[..6];
    }

    private static string GenerateCode(string userId, string dateTime)
    {
        return Math.Abs(BitConverter.ToInt32(Hash(userId, dateTime), 0)).ToString();
    }

    private static byte[] Hash(string userId, string dateTime)
    {
        return Hash(Encoding.Unicode.GetBytes(userId), Encoding.Unicode.GetBytes(dateTime));
    }

    private static byte[] Hash(IEnumerable<byte> userId, IEnumerable<byte> dateTime)
    {
        var saltedValue = userId.Concat(dateTime).ToArray();
        var sha384 = SHA384.Create();

        return sha384.ComputeHash(saltedValue);
    }
}