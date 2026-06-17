<%@ Page Title="Search Category Master" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="search_category_master.aspx.cs" Inherits="IWPS.search_category_master" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Search Category Master</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-2"><asp:TextBox ID="txtCode" runat="server" CssClass="form-control" placeholder="Cat Code"></asp:TextBox></div>
      <div class="col-md-3"><asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Category Name"></asp:TextBox></div>
      <div class="col-md-2">
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
          <asp:ListItem Text="-- All Status --" Value=""></asp:ListItem>
          <asp:ListItem Text="ACTIVE"></asp:ListItem><asp:ListItem Text="DRAFT"></asp:ListItem><asp:ListItem Text="INITIATED"></asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-md-2 d-flex gap-2">
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        <asp:Button ID="btnExport" runat="server" Text="CSV" CssClass="btn btn-secondary" OnClick="btnExport_Click" />
      </div>
    </div>
    <asp:GridView ID="gvCat" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="CAT_CODE" HeaderText="Code" />
        <asp:BoundField DataField="CAT_NAME" HeaderText="Name" />
        <asp:BoundField DataField="DEPT" HeaderText="Dept" />
        <asp:BoundField DataField="STATUS" HeaderText="Status" />
        <asp:BoundField DataField="CREATED_DATE" HeaderText="Created" DataFormatString="{0:dd-MMM-yyyy}" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
