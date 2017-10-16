using System;

namespace al.performancemanagement.BOL.Model
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get
            {
                return string.Format(LastName + ", " + FirstName);
            }
        }
    }
}
