using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dispatch.Contexts;
using Dispatch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dispatch.Controllers {
    public class HomeController : Controller {
        private HomeContext dbContext;

        public HomeController (HomeContext context) {
            dbContext = context;
        }
        public IActionResult Index () {
            return View ();
        }

        [HttpPost ("register")]
        public IActionResult Register (User registree) {
            if (ModelState.IsValid) {
                if (dbContext.Users.Any (u => u.Email == registree.Email)) {
                    ModelState.AddModelError ("Email", "Email Address Already in System. Sorry");
                    return View ("Index");
                } else {
                    //hash the password
                    PasswordHasher<User> hash = new PasswordHasher<User> ();
                    registree.Password = hash.HashPassword (registree, registree.Password);
                    //add the registree to the db
                    dbContext.Users.Add (registree);

                    //save the datase
                    dbContext.SaveChanges ();
                    //go back to the login page and make them log in
                    return RedirectToAction ("Login");
                    //make them login again (first time)
                }
            } else {
                return View ("Index");
            }

        }

        [HttpPost ("SignIn")]
        public IActionResult Signin (LoginUser loginner)
        //take in a loginner--a login user trying to log in from the LoginUser class
        {
            if (ModelState.IsValid) {
                User checkmatch = dbContext.Users.FirstOrDefault (u => u.Email == loginner.LoginEmail);
                //declare a User from the db, call him checkmatch....
                //check checkmatch (from the database )to see if he exists. Hope that's not null. If it is....
                if (checkmatch == null) {
                    ModelState.AddModelError ("LoginEmail", "Your Email or Password is invalid");
                    return View ("Login");
                    //add an error message and redirect back to login page

                } else {
                    //check to see if passwords match
                    PasswordHasher<LoginUser> compare = new PasswordHasher<LoginUser> ();
                    var result = compare.VerifyHashedPassword (loginner, checkmatch.Password, loginner.LoginPassword);
                    if (result == 0) //this means that they don't match.
                    {
                        ModelState.AddModelError ("LoginEmail", "Your Email or Password is invalid");
                        return View ("Login");
                    } else {
                        HttpContext.Session.SetInt32 ("UserId", checkmatch.UserId);
                        //IF it works out, and the emails and psaswords match, store the checkmatch user's id in session (above);
                        return RedirectToAction ("Dashboard");
                        //this is where ya go when ya log in successfully. might need to change it
                    }

                }

            } else {
                return View ("Login");
            }

        }

        //query up a user with the ID "UserID" and store him in session

        [HttpGet ("login")]
        public IActionResult Login () {
            return View ();
        }

        [HttpGet ("logout")]
        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index");
        }

        //#######END OF LOGIN###############END OF LOGIN##############END OF LOGIN##############END OF LOGIN
        private User LoggedIn () {
            User LoggedIn = dbContext.Users.FirstOrDefault (u => u.UserId == HttpContext.Session.GetInt32 ("UserId"));
            return LoggedIn;
        }

        [HttpGet ("dashboard")]
        public IActionResult Dashboard () {
            User userinDB = LoggedIn ();
            if (userinDB == null) {
                return RedirectToAction ("Logout");
            }

            List<Incident> Incidents = dbContext.Incidents.Include (i => i.dispatchedUnits).ThenInclude (p => p.UnitDispatched).ToList ();
            ViewBag.UserLoggedIn = LoggedIn ();
            List<Unit> AvailableUnits = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true).OrderBy (i => i.NumberType).ThenBy (i => i.Number).ToList ();
            ViewBag.AvailableUnits = AvailableUnits;
            ViewBag.AllUnits = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned);
            List<Unit> UnitsOnCall = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.calls.Count == 1).ToList ();
            ViewBag.UnitsOnCall = UnitsOnCall;
            ViewBag.OOS = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.calls.Count == 0 && u.IsAvailable == false);
            return View (Incidents);
        }

        [HttpPost ("createincident")]
        public IActionResult CreateIncident () {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            ViewBag.Dispatcher = LoggedIn ();
            return View ();
        }

        [HttpPost ("createrescuer")]
        public IActionResult CreateRescuer () {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            ViewBag.Dispatcher = LoggedIn ();
            return View ();
        }

        //Add an Incident to DB

        [HttpPost ("addincident")]
        public IActionResult AddIncident (Incident newIncident) {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            if (ModelState.IsValid) {
                dbContext.Incidents.Add (newIncident);
                dbContext.SaveChanges ();
                List<Incident> Incidents = dbContext.Incidents.Include (i => i.dispatchedUnits).ThenInclude (p => p.UnitDispatched).ToList ();
                ViewBag.UserLoggedIn = LoggedIn ();
                newIncident.IsActive = true;
                ViewBag.AvailableUnits = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true);
                ViewBag.AllUnits = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned);
                return View ("Dashboard", Incidents);

            } else {
                ViewBag.Dispatcher = LoggedIn ();
                return View ("CreateIncident");

            }
        }

        [HttpPost ("addrescuer")]
        public IActionResult AddRescuer (Rescuer newRescuer) {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            if (ModelState.IsValid) {
                dbContext.Add (newRescuer);
                dbContext.SaveChanges ();
                ViewBag.UserLoggedIn = LoggedIn ();
                return RedirectToAction ("Dashboard");

            } else {
                ViewBag.Dispatcher = LoggedIn ();
                return View ("CreateRescuer");
            }
        }

        [HttpPost ("createunit")]
        public IActionResult CreateUnit () {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            ViewBag.Dispatcher = LoggedIn ();
            return View ();

        }

        [HttpPost ("addunit")]
        public IActionResult AddUnit (Unit newUnit) {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            if (ModelState.IsValid) {
                Console.WriteLine ("hi ari");
                dbContext.Add (newUnit);
                dbContext.SaveChanges ();
                ViewBag.UserLoggedIn = LoggedIn ();
                return RedirectToAction ("Dashboard");

            } else {
                ViewBag.Dispatcher = LoggedIn ();
                Console.WriteLine ("oops, wrong");
                return RedirectToAction ("CreateUnit");
            }
        }

        [HttpGet ("unit/{unitId}")]
        public IActionResult ViewUnit (int unitId) {

            ViewBag.AvailableUnits = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true);

            Unit Unittodisplay = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (a => a.RiderAssigned)
                .Include (u => u.calls)
                .ThenInclude (c => c.DispatchedIncident)
                .FirstOrDefault (u => u.UnitId == unitId);

            // ViewBag.Assignments=dbContext.Anrssignments
            // .Include(u=>u.personnel)
            // .ThenInclude(a=>a.RiderAssigned)
            // .Include(u=>u.calls)
            // .ThenInclude(c=>c.DispatchedIncident)
            // .ToList();
            return View (Unittodisplay);

        }

        [HttpPost ("inactivate/{unitId}")]
        public IActionResult InactivateUnit (int unitId) {
            Unit unitToInactivate = dbContext.Units.FirstOrDefault (u => u.UnitId == unitId);
            unitToInactivate.IsAvailable = false;
            dbContext.SaveChanges ();
            Unit Unittodisplay = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (a => a.RiderAssigned)
                .Include (u => u.calls)
                .ThenInclude (c => c.DispatchedIncident)
                .FirstOrDefault (u => u.UnitId == unitId);
            return RedirectToAction ("ViewUnit", Unittodisplay);
        }

        [HttpPost ("activate/{unitId}")]
        public IActionResult ActivateUnit (int unitId) {
            Unit unitToAactivate = dbContext.Units.FirstOrDefault (u => u.UnitId == unitId);
            unitToAactivate.IsAvailable = true;
            dbContext.SaveChanges ();
            Unit Unittodisplay = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (a => a.RiderAssigned)
                .Include (u => u.calls)
                .ThenInclude (c => c.DispatchedIncident)
                .FirstOrDefault (u => u.UnitId == unitId);
            return RedirectToAction ("ViewUnit", Unittodisplay);
        }

        [HttpGet ("addunittoincident")]
        public IActionResult AddUnitToIncident (int incidentId)

        {
            ViewBag.Dispatcher = LoggedIn ();
            ViewBag.AvailableUnits = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true);
            ViewBag.AvailableAmbulances = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true && u.NumberType == "A");
            ViewBag.AvailableMedics = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true && u.NumberType == "M");
            ViewBag.AvailableEngines = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true && u.NumberType == "E");
            ViewBag.AvailableTrucks = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true && u.NumberType == "T");
            ViewBag.AvailableRescueSquads = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true && u.NumberType == "RS");
            ViewBag.IncidentToEdit = dbContext.Incidents.Include (i => i.dispatchedUnits).ThenInclude (p => p.UnitDispatched).FirstOrDefault (i => i.IncidentId == incidentId);

            return View ();
        }

        [HttpGet ("confirmcomplete/{incidentId}")]

        public IActionResult ConfirmCompleteIncident (int incidentId) {

            Incident IncidentToRemove = dbContext.Incidents.FirstOrDefault (i => i.IncidentId == incidentId);
            return View (IncidentToRemove);
        }

        [HttpPost ("dispatch")]
        public IActionResult ProcessDispatch (Dispatchh newDispatch, int unitId)

        {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            dbContext.Dispatches.Add (newDispatch);
            dbContext.SaveChanges ();
            Unit unittoinactivate = dbContext.Units.FirstOrDefault (u => u.UnitId == newDispatch.UnitId);
            unittoinactivate.IsAvailable = false;
            dbContext.SaveChanges ();

            return RedirectToAction ("Dashboard");
        }

        [HttpGet ("confirmdispatch/{unitId}/{incidentId}")]

        public IActionResult Confirm (int unitId, int incidentId) {
            Unit unitTodispatch = dbContext.Units.FirstOrDefault (u => u.UnitId == unitId);
            ViewBag.IncidentToaddTo = dbContext.Incidents.FirstOrDefault (i => i.IncidentId == incidentId);
            return View (unitTodispatch);
        }

        [HttpGet ("confirmremoval/{unitId}/{incidentId}")]
        public IActionResult ConfirmRemoval (int unitId, int incidentId) {
            Unit unitToremove = dbContext.Units.FirstOrDefault (u => u.UnitId == unitId);
            ViewBag.IncidentRemovedFrom = dbContext.Incidents.FirstOrDefault (i => i.IncidentId == incidentId);
            return View (unitToremove);

        }

        [HttpPost ("cancelremoval")]
        public IActionResult CancelRemoval () {
            return RedirectToAction ("Dashboard");
        }

        [HttpPost ("removeunit/{unitId}/{incidentId}")]
        public IActionResult RemoveUnit (int unitId, int incidentId)

        {
            Dispatchh dispatchToremove = dbContext.Dispatches.FirstOrDefault (d => d.IncidentId == incidentId && d.UnitId == unitId);
            dbContext.Dispatches.Remove (dispatchToremove);
            Unit unitTorestore = dbContext.Units.FirstOrDefault (u => u.UnitId == dispatchToremove.UnitId);
            unitTorestore.IsAvailable = true;
            dbContext.SaveChanges ();
            return RedirectToAction ("Dashboard");
        }

        [HttpPost ("availablepersonnel")]
        public IActionResult AvailablePersonnel () {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            ViewBag.AvailableRescuers = dbContext.Rescuers
                .Include (r => r.AssignedUnit)
                .ThenInclude (a => a.UnitAssigned)
                .Where (r => r.IsAssigned == false).ToList ();
            ViewBag.UserLoggedIn = LoggedIn ();
            ViewBag.AllRescuers = dbContext.Rescuers
                .Include (r => r.AssignedUnit)
                .ThenInclude (a => a.UnitAssigned)
                .ToList ();

            return View ();
        }

        ///Assign Rescuer
        [HttpGet ("AddRescuer/{rescuerId}")]
        public IActionResult AddRescuerToUnit (int rescuerId) {

            ViewBag.Ambulances = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.NumberType == "A");
            ViewBag.Medics = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.NumberType == "M");
            ViewBag.Engines = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.NumberType == "E");
            ViewBag.Trucks = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.NumberType == "T");
            ViewBag.RescueSquads = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.NumberType == "RS");
            ViewBag.RescuerToAdd = dbContext.Rescuers.FirstOrDefault (r => r.RescuerId == rescuerId);
            return View ();
        }

        [HttpPost ("processassignment")]
        public IActionResult ProcessAssignment (Assignment newAssignment) {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            dbContext.Assignments.Add (newAssignment);
            Rescuer rescuerToupdate = dbContext.Rescuers.FirstOrDefault (r => r.RescuerId == newAssignment.RescuerId);
            rescuerToupdate.IsAssigned = true;
            dbContext.SaveChanges ();
            Console.WriteLine ("######$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Unit UnitToDisplay = newAssignment.UnitAssigned;
            // int unitId= UnitToDisplay.UnitId;
            ViewBag.AvailableRescuers = dbContext.Rescuers
                .Include (r => r.AssignedUnit)
                .ThenInclude (a => a.UnitAssigned)
                .Where (r => r.IsAssigned == false).ToList ();

            ViewBag.UserLoggedIn = LoggedIn ();

            ViewBag.AllRescuers = dbContext.Rescuers
                .Include (r => r.AssignedUnit)
                .ThenInclude (a => a.UnitAssigned)
                .ToList ();
            // return View("AvailablePersonnel");
            return RedirectToAction ("ViewUnit", new { unitId = newAssignment.UnitAssigned.UnitId });

        }

        [HttpGet ("removerescuer/{unitId}/{rescuerId}")]
        public IActionResult RemoveRescuerFromUnit (int unitId, int rescuerId) {
            Assignment assignmentToremove = dbContext.Assignments.FirstOrDefault (a => a.UnitId == unitId && a.RescuerId == rescuerId);
            dbContext.Assignments.Remove (assignmentToremove);
            dbContext.SaveChanges ();
            Rescuer rescuertoupdate = dbContext.Rescuers.FirstOrDefault (r => r.RescuerId == rescuerId);
            rescuertoupdate.IsAssigned = false;
            dbContext.SaveChanges ();

            return RedirectToAction ("ViewUnit", new { unitId = unitId });

        }

        public IActionResult CompleteIncident (int incidentId)

        {
            //Change Incident Status to InActive
            Incident incidentTocomplete = dbContext.Incidents.FirstOrDefault (i => i.IncidentId == incidentId);
            incidentTocomplete.IsActive = false;
            dbContext.SaveChanges ();

            //Take Every Unit on that incident and change status IsAvailable to true
            ViewBag.Dispatches = dbContext.Dispatches
                .Include (u => u.DispatchedIncident)
                .Include (u => u.UnitDispatched)
                .Where (u => u.IncidentId == incidentId).ToList ();

            foreach (var u in @ViewBag.Dispatches) {
                dbContext.Dispatches.Remove (u);
                u.UnitDispatched.IsAvailable = true;
                dbContext.SaveChanges ();
            }
            dbContext.SaveChanges ();

            return RedirectToAction ("Dashboard");

        }

        public IActionResult ViewRescuer (int rescuerId)

        {
            Rescuer rescuerToview = dbContext.Rescuers
                .Include (r => r.AssignedUnit)
                .ThenInclude (a => a.UnitAssigned)
                .FirstOrDefault (r => r.RescuerId == rescuerId);

            ViewBag.UserLoggedIn = LoggedIn ();
            return View (rescuerToview);
        }

        // [HttpGet("unit/{unitId}")]
        //     public IActionResult ViewUnit(int unitId)
        //     {

        //         ViewBag.AvailableUnits=dbContext.Units.Include(u=>u.personnel).ThenInclude(p=>p.RiderAssigned).Where(u=>u.IsAvailable==true);

        //         Unit Unittodisplay=dbContext.Units
        //         .Include(u=>u.personnel)
        //         .ThenInclude(a=>a.RiderAssigned)
        //         .Include(u=>u.calls)
        //         .ThenInclude(c=>c.DispatchedIncident)
        //         .FirstOrDefault(u=>u.UnitId==unitId);
        //         return View(Unittodisplay);

        //     }

        public IActionResult Privacy () {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}