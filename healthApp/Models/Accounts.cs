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

namespace healthApp.Models
{
    public class Accounts
    {
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [DisplayName("First Name")]
        public string fName { get; set; }

        [DisplayName("Last Name")]
        public string lName { get; set; }

        [Required]
        [DisplayName("Account Type")]
        public string acctType { get; set; }



        public static bool IsValid(string _username, string _password, AccountsDBContext db)
        {
            Accounts user = db.Accounts.SingleOrDefault(account => account.Username == _username && account.Password == _password);

            return (!(user == null));

            /*
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=H:\Login\Login\App_Data\database.mdf;Integrated Security=True")) 
            {
                string _sql = @"SELECT [Username] FROM [dbo].[Accounts] " +
                       @"WHERE [Username] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _username;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = (_password);
                cn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
             * */
        }

        public static string findType(string _username, string _password, AccountsDBContext db)
        {

            Accounts user = db.Accounts.SingleOrDefault(account => account.Username == _username);

            return user.acctType;

            /*
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=H:\Login\Login\App_Data\database.mdf;Integrated Security=True"))
            {
                string _sql = @"SELECT [acctType] FROM [dbo].[Accounts] " +
                       @"WHERE [Username] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, cn);

                
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _username;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = (_password);
                
                cn.Open();
                var reader = cmd.ExecuteReader();

                int i =0;
                string test = "";
                while (reader.Read())
                {
                    test += reader.GetString(i++);
                }
                return test;
            }
             * */
        }
        
    }


    /*
    public class AccountsDBContext : DbContext
    {
        public DbSet<Accounts> Accounts { get; set; }
    }
    */
    public class ApplicationUser : IdentityUser
    {
    }

    public class AccountsDBContext : IdentityDbContext<ApplicationUser>
    {

        public AccountsDBContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Accounts> Accounts { get; set; }
    }

}