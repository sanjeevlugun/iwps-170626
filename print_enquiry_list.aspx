<%@ Page Title="Enquiry List - Print" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="print_enquiry_list.aspx.cs" Inherits="IWPS.print_enquiry_list" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="cph_maindiv" runat="server">
  <div class="container-fluid">
    <h4>Enquiry List</h4>
    <div class="row mb-2">
      <div class="col-md-3"><asp:TextBox ID="txtEnqNo" runat="server" CssClass="form-control" placeholder="Enquiry No"/></div>
      <div class="col-md-3">
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
          <asp:ListItem Value="">-- All Status --</asp:ListItem>
          <asp:ListItem>INITIATED</asp:ListItem>
          <asp:ListItem>TECH-OPEN</asp:ListItem>
          <asp:ListItem>PRICE-OPEN</asp:ListItem>
          <asp:ListItem>CLOSED</asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-md-2"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click"/></div>
    </div>
    <asp:GridView ID="gvEnquiries" runat="server" CssClass="table table-bordered table-sm" AutoGenerateColumns="false" EmptyDataText="No records found.">
      <Columns>
        <asp:BoundField DataField="ENQ_NO" HeaderText="Enquiry No"/>
        <asp:BoundField DataField="INDENT_NO" HeaderText="Indent No"/>
        <asp:BoundField DataField="ENQ_TYPE" HeaderText="Type"/>
        <asp:BoundField DataField="STATUS" HeaderText="Status"/>
        <asp:BoundField DataField="CREATED_DATE" HeaderText="Created Date" DataFormatString="{0:dd-MMM-yyyy}"/>
        <asp:TemplateField HeaderText="Print">
          <ItemTemplate>
            <a href='enquiry_print.aspx?id=<%# Eval("ENQ_NO") %>' target='_blank' class='btn btn-sm btn-info'>Print</a>
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>
  </div>
</asp:Content>
