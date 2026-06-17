<%@ Page Title="Approve WO Amendment" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="app_wo_amend_req.aspx.cs" Inherits="IWPS.app_wo_amend_req" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">WO Amendment Requests</h5></div>
  <div class="card-body">
    <asp:GridView ID="gvAmend" runat="server" CssClass="table table-bordered table-hover table-sm"
      AutoGenerateColumns="false" DataKeyNames="AMEND_ID" OnRowCommand="gvAmend_RowCommand">
      <Columns>
        <asp:BoundField DataField="WO_NO" HeaderText="WO No" />
        <asp:BoundField DataField="AMEND_REASON" HeaderText="Reason" />
        <asp:BoundField DataField="NEW_END_DATE" HeaderText="New End Date" DataFormatString="{0:dd-MMM-yyyy}" />
        <asp:BoundField DataField="NEW_CONTRACT_VALUE" HeaderText="New CV" DataFormatString="{0:N2}" />
        <asp:BoundField DataField="STATUS" HeaderText="Status" />
        <asp:TemplateField HeaderText="Remarks">
          <ItemTemplate><asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control form-control-sm" Width="150px"></asp:TextBox></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
          <ItemTemplate>
            <asp:LinkButton runat="server" CommandName="APR" CommandArgument='<%# Eval("AMEND_ID") %>' CssClass="btn btn-xs btn-success">Approve</asp:LinkButton>
            <asp:LinkButton runat="server" CommandName="REJ" CommandArgument='<%# Eval("AMEND_ID") %>' CssClass="btn btn-xs btn-danger">Reject</asp:LinkButton>
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
