using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace @namespace
{
    public class ProductOrder {
		public int IDOrder {get; set;}
		public int IDStatus {get; set;}
		public string IDPayment {get; set;}
		public string DeliveryAddress {get; set;}
		public DateTime DeliveryTime {get; set;}
		public string DisCode {get; set;}
		public string Note {get; set;}
		public bool IsAdmin {get; set;}
		public bool IsDelete {get; set;}

    }

}