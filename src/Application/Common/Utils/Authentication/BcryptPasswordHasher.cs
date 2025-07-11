namespace Application.Common.Utils;

using BCrypt.Net;

public static class BcryptPasswordHasher
{
   public static string Hash(string password)
    {
        return BCrypt.HashPassword(password);
    }

    public static bool Verify(string password, string hash)
    {
        return BCrypt.Verify(password, hash);
    }
}