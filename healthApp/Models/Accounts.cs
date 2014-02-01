using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace healthApp.Models {
    public class Accounts {
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [DisplayName( "First Name" )]
        public string fName { get; set; }

        [DisplayName( "Last Name" )]
        public string lName { get; set; }

        [Required]
        [DisplayName( "Account Type" )]
        public string acctType { get; set; }

        public static bool IsValid( string _username, string _password, AccountsDBContext db ) {
            Accounts user = db.Accounts.SingleOrDefault( account => account.Username == _username && account.Password == _password );

            return (!(user == null));
        }

        public static string findType( string _username, string _password, AccountsDBContext db ) {

            Accounts user = db.Accounts.SingleOrDefault( account => account.Username == _username );

            return user.acctType;
        }

    }

    public class ApplicationUser : IdentityUser {
    }

    public class AccountsDBContext : IdentityDbContext<ApplicationUser> {

        public AccountsDBContext()
            : base( "DefaultConnection" ) {
        }

        public DbSet<Accounts> Accounts { get; set; }
    }

}