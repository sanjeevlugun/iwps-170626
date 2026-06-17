<%@ Page Title="Dept-wise Categories" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="deptwise_category_master.aspx.cs" Inherits="IWPS.deptwise_category_master" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Department-wise Category Master</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-3">
        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_Changed">
          <asp:ListItem Text="-- Select Department --" Value=""></asp:ListItem>
        </asp:DropDownList>
      </div>
    </div>
    <asp:GridView ID="gvCat" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="CAT_CODE" HeaderText="Code" />
        <asp:BoundField DataField="CAT_NAME" HeaderText="Name" />
        <asp:BoundField DataField="STATUS" HeaderText="Status" />
        <asp:BoundField DataField="ITEM_COUNT" HeaderText="Item Count" />
        <asp:BoundField DataField="CREATED_DATE" HeaderText="Created" DataFormatString="{0:dd-MMM-yyyy}" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
