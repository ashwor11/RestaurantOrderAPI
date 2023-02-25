namespace Core.Security.JWT;

public class EmailVerificationTokenOptions
{
    public string SecretKey { get; set; }

    public int TokenExpiration { get; set; }
}