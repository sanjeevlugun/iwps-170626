<%@ Page Title="Indent Acceptance" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="Indent_list_enq_dept_approval.aspx.cs" Inherits="IWPS.Indent_list_enq_dept_approval" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
  <div class="iwps-card-header">INDENT ACCEPTANCE - ENQUIRY DEPARTMENT</div>
  <div class="p-3">
    <asp:Label ID="lblMsg" runat="server" style="color:green;font-weight:bold;font-size:13px;display:block;margin-bottom:8px;"></asp:Label>
    <div class="iwps-grid-container">
      <div class="iwps-grid-caption">FORWARDED INDENTS - PENDING ACCEPTANCE</div>
      <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered iwps-custom-grid mb-0"
        AutoGenerateColumns="false" Width="100%" OnRowCommand="gvList_RowCommand">
        <Columns>
          <asp:BoundField DataField="INDENT_NO" HeaderText="Indent No" />
          <asp:BoundField DataField="INDENT_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
          <asp:BoundField DataField="TITLE" HeaderText="Title" />
          <asp:BoundField DataField="INDENT_DEPT" HeaderText="Initiating Dept" />
          <asp:BoundField DataField="ENQUIRY_DEPT" HeaderText="Enquiry Dept" />
          <asp:BoundField DataField="TOTAL_ESTIMATE" HeaderText="Estimate" DataFormatString="{0:N2}" />
          <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate><asp:TextBox ID="txtRemarks" runat="server" CssClass="grid-input-slim" Width="150px"></asp:TextBox></ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
              <asp:Button ID="btnAcc" runat="server" Text="ACCEPT" CssClass="btn-slim-success" CommandName="ACC" CommandArgument='<%# Eval("INDENT_NO") %>' />
              <asp:Button ID="btnRet" runat="server" Text="RETURN" CssClass="btn-slim-danger" CommandName="RET" CommandArgument='<%# Eval("INDENT_NO") %>' />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate><div style="padding:15px;text-align:center;color:#999;font-style:italic;">No forwarded indents</div></EmptyDataTemplate>
      </asp:GridView>
    </div>
  </div>
</div>
</asp:Content>
