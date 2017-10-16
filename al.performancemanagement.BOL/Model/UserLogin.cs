using System;

namespace al.performancemanagement.BOL.Model
{
    public class UserLogin
    {
        public long Id { get; set; }
        public long UserInfoId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AuthCode { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int Attempt { get; set; }
        public DateTime? LastSuccessfulLogin { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
