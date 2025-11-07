using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using product_inventory_manager.Models;

namespace product_inventory_manager.Infrastructure
{
    public class Repository
    {
        private readonly string _connectionString = @"Server=127.0.0.1;Port=3306;Database=csm_7th_november;Uid=root;Pwd=;";
        private readonly string _storedProcedure = "sp_prod_ops";
        public int InsertProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(_storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("prodId", DBNull.Value);
                cmd.Parameters.AddWithValue("prodName", prod.Name);
                cmd.Parameters.AddWithValue("prodRate", prod.Rate);
                cmd.Parameters.AddWithValue("prodQuantity", prod.Quantity);
                cmd.Parameters.AddWithValue("orderId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderDate", DBNull.Value);
                cmd.Parameters.AddWithValue("orderProductId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderValue", DBNull.Value);
                cmd.Parameters.AddWithValue("action", "PI");

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(_storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("prodId", prod.Id);
                cmd.Parameters.AddWithValue("prodName", prod.Name);
                cmd.Parameters.AddWithValue("prodRate", prod.Rate);
                cmd.Parameters.AddWithValue("prodQuantity", prod.Quantity);
                cmd.Parameters.AddWithValue("orderId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderDate", DBNull.Value);
                cmd.Parameters.AddWithValue("orderProductId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderValue", DBNull.Value);
                cmd.Parameters.AddWithValue("action", "PU");

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteProduct(int prodId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(_storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("prodId", prodId);
                cmd.Parameters.AddWithValue("prodName", DBNull.Value);
                cmd.Parameters.AddWithValue("prodRate", DBNull.Value);
                cmd.Parameters.AddWithValue("prodQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderDate", DBNull.Value);
                cmd.Parameters.AddWithValue("orderProductId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderValue", DBNull.Value);
                cmd.Parameters.AddWithValue("action", "PD");

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public Product GetProduct(int prodId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(_storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("prodId", prodId);
                cmd.Parameters.AddWithValue("prodName", DBNull.Value);
                cmd.Parameters.AddWithValue("prodRate", DBNull.Value);
                cmd.Parameters.AddWithValue("prodQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderDate", DBNull.Value);
                cmd.Parameters.AddWithValue("orderProductId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderValue", DBNull.Value);
                cmd.Parameters.AddWithValue("action", "PS");

                using (var dataAdapter = new MySqlDataAdapter(cmd))
                {
                    var dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        return null;
                    }

                    Product product = new Product
                    {
                        Id = Convert.ToInt32(dataTable.Rows[0]["Id"]),
                        Name = dataTable.Rows[0]["Name"].ToString(),
                        Rate = (float)Convert.ToDouble(dataTable.Rows[0]["Rate"]),
                        Quantity = Convert.ToInt32(dataTable.Rows[0]["Quantity"])
                    };

                    return product;
                }
            }
        }

        public bool Check(int prodId)
        {
            return GetProduct(prodId) != null;
        }

        public List<Product> GetProducts()
        {
            var list = new List<Product>();
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(_storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("prodId", DBNull.Value);
                cmd.Parameters.AddWithValue("prodName", DBNull.Value);
                cmd.Parameters.AddWithValue("prodRate", DBNull.Value);
                cmd.Parameters.AddWithValue("prodQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderDate", DBNull.Value);
                cmd.Parameters.AddWithValue("orderProductId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderValue", DBNull.Value);
                cmd.Parameters.AddWithValue("action", "PA");

                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    list = (from DataRow row in dt.Rows
                            select new Product
                            {
                                Name = row["Name"].ToString(),
                                Id = Convert.ToInt32(row["Id"].ToString())
                            }).ToList();
                }
            }
            return list;
        }


        public Order GetOrder(int orderId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(_storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("prodId", DBNull.Value);
                cmd.Parameters.AddWithValue("prodName", DBNull.Value);
                cmd.Parameters.AddWithValue("prodRate", DBNull.Value);
                cmd.Parameters.AddWithValue("prodQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderId", orderId);
                cmd.Parameters.AddWithValue("orderDate", DBNull.Value);
                cmd.Parameters.AddWithValue("orderProductId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderValue", DBNull.Value);
                cmd.Parameters.AddWithValue("action", "OS");

                using (var dataAdapter = new MySqlDataAdapter(cmd))
                {
                    var dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        return null;
                    }

                    Order order = new Order
                    {
                        Id = Convert.ToInt32(dataTable.Rows[0]["Id"]),
                        OrderDate = DateTime.Parse(dataTable.Rows[0]["Date"].ToString()),
                        ProductQuantity = Convert.ToInt32(dataTable.Rows[0]["Product_Quantity"]),
                        OrderValue = (float)Convert.ToDouble(dataTable.Rows[0]["Value"].ToString()),
                        ProductId = Convert.ToInt32(dataTable.Rows[0]["Product_Id"]),
                        ProductName = dataTable.Rows[0]["Product_Name"].ToString(),
                        ProductRate = (float)Convert.ToDouble(dataTable.Rows[0]["Product_Rate"].ToString())
                    };

                    return order;
                }
            }
        }

        public List<Order> GetOrders()
        {
            var list = new List<Order>();
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(_storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("prodId", DBNull.Value);
                cmd.Parameters.AddWithValue("prodName", DBNull.Value);
                cmd.Parameters.AddWithValue("prodRate", DBNull.Value);
                cmd.Parameters.AddWithValue("prodQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderDate", DBNull.Value);
                cmd.Parameters.AddWithValue("orderProductId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderValue", DBNull.Value);
                cmd.Parameters.AddWithValue("action", "OA");

                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    list = (from DataRow row in dt.Rows
                            select new Order
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                OrderDate = DateTime.Parse(row["Date"].ToString()),
                                ProductQuantity = Convert.ToInt32(row["Product_Quantity"]),
                                OrderValue = (float)Convert.ToDouble(row["Value"].ToString()),
                                ProductId = Convert.ToInt32(row["Product_Id"]),
                                ProductName = row["Product_Name"].ToString(),
                               ProductRate = (float)Convert.ToDouble(row["Product_Rate"].ToString())
                            }).ToList();
                }
            }
            return list;
        }

        public int InsertOrder(Order order)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(_storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("prodId", DBNull.Value);
                cmd.Parameters.AddWithValue("prodName", DBNull.Value);
                cmd.Parameters.AddWithValue("prodRate", DBNull.Value);
                cmd.Parameters.AddWithValue("prodQuantity", DBNull.Value);
                cmd.Parameters.AddWithValue("orderId", DBNull.Value);
                cmd.Parameters.AddWithValue("orderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("orderProductId", order.ProductId);
                cmd.Parameters.AddWithValue("orderQuantity", order.ProductQuantity);
                cmd.Parameters.AddWithValue("orderValue", order.OrderValue);
                cmd.Parameters.AddWithValue("action", "OI");

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}