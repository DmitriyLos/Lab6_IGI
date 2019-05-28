using lab6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab6.ViewModels
{
    public class OrdersViewModel
    {
        public int OrderID { get; set; }
        public int CarID { get; set; }
        public string Model { get; set; }
        public int WorkerID { get; set; }
        public string FioWorker { get; set; }
        public DateTime DateReceipt { get; set; }
        public DateTime? DateCompletion { get; set; }
    }
}
