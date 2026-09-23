using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyApp.Models;

namespace VidlyApp.Controllers
{
    public class CustomersController : Controller
    {
        private List<Customer> _customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe" },
                new Customer { Id = 2, Name = "Jane Smith" },
                new Customer { Id = 3, Name = "Bob Johnson" }
            };

        // GET: Customers
        public ActionResult Index()
        {
            return View(_customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
    }
}