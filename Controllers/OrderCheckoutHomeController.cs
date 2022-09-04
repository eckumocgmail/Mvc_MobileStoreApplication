using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoolfOrb_OrderCheckout.Models;

namespace SoolfOrb_OrderCheckout.Controllers
{
    public class UserSession
    {
        private ConcurrentDictionary<string,object> _memory = new ConcurrentDictionary<string,object>();
        public bool Has(string key)
        {
            return _memory.ContainsKey(key);
        }
        public object Get(string key)
        {
            return _memory.ContainsKey(key) ? _memory[key] : null;
        }
        public void Set(string key, object val)
        {
            _memory[key] = val;
        }
    }

    public class OrderCheckoutHomeController : Controller
    {
        private readonly ILogger<OrderCheckoutHomeController> _logger;
        private readonly ProductionDbContext _context;
        private readonly UserSession _session;

        private DeliveryOrderViewModel Model
        {
            get
            {
                if (_session.Has(nameof(DeliveryOrderViewModel)) == false)
                {
                    _session.Set(nameof(DeliveryOrderViewModel), new DeliveryOrderViewModel() { 
                        Available = _context.OrderItems.ToList(),
                        AvailableItems = (from p in _context.OrderItems select p.Name).Distinct().ToList()
                    });
                }
                return (DeliveryOrderViewModel)_session.Get(nameof(DeliveryOrderViewModel));
            }
        }

        public OrderCheckoutHomeController(ILogger<OrderCheckoutHomeController> logger, ProductionDbContext context, UserSession session)
        {
            _logger = logger;
            _context = context;
            _session = session;            
        }

        public IActionResult Update(int providerid, string item)
        {
            int id = providerid;
            Model.SelectedItem = item;
            Model.SelectedProvider = id;
            if(id != -1) {
                Model.AvailableItems = (from p in _context.OrderItems where p.ProviderID == (int)id select p.Name).Distinct().ToList();
                Model.Available = (from p in _context.OrderItems where p.ProviderID == (int)id select p).ToList();
            }
            else
            {
                Model.AvailableItems = (from p in _context.OrderItems select p.Name).Distinct().ToList();
                Model.Available = (from p in _context.OrderItems select p).Distinct().ToList();
            }       
            if(item != "все")
            {
                Model.Available = Model.Available.Where(i => i.Name == item).ToList();
            }

            return View("Index", Model);
        }

        public IActionResult Index()
        {
            return View(Model);
        }

        public IActionResult AddToOrder(int id)
        {
            var item = (from p in Model.Available where p.ID == id select p).SingleOrDefault();
            if (item != null)
            {
                Model.Available.Remove(item);
                Model.Selected.Add(item);
            }
            return View("Index", Model);
        }
        public IActionResult RemoveFromOrder(int id)
        {
            var item = (from p in Model.Selected where p.ID == id select p).Single();
            if( item != null)
            {
                Model.Selected.Remove(item);
                Model.Available.Add(item);
            }
            return View("Index", Model);
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
