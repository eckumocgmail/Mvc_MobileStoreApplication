 

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoolfOrb_OrderCheckout.Models { 
    public class DeliveryOrderViewModel
    {

        public List<OrderItem> Available { get; set; } = new List<OrderItem>();
        public List<OrderItem> Selected { get; set; } = new List<OrderItem>();
        public List<string> AvailableItems { get; set; } = new List<string>();
        public int PreviewItem { get; set; } = -1;
        public string SelectedItem { get; set; } = "все";        
        public int SelectedProvider { get; set; } = -1;        

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SearchQuery { get; set; }
        public int NextPage()
        {
            return CurrentPage = CurrentPage + 1;
        }
        public int PrevPage ()
        {
            return CurrentPage = CurrentPage - 1;
        }

        public float GetSumm()
        {
            float sum = 0f;
            Selected.ForEach(i =>
            {
                sum += i.Quantity;
            });
            return sum;
        }
    }
}
