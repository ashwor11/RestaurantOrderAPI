namespace Application.Features.Auths.Dtos;

public class EnableOtpVerificationDto
{
    public string SecretKey { get; set; }
    public string QrCode { get; set; }
}