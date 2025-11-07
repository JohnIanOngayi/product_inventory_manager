using product_inventory_manager.Infrastructure;
using product_inventory_manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product_inventory_manager.Domain
{
    public class ProductOrderService
    {
        private readonly Repository _repository;

        public ProductOrderService()
        {
                _repository = new Repository();
        }

        public int AddProduct(Product prod)
        {
            try
            {
                return _repository.InsertProduct(prod);
            }
            catch
            {
                return 0;
            }
        }

        public int AddOrder(Order order)
        {
            try
            {
                return _repository.InsertOrder(order);
            }
            catch
            {
                return 0;
                throw;
            }
        }

        public Product GetProduct(int prodId)
        {
            return _repository.GetProduct(prodId);
        }

        public Order GetOrder(int orderId)
        {
            return _repository.GetOrder(orderId);
        }

        public List<Product> GetProducts()
        {
            return _repository.GetProducts();
        }

        public List<Order> GetOrders()
        {
            return _repository.GetOrders();
        }

        public int HandleOrder(Order order)
        {
            Product prod = _repository.GetProduct(order.ProductId);
            int newProdQuantity = prod.Quantity - order.ProductQuantity;
            prod.Quantity = newProdQuantity;
            int x = _repository.UpdateProduct(prod);
            int y = _repository.InsertOrder(order);

            return x > 0 && y > 0 ? 1 : 0;
        }
    }
}