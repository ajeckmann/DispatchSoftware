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

                    //save the database
                    dbContext.SaveChanges ();
                    //go back to the login page and make them log in
                    return RedirectToAction ("Index");
                    //make them login again (first time)
                }
            } else {
                return View ("Login");
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
            List<Unit> AllUnits = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).OrderBy (i => i.NumberType).ThenBy (i => i.Number).ToList ();
            ViewBag.AllUnits = AllUnits;
            List<Unit> UnitsOnCall = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.calls.Count == 1).OrderBy (u => u.NumberType).ThenBy (i => i.Number).ToList ();
            ViewBag.UnitsOnCall = UnitsOnCall;
            List<Unit> OutOfService = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.calls.Count == 0 && u.IsAvailable == false).OrderBy (u => u.NumberType).ThenBy (i => i.Number).ToList ();
            ViewBag.OOS = OutOfService;
            List<Incident> ActiveIncidents = dbContext.Incidents.Include (i => i.dispatchedUnits).ThenInclude (p => p.UnitDispatched).Where (i => i.IncidentStatus == "awaitingDispatch" | i.IncidentStatus == "dispatched").ToList ();
            ViewBag.ActiveIncidents = ActiveIncidents;
            return View (Incidents);
        }
        //Navigate to createincident page to complete form/add new incident.
        [HttpPost ("createincident")]
        public IActionResult CreateIncident () {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            ViewBag.Dispatcher = LoggedIn ();
            return View ();
        }

        [HttpGet ("editincident/{incidentId}")]
        public IActionResult EditIncident (int incidentId) {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            Incident IncidentToEdit = dbContext.Incidents.FirstOrDefault (i => i.IncidentId == incidentId);
            ViewBag.Dispatcher = LoggedIn ();
            return View (IncidentToEdit);
        }

        [HttpPost ("updateincident/{incidentId}")]
        public IActionResult UpdateIncident (int incidentId, string location, string description, string type) {
            Incident IncidentToEdit = dbContext.Incidents.FirstOrDefault (i => i.IncidentId == incidentId);
            IncidentToEdit.Location = location;
            IncidentToEdit.Description = description;
            IncidentToEdit.Type = type;
            IncidentToEdit.UpdatdAt = DateTime.Now;
            dbContext.SaveChanges ();

            return RedirectToAction ("Dashboard");
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

        [HttpGet ("editrescuer/{rescuerId}")]
        public IActionResult EditRescuer (int rescuerId) {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            ViewBag.Dispatcher = LoggedIn ();
            Rescuer RescuerToEdit = dbContext.Rescuers.FirstOrDefault (r => r.RescuerId == rescuerId);
            return View (RescuerToEdit);
        }

        [HttpPost ("updaterescuer/{rescuerId}")]
        public IActionResult UpdateRescuer (int rescuerId, string firstname, string lastname, int age, string rank) {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }

            Rescuer RescuerToEdit = dbContext.Rescuers.FirstOrDefault (r => r.RescuerId == rescuerId);
            RescuerToEdit.FirstName = firstname;
            RescuerToEdit.LastName = lastname;
            RescuerToEdit.Age = age;
            RescuerToEdit.Rank = rank;
            dbContext.SaveChanges ();
            return RedirectToAction ("ViewRescuer", RescuerToEdit);
        }

        //Add an Incident to DB. Create new instance of Incident class.

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
                ViewBag.AvailableUnits = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true);
                ViewBag.AllUnits = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned);
                return RedirectToAction ("Dashboard", Incidents);

            } else {
                ViewBag.Dispatcher = LoggedIn ();
                return View ("CreateIncident");

            }
        }
        //Navigage to createrescuer page to add a rescuer
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
                return RedirectToAction ("AvailablePersonnel");

            } else {
                ViewBag.Dispatcher = LoggedIn ();
                return View ("CreateRescuer");
            }
        }
        //Navigage to createunit page to add a new unit
        [HttpPost ("createunit")]
        public IActionResult CreateUnit () {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            ViewBag.Dispatcher = LoggedIn ();
            return View ();

        }
        //add new object to the Unit class
        [HttpPost ("addunit")]
        public IActionResult AddUnit (Unit newUnit) {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            if (ModelState.IsValid) {

                dbContext.Add (newUnit);
                dbContext.SaveChanges ();
                ViewBag.UserLoggedIn = LoggedIn ();
                return RedirectToAction ("Dashboard");

            } else {
                ViewBag.Dispatcher = LoggedIn ();
                return RedirectToAction ("CreateUnit");
            }
        }

        [HttpGet ("unit/{unitId}")]
        public IActionResult ViewUnit (int unitId) {

            // ViewBag.AvailableUnits = dbContext.Units.Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true);
            Unit Unittodisplay = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (a => a.RiderAssigned)
                .Include (u => u.calls)
                .ThenInclude (c => c.DispatchedIncident)
                .FirstOrDefault (u => u.UnitId == unitId);
            ViewBag.TimeElapsed = (DateTime.Now - Unittodisplay.CreatedAt).TotalDays;
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

        [HttpPost ("editresponsestatus/{unitId}/{status}")]
        public IActionResult EditResponseStatus (int unitId, string status) {
            Unit unitToEdit = dbContext.Units.FirstOrDefault (u => u.UnitId == unitId);
            if (status == "enroute") {
                unitToEdit.ResponseStatus = "Enroute";
            } else if (status == "on_scene") {
                unitToEdit.ResponseStatus = "On The Scene";
            } else if (status == "transport") {
                unitToEdit.ResponseStatus = "Enroute To Hospital";
            } else if (status == "at_hospital") {
                unitToEdit.ResponseStatus = "At the Hospital";
            } else if (status == "clear") {
                unitToEdit.ResponseStatus = "Clear of Incident";
            }

            dbContext.SaveChanges ();
            Unit EditedUnit = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (a => a.RiderAssigned)
                .Include (u => u.calls)
                .ThenInclude (c => c.DispatchedIncident)
                .FirstOrDefault (u => u.UnitId == unitId);
            return RedirectToAction ("ViewUnit", EditedUnit);

        }

        [HttpGet ("addunittoincident")]
        public IActionResult AddUnitToIncident (int incidentId) {
            ViewBag.Dispatcher = LoggedIn ();
            ViewBag.AvailableUnits = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true);
            ViewBag.AvailableAmbulances = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true && u.NumberType == "A");
            ViewBag.AvailableMedics = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true && u.NumberType == "M");
            ViewBag.AvailableEngines = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true && u.NumberType == "E");
            ViewBag.AvailableTrucks = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true && u.NumberType == "T");
            ViewBag.AvailableRescueSquads = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (p => p.RiderAssigned).Where (u => u.IsAvailable == true && u.NumberType == "RS");
            ViewBag.IncidentToEdit = dbContext.Incidents
                .Include (i => i.dispatchedUnits)
                .ThenInclude (p => p.UnitDispatched).FirstOrDefault (i => i.IncidentId == incidentId);

            return View ();
        }

        [HttpGet ("confirmcomplete/{incidentId}")]

        public IActionResult ConfirmCompleteIncident (int incidentId) {

            Incident IncidentToRemove = dbContext.Incidents.FirstOrDefault (i => i.IncidentId == incidentId);
            return View (IncidentToRemove);
        }

        [HttpPost ("dispatch")]
        public IActionResult ProcessDispatch (Dispatchh newDispatch, int unitId) {

            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            dbContext.Dispatches.Add (newDispatch);
            dbContext.SaveChanges ();
            Unit unittoinactivate = dbContext.Units.FirstOrDefault (u => u.UnitId == newDispatch.UnitId);
            unittoinactivate.IsAvailable = false;
            unittoinactivate.ResponseStatus = "Dispatched";
            Incident incidentToUpdate = dbContext.Incidents.FirstOrDefault (i => i.IncidentId == newDispatch.IncidentId);
            incidentToUpdate.IncidentStatus = "dispatched";
            dbContext.SaveChanges ();

            return RedirectToAction ("Dashboard");
        }

        [HttpGet ("confirmdispatch/{unitId}/{incidentId}")]

        public IActionResult Confirm (int unitId, int incidentId) {
            Unit unitTodispatch = dbContext.Units.FirstOrDefault (u => u.UnitId == unitId);
            ViewBag.IncidentToaddTo = dbContext.Incidents.FirstOrDefault (i => i.IncidentId == incidentId);
            return View (unitTodispatch);
        }

        [HttpPost ("confirmclear/{unitId}/{incidentId}")]
        public IActionResult ConfirmClearFromIncident (int unitId, int incidentId) {
            Unit unitToremove = dbContext.Units.FirstOrDefault (u => u.UnitId == unitId);
            ViewBag.IncidentRemovedFrom = dbContext.Incidents.FirstOrDefault (i => i.IncidentId == incidentId);
            return View (unitToremove);

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
            unitTorestore.ResponseStatus = "";
            dbContext.SaveChanges ();
            return RedirectToAction ("Dashboard");
        }

        [HttpGet ("availablepersonnel")]
        public IActionResult AvailablePersonnel () {
            User userinDb = LoggedIn ();
            if (userinDb == null) {
                return RedirectToAction ("Logout");
            }
            ViewBag.AvailableRescuers = dbContext.Rescuers
                .Include (r => r.AssignedUnit)
                .ThenInclude (a => a.UnitAssigned)
                .Where (r => r.IsAssigned == false)
                .OrderBy (r => r.LastName)
                .ToList ();
            ViewBag.UserLoggedIn = LoggedIn ();
            ViewBag.AllRescuers = dbContext.Rescuers
                .Include (r => r.AssignedUnit)
                .ThenInclude (a => a.UnitAssigned)
                .OrderBy (r => r.LastName)
                .ToList ();

            return View ();
        }

        ///Assign Rescuer
        [HttpGet ("AddRescuer/{rescuerId}")]
        public IActionResult AddRescuerToUnit (int rescuerId) {

            ViewBag.Ambulances = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (p => p.RiderAssigned).Where (u => u.NumberType == "A");
            ViewBag.Medics = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (p => p.RiderAssigned).Where (u => u.NumberType == "M");
            ViewBag.Engines = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (p => p.RiderAssigned).Where (u => u.NumberType == "E");
            ViewBag.Trucks = dbContext.Units
                .Include (u => u.personnel).ThenInclude (p => p.RiderAssigned).Where (u => u.NumberType == "T");
            ViewBag.RescueSquads = dbContext.Units
                .Include (u => u.personnel)
                .ThenInclude (p => p.RiderAssigned).Where (u => u.NumberType == "RS");
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
            incidentTocomplete.IncidentStatus = "complete";
            dbContext.SaveChanges ();

            //Take Every Unit on that incident and change status IsAvailable to true
            ViewBag.Dispatches = dbContext.Dispatches
                .Include (u => u.DispatchedIncident)
                .Include (u => u.UnitDispatched)
                .Where (u => u.IncidentId == incidentId).ToList ();

            foreach (var d in @ViewBag.Dispatches) {
                dbContext.Dispatches.Remove (d);
                d.UnitDispatched.IsAvailable = true;
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

        public IActionResult Privacy () {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}