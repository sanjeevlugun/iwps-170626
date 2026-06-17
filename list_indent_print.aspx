<%@ Page Title="Print Indent" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="list_indent_print.aspx.cs" Inherits="IWPS.list_indent_print" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
  <div class="iwps-card-header">PRINT INDENT</div>
  <div class="p-3">
    <div class="row mb-2">
      <div class="col-md-3">
        <label class="form-label-custom">Search Indent No</label>
        <asp:TextBox ID="txtSearch" runat="server" CssClass="iwps-input-edit" style="width:100%;"></asp:TextBox>
      </div>
      <div class="col-md-2 d-flex align-items-end">
        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" CssClass="btn-iwps-primary" OnClick="btnSearch_Click" />
      </div>
    </div>
    <div class="iwps-grid-container">
      <div class="iwps-grid-caption">INDENT LIST</div>
      <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered iwps-custom-grid mb-0"
        AutoGenerateColumns="false" Width="100%">
        <Columns>
          <asp:BoundField DataField="INDENT_NO" HeaderText="Indent No" />
          <asp:BoundField DataField="INDENT_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
          <asp:BoundField DataField="TITLE" HeaderText="Title" />
          <asp:BoundField DataField="INDENT_DEPT" HeaderText="Dept" />
          <asp:BoundField DataField="STATUS" HeaderText="Status" />
          <asp:TemplateField HeaderText="Print">
            <ItemTemplate>
              <a href='#' onclick='window.open("indent_print.aspx?id=<%# Eval("INDENT_NO") %>","_blank","width=900,height=600");return false;' class="btn-slim-primary">PRINT</a>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate><div style="padding:15px;text-align:center;color:#999;font-style:italic;">No indents found</div></EmptyDataTemplate>
      </asp:GridView>
    </div>
  </div>
</div>
</asp:Content>
