<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="product_inventory_manager.Inventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory Manager</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="1" align="center">
                <tr>
                    <th colspan="2">Inventory Manager</th>
                </tr>

                <tr>
                    <td>Products</td>
                    <td>
                        <asp:DropDownList ID="prodsDrpDwn" runat="server"></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>Product Rate</td>
                    <td>
                        <asp:TextBox ID="txtProductRate" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Order Date</td>
                    <td>
                        <asp:TextBox ID="txtOrderDate" runat="server" TextMode="Date"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Order Quantity</td>
                    <td>
                        <asp:TextBox ID="txtOrderQuantity" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Order Value</td>
                    <td>
                        <asp:TextBox ID="txtOrderValue" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label></td>
                    <td>
                        <asp:Button ID="btnInsert" runat="server" Text="Insert" />

                        <asp:Button ID="btnReset" runat="server" Text="Reset" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="grdvOrders" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Order Id"></asp:BoundField>
                                <asp:BoundField DataField="OrderDate" HeaderText="Order Date"></asp:BoundField>
                                <asp:BoundField DataField="ProductName" HeaderText="Product"></asp:BoundField>
                                <asp:BoundField DataField="ProductQuantity" HeaderText="Quantity"></asp:BoundField>
                                <asp:BoundField DataField="ProductRate" HeaderText="Rate"></asp:BoundField>
                                <asp:BoundField DataField="OrderValue" HeaderText="Total"></asp:BoundField>
                                <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
