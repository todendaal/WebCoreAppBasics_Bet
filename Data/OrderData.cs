using System;
using System.Linq;
using Dapper;
using System.Data;
using WebCoreAppBasics.Models;
using System.Collections.Generic;

namespace WebCoreAppBasics.Data
{
    public class OrderData
    {
        private readonly Helper _Helper = new Helper();

        public Order insert_NewOrder(Guid UserID)
        {
            Order newOrder = new Order();
            newOrder.Id = Guid.NewGuid();
            newOrder.UserId = UserID;
            newOrder.OrderDate = DateTime.Now;
            newOrder.OrderTotal = 0;
            newOrder.OrderStatusId = 1;
            newOrder.TaxPrice = 0;
            newOrder.SuccessDate = null;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                connection.Execute("dbo.Order_newOrder @Id, @UserId, @OrderDate, @OrderTotal, @OrderStatusId, @TaxPrice, @SuccessDate", newOrder);
                return newOrder;
            }

        }

        public Order get_CurrentOrder(Guid UserID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                var output = connection.Query<Order>("dbo.Order_CurrentOrder @UserId", new { UserID });
                if (output.Count() != 0)
                {
                    setOrderList(output.First());
                    return output.First();
                }
                else
                {
                    return null;
                }
            }
        }

        public Order? get_Order(Guid OrderID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                var output = connection.Query<Order>("dbo.Order_get @OrderID", new { OrderID });
                if (output.Count() != 0)
                {
                    setOrderList(output.First());
                    return output.First();
                }
                else
                {
                    return null;
                }
            }
        }

        public OrderProducts insert_OrderProduct(Guid OrderID, Guid ProductMealID, int Quantity)
        {
            OrderProducts newOrderProduct = new OrderProducts();
            newOrderProduct.Id = Guid.NewGuid();
            newOrderProduct.OrderId = OrderID;
            newOrderProduct.ProductMealId = ProductMealID;
            newOrderProduct.Quantity = Quantity;

            ProductData _prodDb = new ProductData();
            ProductMeals PMeal = _prodDb.get_Meal(ProductMealID);

            newOrderProduct.ProductName = PMeal.Name;
            newOrderProduct.ProductPrice = PMeal.Price;
            newOrderProduct.TotalPrice = Quantity * PMeal.Price;
            newOrderProduct.OrderDate = DateTime.Now;
            newOrderProduct.Size = PMeal.Size;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                connection.Execute("dbo.Order_AddNewProduct @Id,@OrderId,@ProductMealId,@ProductName,@Size,@ProductPrice,@TotalPrice,@Quantity,@OrderDate", newOrderProduct);

                //remember to update the actual Order Data

                return newOrderProduct;
            }
        }

        public List<OrderProducts> get_CurrentOrderList(Guid OrderID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                var output = connection.Query<OrderProducts>("dbo.Order_CurrentProducts @OrderID", new { OrderID }).ToList();
                return output;
            }
        }

        private void setOrderList(Order CurrentOrder)
        {
            CurrentOrder.OrderList = get_CurrentOrderList(CurrentOrder.Id);
        }

        public void Update_OrderProductQuantity(Guid OrderItemID, int Quantity)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                connection.Execute("dbo.Order_UpdateProductQuantity @OrderItemID, @Quantity", new { OrderItemID, Quantity });
            }
        }

        public void Cancel_Order(Guid OrderId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                connection.Execute("dbo.Order_CancelOrder @OrderID", new { OrderId });
            }
        }

        public void Finalise_Order(Guid OrderId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_Helper.CnnVal("DefaultConnection")))
            {
                connection.Execute("dbo.Order_FinaliseOrder @OrderID", new { OrderId });
            }
        }
    }
}
