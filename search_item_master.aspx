<%@ Page Title="Search Item Master" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="search_item_master.aspx.cs" Inherits="IWPS.search_item_master" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Search Item Master</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-2"><asp:TextBox ID="txtCode" runat="server" CssClass="form-control" placeholder="Item Code"></asp:TextBox></div>
      <div class="col-md-3"><asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Item Name"></asp:TextBox></div>
      <div class="col-md-3">
        <asp:DropDownList ID="ddlCat" runat="server" CssClass="form-control">
          <asp:ListItem Text="-- All Categories --" Value=""></asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-md-2">
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
          <asp:ListItem Text="-- All Status --" Value=""></asp:ListItem>
          <asp:ListItem Text="ACTIVE"></asp:ListItem><asp:ListItem Text="INACTIVE"></asp:ListItem><asp:ListItem Text="DRAFT"></asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-md-2 d-flex gap-2">
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        <asp:Button ID="btnExport" runat="server" Text="CSV" CssClass="btn btn-secondary" OnClick="btnExport_Click" />
      </div>
    </div>
    <asp:GridView ID="gvItems" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
        <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
        <asp:BoundField DataField="CAT_CODE" HeaderText="Category" />
        <asp:BoundField DataField="UOM" HeaderText="UOM" />
        <asp:BoundField DataField="STATUS" HeaderText="Status" />
        <asp:BoundField DataField="CREATED_DATE" HeaderText="Created" DataFormatString="{0:dd-MMM-yyyy}" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
