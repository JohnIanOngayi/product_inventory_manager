using product_inventory_manager.Domain;
using product_inventory_manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace product_inventory_manager
{
    public partial class Inventory : System.Web.UI.Page
    {

        private readonly ProductOrderService _inventoryManager;
        private readonly List<Product> tempProductsListToStartApp = new List<Product>
        {
            new Product{Id = 1, Name = "Laptop", Rate = 899.99F, Quantity = 50 },
            new Product{ Id = 2, Name = "Mouse", Rate = 25.50F, Quantity = 100 },
            new Product{Id = 3, Name = "Keyboard", Rate = 75.00F, Quantity = 80 },
            new Product{Id = 4, Name = "Monitor", Rate = 299.99F, Quantity = 30 },
            new Product{Id = 5, Name = "Headphones", Rate = 149.99F, Quantity = 60}
        };

        public Inventory()
        {
            _inventoryManager = new ProductOrderService();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillProductsDropDown();
                fillOrdersGridView();
                txtProductRate.Text = "0";
                txtOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtOrderQuantity.Text = "1";
                txtOrderValue.Text = "0";
                lblMsg.Text = "";
            }
        }

        private void fillProductsDropDown()
        {
            prodsDrpDwn.DataTextField = "Name";
            prodsDrpDwn.DataValueField = "Id";
            List<Product> products = _inventoryManager.GetProducts();
            if (products == null || products.Count == 0)
                products = tempProductsListToStartApp;
            prodsDrpDwn.DataSource = products;
            prodsDrpDwn.DataBind();
            prodsDrpDwn.Items.Insert(0, new ListItem("-- Select Product --", ""));
            //prodsDrpDwn.Items.Insert(0, "Select A Product");
        }

        private void fillOrdersGridView()
        {
            List<Order> orders = _inventoryManager.GetOrders();
            grdvOrders.DataSource = orders;
            grdvOrders.DataBind();
        }

        protected void prodsDrpDwn_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if a valid product is selected
            if (!string.IsNullOrEmpty(prodsDrpDwn.SelectedValue) && prodsDrpDwn.SelectedValue != "")
            {
                if (int.TryParse(prodsDrpDwn.SelectedValue, out int selectedValue))
                {
                    var prod = _inventoryManager.GetProduct(selectedValue);
                    if (prod != null)
                    {
                        txtProductRate.Text = prod.Rate.ToString();
                    }
                }
            }
            else
            {
                txtProductRate.Text = string.Empty;
            }
        }

        //protected void CalculateOrderValue(object sender, EventArgs e)
        //{
        //    int newOrderRate = 0;
        //    int newQuantity = 0;

        //    // Safely parse Product Rate
        //    if (!string.IsNullOrEmpty(txtProductRate.Text) && int.TryParse(txtProductRate.Text, out newOrderRate))
        //    {
        //        // Rate parsed successfully
        //    }

        //    // Safely parse Order Quantity
        //    if (!string.IsNullOrEmpty(txtOrderQuantity.Text) && int.TryParse(txtOrderQuantity.Text, out newQuantity))
        //    {
        //        // Quantity parsed successfully
        //    }
        //    else
        //    {
        //        // Default to 1 if quantity is invalid
        //        newQuantity = 1;
        //        txtOrderQuantity.Text = "1";
        //    }

        //    // Calculate and display order value
        //    txtOrderValue.Text = (newOrderRate * newQuantity).ToString();
        //}

        protected void btnInsert_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrEmpty(prodsDrpDwn.SelectedValue) || prodsDrpDwn.SelectedValue == "")
                {
                    lblMsg.Text = "Please select a product";
                    return;
                }

                if (string.IsNullOrEmpty(txtOrderDate.Text))
                {
                    lblMsg.Text = "Please select an order date";
                    return;
                }

                if (string.IsNullOrEmpty(txtOrderQuantity.Text) || Convert.ToInt32(txtOrderQuantity.Text) <= 0)
                {
                    lblMsg.Text = "Please enter a valid quantity";
                    return;
                }

                Product product = _inventoryManager.GetProduct(Convert.ToInt32(prodsDrpDwn.SelectedValue));

                if (product == null)
                {
                    lblMsg.Text = "Product not found";
                    return;
                }

                int orderQuantity = Convert.ToInt32(txtOrderQuantity.Text);

                Order newOrder = new Order
                {
                    OrderDate = DateTime.Parse(txtOrderDate.Text),
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductQuantity = orderQuantity,
                    ProductRate = product.Rate,
                    OrderValue = product.Rate * orderQuantity,
                };

                int result = _inventoryManager.AddOrder(newOrder);

                if (result > 0)
                {
                    lblMsg.Text = "Order inserted successfully!";
                    fillOrdersGridView();
                    ResetForm();
                }
                else
                {
                    lblMsg.Text = "Failed to insert order. Please try again.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error: " + ex.Message;
            }
        }


        protected void txtOrderQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateOrderValue();
        }

        private void CalculateOrderValue()
        {
            float rate = 0;
            int quantity = 0;

            if (!string.IsNullOrEmpty(txtProductRate.Text) && float.TryParse(txtProductRate.Text, out rate))
            {
                // Rate parsed successfully
            }

            if (!string.IsNullOrEmpty(txtOrderQuantity.Text) && int.TryParse(txtOrderQuantity.Text, out quantity))
            {
                // Quantity parsed successfully
            }
            else
            {
                quantity = 1;
                txtOrderQuantity.Text = "1";
            }

            txtOrderValue.Text = (rate * quantity).ToString("0.00");
        }
        private void ResetForm()
        {
            prodsDrpDwn.SelectedIndex = 0;
            txtProductRate.Text = "0";
            txtOrderQuantity.Text = "1";
            txtOrderValue.Text = "";
        }
    }
}