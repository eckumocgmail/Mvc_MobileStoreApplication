using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoolfOrb_OrderCheckout.Views.Shared.Components.Checkout
{
    public class CheckoutViewComponent: ViewComponent
    {
       

        public IViewComponentResult Invoke(CheckoutModel Model)
        {

            return View(Model);
        }

        
    }
}
