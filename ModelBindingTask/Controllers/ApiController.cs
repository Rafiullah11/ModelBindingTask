using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelBindingTask.Models;

namespace ModelBindingTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApiController(AppDbContext context)
        {
            _context = context;
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
            return Ok(customer);

        }

        [HttpPost]
        [Route("FromRoute/{Id}/{FirstName}/{LastName}/{age}")]
        public IActionResult FromRoute([FromRoute] Customer model)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                message = " Customer id " + model.Id + "First Name" + model.FirstName + " Last Name " + model.LastName + " Age " + model.Age;
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
                message = "FromQuery " + " Customer id " + model.Id + "First Name" + model.FirstName + " Last Name " + model.LastName + " Age " + model.Age;

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

            return Ok(customer);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            return Ok(customer);
        }
    }

}
