using AuthDemo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Data
{
    public static class DataSeeder
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("123qwe!@#QWE", out passwordHash, out passwordSalt);
            modelBuilder.Entity<UserInfo>().HasData(
                new UserInfo { UserInfoId = 1, LoginUserName = "88881", Email = "KENNY@FAIRYSCHOOL.COM", FullName = "KENNY", IsActive=true,PasswordHash=passwordHash,PasswordSalt=passwordSalt, Role="Admin" },
                new UserInfo { UserInfoId = 2, LoginUserName = "88882", Email = "JULIET@FAIRYSCHOOL.COM", FullName = "JULIET", IsActive = true, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Role = "Admin" },
                new UserInfo { UserInfoId = 3, LoginUserName = "88883", Email = "RANDY@HOTINSTRUCTOR.COM", FullName = "RANDY", IsActive = true, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Role = "Instructor" },
                new UserInfo { UserInfoId = 4, LoginUserName = "88884", Email = "THOMAS@HOTINSTRUCTOR.COM", FullName = "THOMAS", IsActive = true, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Role = "Instructor" },
                new UserInfo { UserInfoId = 5, LoginUserName = "88885", Email = "BEN@HOTINSTRUCTOR.COM", FullName = "BEN", IsActive = true, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Role = "Instructor" },
                new UserInfo { UserInfoId = 6, LoginUserName = "88886", Email = "GABRIEL@HOTINSTRUCTOR.COM", FullName = "GABRIEL", IsActive = true, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Role = "Instructor" },
                new UserInfo { UserInfoId = 7, LoginUserName = "88887", Email = "FRED@HOTINSTRUCTOR.COM", FullName = "FRED", IsActive = true, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Role = "Instructor" }
                );
            
        }

        private static void CreatePasswordHash(string inPassword, out byte[] inPasswordHash, out byte[] inPasswordSalt)
        {
            if (inPassword == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(inPassword)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                inPasswordSalt = hmac.Key;
                inPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inPassword));
            }
        }
    }
}
