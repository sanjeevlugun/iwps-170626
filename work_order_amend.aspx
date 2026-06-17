<%@ Page Title="WO Amendment Request" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="work_order_amend.aspx.cs" Inherits="IWPS.work_order_amend" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Work Order Amendment Request</h5></div>
  <div class="card-body">
    <div class="row">
      <div class="col-md-4 form-group">
        <label>Select Approved WO</label>
        <asp:DropDownList ID="ddlWO" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWO_Changed">
          <asp:ListItem Text="-- Select WO --" Value=""></asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-md-4 form-group">
        <label>Current End Date</label>
        <asp:TextBox ID="txtCurEnd" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
      </div>
      <div class="col-md-4 form-group">
        <label>Current Contract Value</label>
        <asp:TextBox ID="txtCurCV" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
      </div>
      <div class="col-md-4 form-group">
        <label>New End Date</label>
        <asp:TextBox ID="txtNewEnd" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
      </div>
      <div class="col-md-4 form-group">
        <label>New Contract Value</label>
        <asp:TextBox ID="txtNewCV" runat="server" CssClass="form-control"></asp:TextBox>
      </div>
      <div class="col-md-4 form-group">
        <label>Amendment Reason</label>
        <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
      </div>
    </div>
    <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT AMENDMENT" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
  </div>
</div>
</asp:Content>
