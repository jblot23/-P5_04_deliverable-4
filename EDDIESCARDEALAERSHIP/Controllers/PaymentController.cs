using EDDIESCARDEALAERSHIP.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDDIESCARDEALAERSHIP.Controllers
{
    public class PaymentController : Controller
    {
        // GET: /Payment/Checkout
        public IActionResult Checkout()
        {
            // Display the checkout page with an empty CheckoutViewModel
            var model = new CheckoutViewModel();
            return View(model);
        }

        // POST: /Payment/Checkout
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Process the checkout logic based on the model data
                // For example, you might save the payment information to a database or process a financing application
                // Redirect to a success page or return a confirmation message
                return RedirectToAction("CheckoutSuccess");
            }
            else
            {
                // If the model is not valid, return the view with validation errors
                return View(model);
            }
        }

        // GET: /Payment/CheckoutSuccess
        public IActionResult CheckoutSuccess()
        {
            // Display the checkout success page
            return View();
        }
    }
}
