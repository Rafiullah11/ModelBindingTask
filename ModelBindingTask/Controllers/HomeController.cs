using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelBindingTask.Models;

namespace ModelBindingTask.Controllers
{
    //Sir please visit Api Controller for Route Method i implement all method in api controller is also
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FromForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FromForm([FromForm] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [HttpPost]
        [Route("FromRoute/{Id}/{FirstName}/{LastName}/{age}")]
        public IActionResult FromRoute([FromRoute] Customer model)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                message =  " Customer id " + model.Id + "First Name" + model.FirstName + " Last Name " + model.LastName + " Age " + model.Age;
            }
            else
            {
                message = "Failed to create the customer. Please try again";
            }
            return Content(message);
        }

        [HttpGet]
        public IActionResult FromQuery([FromQuery] Customer model)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                message = "FromQuery "  + " Customer id " + model.Id + "First Name" + model.FirstName + " Last Name " + model.LastName + " Age " + model.Age;

            }
            else
            {
                message = "Failed to create the customer. Please try again";
            }
            return Content(message);
        }

        [HttpPost]
        public IActionResult FromHeader([FromHeader] string firstname, [FromHeader] string lastname, [FromHeader] string age)
        {
            var customer = new Customer();
            customer.FirstName = firstname;
            customer.LastName = lastname;
            customer.Age = age;

            return View(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            return View(customer);
        }
    }

}
