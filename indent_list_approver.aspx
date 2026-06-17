<%@ Page Title="Indent Approval" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="indent_list_approver.aspx.cs" Inherits="IWPS.indent_list_approver" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
  <div class="iwps-card-header">INDENT APPROVAL - HOD</div>
  <div class="p-3">
    <asp:Label ID="lblMsg" runat="server" style="color:green;font-weight:bold;font-size:13px;display:block;margin-bottom:8px;"></asp:Label>
    <div class="iwps-grid-container">
      <div class="iwps-grid-caption">INITIATED INDENTS PENDING APPROVAL</div>
      <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered iwps-custom-grid mb-0"
        AutoGenerateColumns="false" Width="100%" OnRowCommand="gvList_RowCommand">
        <Columns>
          <asp:BoundField DataField="INDENT_NO" HeaderText="Indent No" />
          <asp:BoundField DataField="INDENT_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
          <asp:BoundField DataField="TITLE" HeaderText="Title" />
          <asp:BoundField DataField="INDENT_DEPT" HeaderText="Dept" />
          <asp:BoundField DataField="ENQUIRY_DEPT" HeaderText="Enquiry Dept" />
          <asp:BoundField DataField="CATEGORY_CODE" HeaderText="Category" />
          <asp:BoundField DataField="TOTAL_ESTIMATE" HeaderText="Estimate" DataFormatString="{0:N2}" />
          <asp:BoundField DataField="STATUS" HeaderText="Status" />
          <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate><asp:TextBox ID="txtRemarks" runat="server" CssClass="grid-input-slim" Width="150px"></asp:TextBox></ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
              <asp:Button ID="btnApp" runat="server" Text="APPROVE" CssClass="btn-slim-success" CommandName="APP" CommandArgument='<%# Eval("INDENT_NO") %>' />
              <asp:Button ID="btnFwd" runat="server" Text="FORWARD" CssClass="btn-slim-warning" CommandName="FWD" CommandArgument='<%# Eval("INDENT_NO") %>' />
              <asp:Button ID="btnRej" runat="server" Text="REJECT" CssClass="btn-slim-danger" CommandName="REJ" CommandArgument='<%# Eval("INDENT_NO") %>' />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate><div style="padding:15px;text-align:center;color:#999;font-style:italic;">No pending indent approvals</div></EmptyDataTemplate>
      </asp:GridView>
    </div>
  </div>
</div>
</asp:Content>
