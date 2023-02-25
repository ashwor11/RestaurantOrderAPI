using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using QRCoder;

namespace Core.Utility.QrCode
{
    public class QrCodeHelper : IQrCodeHelper
    {
        

        public string CreateTotpQrCode(string uri)
        {
            
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            
            QRCodeData qrData = qrGenerator.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrData);
            
            return Convert.ToBase64String(qrCode.GetGraphic(20));
        }
    }
}
