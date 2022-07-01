using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortageSimulation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShortageSimulation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerServices _customerServices;

        public HomeController(ILogger<HomeController> logger, ICustomerServices customerServices)
        {
            _logger = logger;
            _customerServices = customerServices;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new List<ShortageViewModel>();
            viewModel=await _customerServices.GetAllFinishedGoods();
            return View(viewModel);
        }
        public IActionResult Calculate(string []FGName, string []qty)
        {
            Request.Form.TryGetValue("FGName", out var nameList);
            var array=nameList.ToArray();
            return View();
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
