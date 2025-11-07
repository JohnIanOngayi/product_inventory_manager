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
            }
        }

        private void fillProductsDropDown()
        {
            prodsDrpDwn.DataTextField = "Name";
            prodsDrpDwn.DataValueField = "Id";
            List<Product> products = _inventoryManager.GetProducts();
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
            int selectedValue = Convert.ToInt32(prodsDrpDwn.SelectedValue);
            var prod = _inventoryManager.GetProduct(selectedValue);
            txtProductRate.Text = prod.Rate.ToString();
        }
    }
}