<%@ Page Title="IWPS Contractor Search" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="search_iwps_contractor_master.aspx.cs" Inherits="IWPS.search_iwps_contractor_master" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Search IWPS Contractor Master</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-2"><asp:TextBox ID="txtCode" runat="server" CssClass="form-control" placeholder="Contractor Code"></asp:TextBox></div>
      <div class="col-md-3"><asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Contractor Name"></asp:TextBox></div>
      <div class="col-md-2">
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
          <asp:ListItem Text="-- All Status --" Value=""></asp:ListItem>
          <asp:ListItem Text="ACTIVE"></asp:ListItem><asp:ListItem Text="INACTIVE"></asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-md-2"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
    </div>
    <asp:GridView ID="gvCon" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="CONTRACTOR_CODE" HeaderText="Code" />
        <asp:BoundField DataField="CONTRACTOR_NAME" HeaderText="Name" />
        <asp:BoundField DataField="CONTRACTOR_EMAIL" HeaderText="Email" />
        <asp:BoundField DataField="CONTRACTOR_MOBILE" HeaderText="Mobile" />
        <asp:BoundField DataField="STATUS" HeaderText="Status" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
