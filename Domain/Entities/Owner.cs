using Core.Security.Entities;
using Core.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Owner : User
    {
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public Owner() : base()
        {

        }
        public Owner(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt,
                    bool isVerified, VertificationType vertificationType) : base(firstName,lastName,email,passwordHash,passwordSalt,isVerified,vertificationType)
        {
            Restaurants = new HashSet<Restaurant>();
        }
    }
}
