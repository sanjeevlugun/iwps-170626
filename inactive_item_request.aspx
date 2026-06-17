<%@ Page Title="Inactivate Item" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="inactive_item_request.aspx.cs" Inherits="IWPS.inactive_item_request" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="cph_maindiv" runat="server">
  <div class="container-fluid">
    <h4>Request to Inactivate Item</h4>
    <div class="card mb-3"><div class="card-body">
      <div class="row">
        <div class="col-md-4">
          <label>Select Active Item</label>
          <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control">
            <asp:ListItem Value="">-- Select Item --</asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="col-md-6">
          <label>Reason</label>
          <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"/>
        </div>
      </div>
      <div class="mt-3"><asp:Button ID="btnSubmit" runat="server" Text="Submit Request" CssClass="btn btn-warning" OnClick="btnSubmit_Click"/></div>
    </div></div>
  </div>
</asp:Content>
