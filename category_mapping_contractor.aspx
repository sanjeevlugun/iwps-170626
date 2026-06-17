<%@ Page Title="Category-Contractor Mapping" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="category_mapping_contractor.aspx.cs" Inherits="IWPS.category_mapping_contractor" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="cph_maindiv" runat="server">
  <div class="container-fluid">
    <h4>Category-Contractor Mapping</h4>
    <div class="row mb-3">
      <div class="col-md-4">
        <label>Select Category</label>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
          <asp:ListItem Value="">-- Select Category --</asp:ListItem>
        </asp:DropDownList>
      </div>
    </div>
    <asp:Panel ID="pnlContractors" runat="server" Visible="false">
      <asp:GridView ID="gvContractors" runat="server" CssClass="table table-bordered table-sm" AutoGenerateColumns="false" DataKeyNames="CONTRACTOR_ID" EmptyDataText="No contractors.">
        <Columns>
          <asp:TemplateField HeaderText="Map">
            <ItemTemplate><asp:CheckBox ID="chkMap" runat="server" Checked='<%# Convert.ToBoolean(Eval("IS_MAPPED")) %>'/></ItemTemplate>
          </asp:TemplateField>
          <asp:BoundField DataField="CONTRACTOR_ID" HeaderText="ID"/>
          <asp:BoundField DataField="CONTRACTOR_NAME" HeaderText="Contractor Name"/>
          <asp:BoundField DataField="CONTRACTOR_TYPE" HeaderText="Type"/>
        </Columns>
      </asp:GridView>
      <asp:Button ID="btnSave" runat="server" Text="Save Mapping" CssClass="btn btn-success mt-2" OnClick="btnSave_Click"/>
    </asp:Panel>
  </div>
</asp:Content>
