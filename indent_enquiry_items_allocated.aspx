<%@ Page Title="Indent-Enquiry Item Allocation" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="indent_enquiry_items_allocated.aspx.cs" Inherits="IWPS.indent_enquiry_items_allocated" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Indent-Enquiry Item Allocation Report</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-3"><asp:TextBox ID="txtIndNo" runat="server" CssClass="form-control" placeholder="Indent Number"></asp:TextBox></div>
      <div class="col-md-2"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
    </div>
    <asp:GridView ID="gvData" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="INDENT_NO" HeaderText="Indent No" />
        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
        <asp:BoundField DataField="IND_QTY" HeaderText="Indent Qty" />
        <asp:BoundField DataField="ENQ_NO" HeaderText="Enquiry No" />
        <asp:BoundField DataField="ENQ_QTY" HeaderText="Enquiry Qty" />
        <asp:BoundField DataField="ENQ_STATUS" HeaderText="Enq Status" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
