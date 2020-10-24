using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DockerCoffee.Web.Models;
using DockerCoffee.Shared.Contracts;

namespace DockerCoffee.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBeverageService _beverageService;
        private readonly IOrderService _orderService;

        public HomeController(IBeverageService beverageService, IOrderService orderService)
        {
            _beverageService = beverageService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View(_beverageService.GetAll());
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderViewModel vieModel)
        {
            var success = _orderService.PlaceOrder(vieModel.BeverageId, vieModel.Customer);

            TempData["Notice"] = success ? "Successfully placed order" : "Failed to place order";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Orders()
        {
            return View(_orderService.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
