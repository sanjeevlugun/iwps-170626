<%@ Page Title="Draft Indent List" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="draft_indent_list.aspx.cs" Inherits="IWPS.draft_indent_list" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
  <div class="iwps-card-header">DRAFT INDENT LIST</div>
  <div class="p-3">
    <div class="row mb-2">
      <div class="col-md-3">
        <label class="form-label-custom">Filter by Status</label>
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="iwps-input-edit" style="width:100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_Changed">
          <asp:ListItem Value="">-- All --</asp:ListItem>
          <asp:ListItem Value="DRAFT">DRAFT</asp:ListItem>
          <asp:ListItem Value="INITIATED">INITIATED</asp:ListItem>
          <asp:ListItem Value="FORWARDED">FORWARDED</asp:ListItem>
          <asp:ListItem Value="APPROVED">APPROVED</asp:ListItem>
          <asp:ListItem Value="REJECTED">REJECTED</asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-md-2 d-flex align-items-end">
        <a href="initiate_indent.aspx" class="btn-iwps-primary" style="text-decoration:none;padding:6px 16px;border-radius:4px;">+ NEW INDENT</a>
      </div>
    </div>
    <asp:Label ID="lblMsg" runat="server" style="color:green;font-size:13px;"></asp:Label>
    <div class="iwps-grid-container">
      <div class="iwps-grid-caption">INDENT LIST</div>
      <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered iwps-custom-grid mb-0"
        AutoGenerateColumns="false" Width="100%" DataKeyNames="INDENT_NO" OnRowCommand="gvList_RowCommand">
        <Columns>
          <asp:BoundField DataField="INDENT_NO" HeaderText="Indent No" />
          <asp:BoundField DataField="INDENT_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
          <asp:BoundField DataField="TITLE" HeaderText="Title" />
          <asp:BoundField DataField="INDENT_DEPT" HeaderText="Dept" />
          <asp:BoundField DataField="CATEGORY_CODE" HeaderText="Category" />
          <asp:BoundField DataField="TOTAL_ESTIMATE" HeaderText="Estimate" DataFormatString="{0:N2}" />
          <asp:BoundField DataField="STATUS" HeaderText="Status" />
          <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
              <a href='initiate_indent.aspx?id=<%# Eval("INDENT_NO") %>' class="btn-slim-primary">EDIT</a>
              <asp:Button ID="btnDel" runat="server" Text="DELETE" CssClass="btn-slim-danger"
                CommandName="DEL" CommandArgument='<%# Eval("INDENT_NO") %>'
                Visible='<%# Eval("STATUS").ToString()=="DRAFT" %>'
                OnClientClick="return confirm('Delete this indent?');" />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate><div style="padding:15px;text-align:center;color:#999;font-style:italic;">No indents found</div></EmptyDataTemplate>
      </asp:GridView>
    </div>
  </div>
</div>
</asp:Content>
