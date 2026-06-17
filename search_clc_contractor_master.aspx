<%@ Page Title="CLC Contractor Search" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="search_clc_contractor_master.aspx.cs" Inherits="IWPS.search_clc_contractor_master" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Search CLC Contractor Master</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-2"><asp:TextBox ID="txtCLCId" runat="server" CssClass="form-control" placeholder="CLC ID"></asp:TextBox></div>
      <div class="col-md-3"><asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Party Name"></asp:TextBox></div>
      <div class="col-md-2"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
    </div>
    <asp:GridView ID="gvCLC" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="CLC_ID" HeaderText="CLC ID" />
        <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name" />
        <asp:BoundField DataField="PARTY_EMAIL" HeaderText="Email" />
        <asp:BoundField DataField="PARTY_MOBILE" HeaderText="Mobile" />
        <asp:BoundField DataField="STATUS" HeaderText="Status" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
