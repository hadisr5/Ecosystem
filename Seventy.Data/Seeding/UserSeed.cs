using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Seventy.DomainClass.Core;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Seventy.Data.Seeding
{
    public static class UserSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            byte[] salt;
            byte[] buffer2;
            string password = "123456";
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);

            var user = new Users { ID = 1, IsActive = true, RegDate = DateTime.Now, Mobile = "09373576025", Password = Convert.ToBase64String(dst) };

            modelBuilder.Entity<Users>().HasData(user);
        }
    }
}
