using System;

namespace ParseOrderData.Models
{
    public class HeaderRecord
    {
        public string PurchaseOrderNumber { get; set; }
        public string Supplier { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime CargoReadyDate { get; set; }
    }
}