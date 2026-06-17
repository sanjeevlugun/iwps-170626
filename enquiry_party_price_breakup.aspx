<%@ Page Title="Enquiry Price Breakup" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="enquiry_party_price_breakup.aspx.cs" Inherits="IWPS.enquiry_party_price_breakup" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Enquiry Party Price Breakup</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-3"><asp:TextBox ID="txtEnqNo" runat="server" CssClass="form-control" placeholder="Enquiry Number"></asp:TextBox></div>
      <div class="col-md-2"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
    </div>
    <asp:GridView ID="gvPrice" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="ENQ_NO" HeaderText="Enq No" />
        <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name" />
        <asp:BoundField DataField="QUOTED_AMOUNT" HeaderText="Quoted Amount" DataFormatString="{0:N2}" />
        <asp:BoundField DataField="RANK" HeaderText="Rank" />
        <asp:BoundField DataField="AWARD_STATUS" HeaderText="Awarded" />
        <asp:BoundField DataField="QUALIFIED_STATUS" HeaderText="Tech Status" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
