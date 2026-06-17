<%@ Page Title="Indent Item Details" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="indent_items_details.aspx.cs" Inherits="IWPS.indent_items_details" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Indent Item Details</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-3"><asp:TextBox ID="txtIndNo" runat="server" CssClass="form-control" placeholder="Indent Number"></asp:TextBox></div>
      <div class="col-md-2"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
    </div>
    <asp:GridView ID="gvItems" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="INDENT_NO" HeaderText="Indent No" />
        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
        <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
        <asp:BoundField DataField="UOM" HeaderText="UOM" />
        <asp:BoundField DataField="QTY" HeaderText="Qty" />
        <asp:BoundField DataField="EST_UNIT_RATE" HeaderText="Est Rate" DataFormatString="{0:N2}" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
