using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortageSimulation.Models;
using ShortageSimulation.ShortageEF;
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new List<ShortageViewModel>();
            viewModel=await _customerServices.GetAllFgNames();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Calculate(string []FGName, int [] OrderQty)
        {
            //Request.Form.TryGetValue("FGName", out var nameList);
            //var array=nameList.ToArray();
            var FgOrder=new List<SalesOrder>();
            for (int i = 0; i < FGName.Length; i++)
            {
                var fg=new SalesOrder();
                fg.FName= FGName[i];
                fg.OrderQty=OrderQty[i];
                FgOrder.Add(fg);
            }
            //總訂單數量
            var Orders= FgOrder.GroupBy(x => x.FName).Select(y => new SalesOrder 
            { 
                OrderQty=y.Sum(x=>x.OrderQty),
                FName=y.Key
            }).ToList();
            var allFGs = await _customerServices.GetAllFgGoods();
            var Mrps = new List<MRPqty>();
            foreach (var item in Orders)
            {
                var mrp = from a in allFGs
                          where a.Fname == item.FName
                          select new MRPqty
                          {
                              Mname = a.Mname,
                              Usage = a.Usage* item.OrderQty
                          };
                foreach (var item2 in mrp)
                {
                    Mrps.Add(item2);
                }    
            }
            //MRP總需求
            var MrpList = Mrps.GroupBy(m => m.Mname).Select(x => new MRPqty 
            {
                Mname=x.Key,
                Usage= x.Sum(z=>z.Usage)
            }).ToList();
            //計算缺料
            var stocks=await _customerServices.GetAllMaterialStocks();
            var shortageModel = new List<ShortageViewModel>();
            foreach (var item in MrpList)
            {
                
                int stock = (int)(from x in stocks where x.Mname == item.Mname select x.Stocks).ToArray()[0];
                var model = new ShortageViewModel();
                model.Demands = item.Usage;
                model.Mname= item.Mname;
                model.Stocks = stock;
                model.ShortageQty= stock- item.Usage;
                if (model.ShortageQty < 0)
                {
                    model.Remark = "缺料";
                }
                else
                {
                    model.ShortageQty = 0;
                }
                shortageModel.Add(model);
            }
            var viewModel = new ResultViewModel();

            viewModel.orders = Orders;
            viewModel.shortages= shortageModel;

            return View(viewModel);
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
