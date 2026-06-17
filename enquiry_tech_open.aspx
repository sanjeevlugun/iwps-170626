<%@ Page Title="Enquiry Part-I Open" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="enquiry_tech_open.aspx.cs" Inherits="IWPS.enquiry_tech_open" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
  <div class="iwps-card-header">ENQUIRY - TECHNICAL OPEN (PART-I)</div>
  <div class="p-3">
    <asp:Label ID="lblMsg" runat="server" style="color:green;font-weight:bold;font-size:13px;display:block;margin-bottom:8px;"></asp:Label>
    <div class="section-title">APPROVED ENQUIRIES (TWO/THREE PART BID)</div>
    <asp:GridView ID="gvEnq" runat="server" CssClass="table table-bordered iwps-custom-grid mb-0"
      AutoGenerateColumns="false" Width="100%" OnRowCommand="gvEnq_RowCommand" DataKeyNames="ENQUIRY_NO">
      <Columns>
        <asp:BoundField DataField="ENQUIRY_NO" HeaderText="Enquiry No" />
        <asp:BoundField DataField="ENQUIRY_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
        <asp:BoundField DataField="TITLE" HeaderText="Title" />
        <asp:BoundField DataField="BID_EVALUATION_METHOD" HeaderText="Bid Method" />
        <asp:BoundField DataField="STATUS" HeaderText="Status" />
        <asp:TemplateField HeaderText="Action">
          <ItemTemplate>
            <asp:Button ID="btnOpen" runat="server" Text="OPEN PART-I" CssClass="btn-slim-primary" CommandName="OPEN" CommandArgument='<%# Eval("ENQUIRY_NO") %>'
              Visible='<%# Eval("STATUS").ToString()=="APPROVED" %>' />
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
      <EmptyDataTemplate><div style="padding:15px;text-align:center;color:#999;font-style:italic;">No approved enquiries with two/three part bid</div></EmptyDataTemplate>
    </asp:GridView>
    <div id="divTechEval" runat="server" visible="false" style="margin-top:20px;">
      <div class="section-title">TECHNICAL EVALUATION - ENQUIRY: <asp:Label ID="lblSelEnq" runat="server"></asp:Label></div>
      <asp:GridView ID="gvParties" runat="server" CssClass="table table-bordered iwps-custom-grid mb-0"
        AutoGenerateColumns="false" Width="100%" DataKeyNames="PARTY_CODE">
        <Columns>
          <asp:BoundField DataField="PARTY_CODE" HeaderText="Party Code" />
          <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name" />
          <asp:TemplateField HeaderText="EMD Status">
            <ItemTemplate>
              <asp:DropDownList ID="ddlEMD" runat="server" CssClass="grid-input-slim" SelectedValue='<%# Eval("EMD_STATUS") %>'>
                <asp:ListItem Value="Y">Y - Submitted</asp:ListItem>
                <asp:ListItem Value="N">N - Not Submitted</asp:ListItem>
              </asp:DropDownList>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Qualified">
            <ItemTemplate>
              <asp:DropDownList ID="ddlQual" runat="server" CssClass="grid-input-slim" SelectedValue='<%# Eval("QUALIFIED_STATUS") %>'>
                <asp:ListItem Value="Y">Y - Qualified</asp:ListItem>
                <asp:ListItem Value="N">N - Disqualified</asp:ListItem>
              </asp:DropDownList>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate><div style="padding:10px;text-align:center;color:#999;">No parties added</div></EmptyDataTemplate>
      </asp:GridView>
      <div style="margin-top:10px;">
        <asp:Button ID="btnSaveTech" runat="server" Text="SAVE TECHNICAL EVALUATION" CssClass="btn-iwps-success" OnClick="btnSaveTech_Click" />
      </div>
    </div>
  </div>
</div>
</asp:Content>
