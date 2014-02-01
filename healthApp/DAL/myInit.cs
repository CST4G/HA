using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using healthApp.Models;

namespace healthApp.DAL
{
    public class myInit : DropCreateDatabaseIfModelChanges<AccountsDBContext>
    {
        protected override void Seed(AccountsDBContext context)
        {
            var users = new List<Accounts>
            {
            new Accounts{ID=1,Username="sysadmin",Password="sysadmin",fName="sysadmin",lName="diggity", acctType="sysadmin"},
            new Accounts{ID=2,Username="admin",Password="admin",fName="Not Hung",lName="Le", acctType="admin"},
            new Accounts{ID=3,Username="Hung",Password="123322",fName="Hung",lName="Le", acctType="staff"}
            };

            users.ForEach(s => context.Accounts.Add(s));
            context.SaveChanges();


        }
    }
}