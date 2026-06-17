<%@ Page Title="Draft Enquiry List" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="draft_enquiry_list.aspx.cs" Inherits="IWPS.draft_enquiry_list" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
  <div class="iwps-card-header">ENQUIRY LIST</div>
  <div class="p-3">
    <div class="row mb-2">
      <div class="col-md-3">
        <label class="form-label-custom">Filter by Status</label>
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="iwps-input-edit" style="width:100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_Changed">
          <asp:ListItem Value="">-- All --</asp:ListItem>
          <asp:ListItem Value="DRAFT">DRAFT</asp:ListItem>
          <asp:ListItem Value="INITIATED">INITIATED</asp:ListItem>
          <asp:ListItem Value="APPROVED">APPROVED</asp:ListItem>
          <asp:ListItem Value="PART-I">PART-I</asp:ListItem>
          <asp:ListItem Value="PART-II">PART-II</asp:ListItem>
          <asp:ListItem Value="LOI">LOI</asp:ListItem>
          <asp:ListItem Value="WO">WO</asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-md-2 d-flex align-items-end">
        <a href="category_wise_initiate_enquiry.aspx" class="btn-iwps-primary" style="text-decoration:none;padding:6px 16px;border-radius:4px;">+ NEW ENQUIRY</a>
      </div>
    </div>
    <div class="iwps-grid-container">
      <div class="iwps-grid-caption">ENQUIRY LIST</div>
      <asp:GridView ID="gvList" runat="server" CssClass="table table-bordered iwps-custom-grid mb-0"
        AutoGenerateColumns="false" Width="100%">
        <Columns>
          <asp:BoundField DataField="ENQUIRY_NO" HeaderText="Enquiry No" />
          <asp:BoundField DataField="ENQUIRY_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
          <asp:BoundField DataField="TITLE" HeaderText="Title" />
          <asp:BoundField DataField="CATEGORY_CODE" HeaderText="Category" />
          <asp:BoundField DataField="ENQUIRY_DEPT" HeaderText="Dept" />
          <asp:BoundField DataField="TOTAL_ESTIMATE" HeaderText="Estimate" DataFormatString="{0:N2}" />
          <asp:BoundField DataField="STATUS" HeaderText="Status" />
          <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
              <a href='category_wise_initiate_enquiry.aspx?id=<%# Eval("ENQUIRY_NO") %>' class="btn-slim-primary">EDIT</a>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate><div style="padding:15px;text-align:center;color:#999;font-style:italic;">No enquiries found</div></EmptyDataTemplate>
      </asp:GridView>
    </div>
  </div>
</div>
</asp:Content>
