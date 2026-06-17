<%@ Page Title="Enquiry Item Details" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="enquiry_item_details.aspx.cs" Inherits="IWPS.enquiry_item_details" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Enquiry Item Details</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-3"><asp:TextBox ID="txtEnqNo" runat="server" CssClass="form-control" placeholder="Enquiry Number"></asp:TextBox></div>
      <div class="col-md-2"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
    </div>
    <asp:GridView ID="gvItems" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="ENQ_NO" HeaderText="Enq No" />
        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
        <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" />
        <asp:BoundField DataField="UOM" HeaderText="UOM" />
        <asp:BoundField DataField="QTY" HeaderText="Qty" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
