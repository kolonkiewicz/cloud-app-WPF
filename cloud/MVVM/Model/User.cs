using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace cloud.MVVM.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<CloudFile> CloudFiles { get; set; } = null!;

        
    }

   
}