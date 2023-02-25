using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Entities
{
    public class OtpVerificator : Entity
    {
        public int UserId { get; set; }
        public bool IsVerified { get; set; }
        public string SecretKey { get; set; }

        public virtual User User { get; set; }

        public OtpVerificator()
        {
            
        }

        public OtpVerificator(int userId, bool isVerified, string secretKey)
        {
            UserId = userId;
            IsVerified = isVerified;
            SecretKey = secretKey;
        }
    }
}
