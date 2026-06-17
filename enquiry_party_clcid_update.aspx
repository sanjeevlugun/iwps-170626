<%@ Page Title="Update CLC ID" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="enquiry_party_clcid_update.aspx.cs" Inherits="IWPS.enquiry_party_clcid_update" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="cph_maindiv" runat="server">
  <div class="container-fluid">
    <h4>Update CLC ID on Enquiry Parties</h4>
    <div class="row mb-3">
      <div class="col-md-4">
        <label>Select Enquiry</label>
        <asp:DropDownList ID="ddlEnquiry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiry_SelectedIndexChanged">
          <asp:ListItem Value="">-- Select Enquiry --</asp:ListItem>
        </asp:DropDownList>
      </div>
    </div>
    <asp:Panel ID="pnlGrid" runat="server" Visible="false">
      <asp:GridView ID="gvParties" runat="server" CssClass="table table-bordered table-sm" AutoGenerateColumns="false" DataKeyNames="ENQ_PARTY_ID" EmptyDataText="No parties found.">
        <Columns>
          <asp:BoundField DataField="ENQ_PARTY_ID" HeaderText="Party ID" ReadOnly="true"/>
          <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name" ReadOnly="true"/>
          <asp:TemplateField HeaderText="CLC ID">
            <ItemTemplate><asp:TextBox ID="txtClc" runat="server" CssClass="form-control form-control-sm" Text='<%# Eval("CLC_ID") %>' Width="150px"/></ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
      <asp:Button ID="btnSave" runat="server" Text="Save CLC IDs" CssClass="btn btn-success" OnClick="btnSave_Click"/>
    </asp:Panel>
  </div>
</asp:Content>
