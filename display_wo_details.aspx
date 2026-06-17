<%@ Page Title="WO Details" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="display_wo_details.aspx.cs" Inherits="IWPS.display_wo_details" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm mb-3">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Work Order Details</h5></div>
  <div class="card-body">
    <div class="row">
      <div class="col-md-3"><strong>WO No:</strong> <asp:Label ID="lblWONo" runat="server"></asp:Label></div>
      <div class="col-md-3"><strong>ENQ No:</strong> <asp:Label ID="lblEnqNo" runat="server"></asp:Label></div>
      <div class="col-md-3"><strong>LOI No:</strong> <asp:Label ID="lblLoiNo" runat="server"></asp:Label></div>
      <div class="col-md-3"><strong>Status:</strong> <asp:Label ID="lblStatus" runat="server"></asp:Label></div>
      <div class="col-md-3 mt-2"><strong>Contract Value:</strong> <asp:Label ID="lblCV" runat="server"></asp:Label></div>
      <div class="col-md-3 mt-2"><strong>WO Date:</strong> <asp:Label ID="lblDate" runat="server"></asp:Label></div>
      <div class="col-md-3 mt-2"><strong>Start Date:</strong> <asp:Label ID="lblStart" runat="server"></asp:Label></div>
      <div class="col-md-3 mt-2"><strong>End Date:</strong> <asp:Label ID="lblEnd" runat="server"></asp:Label></div>
      <div class="col-12 mt-2"><strong>Description:</strong> <asp:Label ID="lblDesc" runat="server"></asp:Label></div>
    </div>
  </div>
</div>
<div class="card shadow-sm mb-3">
  <div class="card-header"><strong>Items</strong></div>
  <div class="card-body p-0">
    <asp:GridView ID="gvItems" runat="server" CssClass="table table-bordered table-sm mb-0" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
        <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" />
        <asp:BoundField DataField="UOM" HeaderText="UOM" />
        <asp:BoundField DataField="QTY" HeaderText="Qty" />
        <asp:BoundField DataField="UNIT_RATE" HeaderText="Rate" DataFormatString="{0:N2}" />
        <asp:BoundField DataField="AMOUNT" HeaderText="Amount" DataFormatString="{0:N2}" />
      </Columns>
    </asp:GridView>
  </div>
</div>
<div class="card shadow-sm">
  <div class="card-header"><strong>Process Log</strong></div>
  <div class="card-body p-0">
    <asp:GridView ID="gvLog" runat="server" CssClass="table table-bordered table-sm mb-0" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="ACTION" HeaderText="Action" />
        <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />
        <asp:BoundField DataField="ACTIONED_BY" HeaderText="By" />
        <asp:BoundField DataField="ACTION_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
