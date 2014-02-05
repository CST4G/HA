using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using healthApp.Models;
using DevOne.Security.Cryptography.BCrypt;

namespace healthApp.DAL {
    public class AccountInit : CreateDatabaseIfNotExists<AccountsDBContext> {
        protected override void Seed( AccountsDBContext context ) {
            string[] mypassword = { "sysadmin", "admin", "123322" };
            string[] salts = { BCryptHelper.GenerateSalt(), BCryptHelper.GenerateSalt(), BCryptHelper.GenerateSalt() };
            string[] hash = new String[3];
            for ( int i = 0; i < 3; ++i ) {
                hash[i] = BCryptHelper.HashPassword( mypassword[i], salts[i] );
            }

            var users = new List<Accounts>
            {
                new Accounts{ID=1,Username="sysadmin",encryptedPassword=hash[0], salt=salts[0], fName="system",lName="admin", acctType="sysadmin"},
            };

            users.ForEach( s => context.Accounts.Add( s ) );
            context.SaveChanges();
        }
    }

    public class ClientInit : CreateDatabaseIfNotExists<ClientDBContext> {
        protected override void Seed( ClientDBContext context ) {

            var clients = new List<Client>
            {
                new Client{ClientID=1, ClientFirstName="John", ClientLastName="Doe", ClientDOB=DateTime.Now, ClientHealthNum=4, ClientGender="Male", ClientBedNum=1, ClientFamilyDoc="John Wayne"},
                new Client{ClientID=2, ClientFirstName="Fred", ClientLastName="Daily", ClientDOB=DateTime.Now, ClientHealthNum=2, ClientGender="Male", ClientBedNum=2, ClientFamilyDoc="Mason Family"},
                new Client{ClientID=3, ClientFirstName="Charlie", ClientLastName="Zoolander", ClientDOB=DateTime.Now, ClientHealthNum=5, ClientGender="Male", ClientBedNum=3, ClientFamilyDoc="Dr Who"},
                new Client{ClientID=4, ClientFirstName="Susan", ClientLastName="Francis", ClientDOB=DateTime.Now, ClientHealthNum=3, ClientGender="Female", ClientBedNum=4, ClientFamilyDoc="You"},
                new Client{ClientID=5, ClientFirstName="Betty", ClientLastName="Faulker", ClientDOB=DateTime.Now, ClientHealthNum=1, ClientGender="Female", ClientBedNum=5, ClientFamilyDoc="Surely"},
            };

            clients.ForEach( s => context.Client.Add( s ) );
            context.SaveChanges();
        }
    }
}