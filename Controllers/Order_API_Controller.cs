using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebCoreAppBasics.Models;
using WebCoreAppBasics.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebCoreAppBasics.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class Order_API_Controller : ControllerBase
    {
        private readonly OrderData _db = new OrderData();

        [HttpGet()]
        public Order? NewOrder(string UserID)
        {
            Console.WriteLine("UserID=" + UserID);
            try
            {
                var itm = _db.insert_NewOrder(Guid.Parse(UserID));
                return itm;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet()]
        public Order? GetOrder(string OrderId)
        {
            try
            {
                var itm = _db.get_Order(Guid.Parse(OrderId));
                return itm;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet()]
        public bool AddOrderProduct(string OrderID, string MealProductID, int Quantity)
        {
            try
            {
                var itm = _db.insert_OrderProduct(Guid.Parse(OrderID), Guid.Parse(MealProductID), Quantity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet()]
        public bool Order_UpdateProductQuantity(string ItemID, int Quantity)
        {
            try
            {
                _db.Update_OrderProductQuantity(Guid.Parse(ItemID), Quantity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet()]
        public bool Order_Cancel(string OrderID)
        {
            try
            {
                _db.Cancel_Order(Guid.Parse(OrderID));
                return true;
            }
            catch
            {
                return false;
            }
        }

        [Authorize]
        [HttpGet()]
        public bool Order_Finalise(string OrderID)
        {
            try
            {
                _db.Finalise_Order(Guid.Parse(OrderID));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
