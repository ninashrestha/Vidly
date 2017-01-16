using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {

            _context.Dispose();
        }
        public ActionResult New()
        {
            var membershipType = _context.MembershipType.ToList();
            var viewModel = new NewCustomer
            {
                MembershipTypes = membershipType
            };
            return View("New",viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewCustomer
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipType.ToList()
                };
                return View("New", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerIndb = _context.Customers.Single(c => c.Id == customer.Id);
                customerIndb.Name = customer.Name;
                customerIndb.Birthdate = customer.Birthdate;
                customerIndb.MembershipTypeId = customer.MembershipTypeId;
                customerIndb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;


            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            var viewModel = new NewCustomer
            {
                Customer = customer,
                MembershipTypes = _context.MembershipType.ToList()
            };
            return View("New",viewModel);
        }

        public ViewResult Index()
        {
            
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
    }
}