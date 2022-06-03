using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelBindingTask.Models;

namespace ModelBindingTask.Controllers
{
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

        public IActionResult Create()
        {
            return View();
        }

        // Complex Model Binding
        // Form fields
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult FromRoute()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FromRoute([FromRoute]string id, Customer model)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                message = "Route " + id + " Customer id " + model.Id +  " Rafi " + model.FirstName + " Ullah " + model.LastName + " Age " + model.Age;
            }
            else
            {
                message = "Failed to create the customer. Please try again";
            }
            return Content(message);
        }

        [HttpGet]
        public IActionResult FormAndQuery()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FormAndQuery([FromQuery]string name, Customer model)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                message = "Query string " + name + " Rafi " + model.FirstName + " Ullah " + model.LastName + " Age " + model.Age;
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
