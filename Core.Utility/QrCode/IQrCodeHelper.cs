using QRCoder;

namespace Core.Utility.QrCode;

public interface IQrCodeHelper
{
    public string CreateTotpQrCode(string uri);


}