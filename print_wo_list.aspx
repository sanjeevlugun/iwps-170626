<%@ Page Title="Print WO List" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="print_wo_list.aspx.cs" Inherits="IWPS.print_wo_list" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Print Work Order List</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-3"><asp:TextBox ID="txtWONo" runat="server" CssClass="form-control" placeholder="WO Number"></asp:TextBox></div>
      <div class="col-md-3">
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
          <asp:ListItem Text="-- All Status --" Value=""></asp:ListItem>
          <asp:ListItem Text="DRAFT"></asp:ListItem><asp:ListItem Text="INITIATED"></asp:ListItem>
          <asp:ListItem Text="APPROVED"></asp:ListItem><asp:ListItem Text="FINAL-APPROVED"></asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-md-2"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
    </div>
    <asp:GridView ID="gvWO" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="WO_NO" HeaderText="WO No" />
        <asp:BoundField DataField="ENQ_NO" HeaderText="Enq No" />
        <asp:BoundField DataField="LOI_NO" HeaderText="LOI No" />
        <asp:BoundField DataField="CONTRACT_VALUE" HeaderText="Contract Value" DataFormatString="{0:N2}" />
        <asp:BoundField DataField="STATUS" HeaderText="Status" />
        <asp:BoundField DataField="WO_DATE" HeaderText="WO Date" DataFormatString="{0:dd-MMM-yyyy}" />
        <asp:TemplateField HeaderText="Print">
          <ItemTemplate>
            <a href='wo_print.aspx?id=<%# Eval("WO_NO") %>' target='_blank' class='btn btn-xs btn-secondary'>Print</a>
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
