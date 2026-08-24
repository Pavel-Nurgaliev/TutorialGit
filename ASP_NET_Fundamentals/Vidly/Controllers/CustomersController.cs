using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models.Customers;
using Vidly.ViewModels;

public class CustomersController : Controller
{
    private readonly List<Customer> _customerList;

    public CustomersController()
    {
        _customerList = new List<Customer>
        {
            new Customer { Id = 1, Name = "John Smith" },
            new Customer { Id = 2, Name = "Mary Williams" }
        };
    }

    public ActionResult Index()
    {
        var viewModel = new CustomersViewModel { Customers = _customerList };
        return View(viewModel);
    }

    public ActionResult Details(int id)
    {
        var customer = _customerList.SingleOrDefault(c => c.Id == id);
        if (customer == null)
            return HttpNotFound();

        return View(customer);
    }
}