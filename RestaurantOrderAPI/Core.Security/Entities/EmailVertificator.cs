using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Entities
{
    public class EmailVertificator : Entity
    {
        public int UserId { get; set; }
        public string? ActivationCode { get; set; }
        public bool IsVerified { get; set; }

        public virtual User User { get; set; }

        public EmailVertificator()
        {
            IsVerified = false;
        }

        public EmailVertificator(int userId, string activationCode, bool isVerified)
        {
            UserId = userId;
            ActivationCode = activationCode;
            IsVerified = isVerified;
        }
    }
}
