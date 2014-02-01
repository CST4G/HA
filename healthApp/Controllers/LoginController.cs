using healthApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace healthApp.Controllers {
    public class LoginController : Controller {
        AccountsDBContext db = new AccountsDBContext();

        public bool hasAccess() {
            return ((Session["UserProfile"] != null) &&
            ((UserProfileObj) Session["UserProfile"]).accType == "sysadmin");

        }
        // Index if system admin gives access to all users otherwise redirecto to Login
        // GET: /Login/
        public ActionResult Index() {
            if ( hasAccess() ) {
                return View( db.Accounts.ToList() );
            } else {
                return RedirectToAction( "Login", "Login" );
            }
        }

        // GET: /Login/Login
        [AllowAnonymous]
        public ActionResult Login() {
            return View();
        }


        // POST: /Login/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login( Accounts model, string returnUrl ) {

            if ( Accounts.IsValid( model.Username, model.Password, db ) ) {
                //ModelState.AddModelError("","Great fucking success fuck");
                string accType = Accounts.findType( model.Username, model.Password, db );
                //ModelState.AddModelError("", accType);

                var profileData = new UserProfileObj( model.Username, accType );

                Session["UserProfile"] = profileData;

                UserProfileObj test = (UserProfileObj) this.Session["UserProfile"];

                //ModelState.AddModelError("", test.Username);
                //ModelState.AddModelError("", test.accType);
                return RedirectToAction( "Index", "Home" );
            } else {
                ModelState.AddModelError( "", "Invalid Login" );
            }

            // If we got this far, something failed, redisplay form
            return View( model );
        }


        //SIGN OUT
        //
        // POST: /Login/LogOff
        public ActionResult LogOff() {
            this.Session.Clear();
            return RedirectToAction( "Index", "Home" );
        }

        // GET: /Login/Details/5
        public ActionResult Details( int? id ) {
            if ( hasAccess() ) {
                if ( id == null ) {
                    return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
                }
                Accounts accounts = db.Accounts.Find( id );
                if ( accounts == null ) {
                    return HttpNotFound();
                }

                return View( accounts );
            } else {
                return RedirectToAction( "Index", "Home" );
            }
        }

        // GET: /Login/Create
        public ActionResult Create() {
            if ( hasAccess() ) {
                return View();
            } else {
                return RedirectToAction( "Index", "Home" );
            }

        }

        // POST: /Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( [Bind( Include = "ID,Username,Password,fName,lName,acctType" )] Accounts accounts ) {
            if ( hasAccess() ) {
                if ( ModelState.IsValid ) {
                    db.Accounts.Add( accounts );
                    db.SaveChanges();
                    return RedirectToAction( "Index" );
                } else {
                    return RedirectToAction( "Index", "Home" );
                }
            } else {
                return RedirectToAction( "Index", "Home" );
            }
        }

        // GET: /Login/Edit/5
        public ActionResult Edit( int? id ) {
            if ( hasAccess() ) {
                if ( id == null ) {
                    return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
                }
                Accounts accounts = db.Accounts.Find( id );
                if ( accounts == null ) {
                    return HttpNotFound();
                }
                return View( accounts );
            } else {
                return RedirectToAction( "Index", "Home" );
            }
        }

        // POST: /Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( [Bind( Include = "ID,Username,Password,fName,lName,acctType" )] Accounts accounts ) {
            if ( hasAccess() ) {
                if ( ModelState.IsValid ) {
                    db.Entry( accounts ).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction( "Index" );
                }
                return View( accounts );
            } else {
                return RedirectToAction( "Index", "Home" );
            }
        }

        // GET: /Login/Delete/5
        public ActionResult Delete( int? id ) {

            if ( hasAccess() ) {
                if ( id == null ) {
                    return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
                }
                Accounts account = db.Accounts.Find( id );
                if ( account == null ) {
                    return HttpNotFound();
                }
                return View( account );
            } else {
                return RedirectToAction( "Index", "Home" );
            }
        }

        // POST: /Login/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed( int id ) {
            if ( hasAccess() ) {
                Accounts accounts = db.Accounts.Find( id );
                db.Accounts.Remove( accounts );
                db.SaveChanges();
                return RedirectToAction( "Index" );
            } else {
                return RedirectToAction( "Index", "Home" );
            }
        }

        protected override void Dispose( bool disposing ) {
            if ( disposing ) {
                db.Dispose();
            }
            base.Dispose( disposing );
        }
    }
}
