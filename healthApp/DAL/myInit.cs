using healthApp.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace healthApp.DAL {
    public class myInit : DropCreateDatabaseIfModelChanges<AccountsDBContext> {
        protected override void Seed( AccountsDBContext context ) {
            var users = new List<Accounts>
            {
            new Accounts{ID=1,Username="sysadmin",Password="sysadmin",fName="System",lName="Admin", acctType="sysadmin"},
            };

            users.ForEach( s => context.Accounts.Add( s ) );
            context.SaveChanges();

        }
    }
}