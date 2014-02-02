using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using healthApp.Models;
using DevOne.Security.Cryptography.BCrypt;

namespace healthApp.DAL
{
    public class myInit : CreateDatabaseIfNotExists<AccountsDBContext>
    {
        protected override void Seed(AccountsDBContext context)
        {
            string[] mypassword = { "sysadmin", "admin", "123322" };
            string[] salts = { BCryptHelper.GenerateSalt(), BCryptHelper.GenerateSalt(), BCryptHelper.GenerateSalt() };
            string[] hash = new String[3];
            for(int i = 0; i < 3; ++i){
                hash[i] =  BCryptHelper.HashPassword(mypassword[i], salts[i]);
            }

            var users = new List<Accounts>
            {
                new Accounts{ID=1,Username="sysadmin",encryptedPassword=hash[0], salt=salts[0], fName="sysadmin",lName="diggity", acctType="sysadmin"},
                new Accounts{ID=2,Username="admin",encryptedPassword=hash[1], salt=salts[1],fName="Not Hung",lName="Le", acctType="admin"},
                new Accounts{ID=3,Username="Hung",encryptedPassword=hash[2], salt=salts[2],fName="Hung",lName="Le", acctType="staff"}
            };

            users.ForEach(s => context.Accounts.Add(s));
            context.SaveChanges();


        }
    }
}