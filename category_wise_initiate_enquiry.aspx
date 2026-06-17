<%@ Page Title="Category-wise Enquiry" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="category_wise_initiate_enquiry.aspx.cs" Inherits="IWPS.category_wise_initiate_enquiry" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="cph_maindiv" runat="server">
  <div class="container-fluid">
    <h4>Initiate Enquiry (Category-wise)</h4>
    <div class="card mb-3"><div class="card-header">Enquiry Header</div><div class="card-body">
      <div class="row">
        <div class="col-md-3">
          <label>Enquiry Type</label>
          <asp:DropDownList ID="ddlEnqType" runat="server" CssClass="form-control">
            <asp:ListItem Value="">-- Select --</asp:ListItem>
            <asp:ListItem>SINGLE</asp:ListItem>
            <asp:ListItem>TWO-PART</asp:ListItem>
            <asp:ListItem>THREE-PART</asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="col-md-3">
          <label>Enquiry Date</label>
          <asp:TextBox ID="txtEnqDate" runat="server" CssClass="form-control" TextMode="Date"/>
        </div>
        <div class="col-md-3">
          <label>Category</label>
          <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            <asp:ListItem Value="">-- Select Category --</asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="col-md-3">
          <label>Estimated Value</label>
          <asp:TextBox ID="txtEstValue" runat="server" CssClass="form-control" placeholder="0.00"/>
        </div>
      </div>
      <div class="row mt-2">
        <div class="col-md-8">
          <label>Description</label>
          <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"/>
        </div>
      </div>
    </div></div>

    <div class="card mb-3"><div class="card-header">Items</div><div class="card-body">
      <div class="row mb-2">
        <div class="col-md-4">
          <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control">
            <asp:ListItem Value="">-- Select Item --</asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="col-md-2">
          <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" placeholder="Qty"/>
        </div>
        <div class="col-md-2">
          <asp:Button ID="btnAddItem" runat="server" Text="Add Item" CssClass="btn btn-secondary" OnClick="btnAddItem_Click"/>
        </div>
      </div>
      <asp:GridView ID="gvItems" runat="server" CssClass="table table-bordered table-sm" AutoGenerateColumns="false" EmptyDataText="No items added.">
        <Columns>
          <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code"/>
          <asp:BoundField DataField="ITEM_DESCRIPTION" HeaderText="Description"/>
          <asp:BoundField DataField="UOM" HeaderText="UOM"/>
          <asp:BoundField DataField="QTY" HeaderText="Qty"/>
          <asp:TemplateField HeaderText="Remove">
            <ItemTemplate><asp:LinkButton ID="lbRemove" runat="server" Text="Remove" CommandName="RemoveItem" CommandArgument='<%# Container.DataItemIndex %>' OnCommand="RemoveItem_Command" CssClass="btn btn-sm btn-danger"/></ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </div></div>

    <div class="card mb-3"><div class="card-header">Parties</div><div class="card-body">
      <div class="row mb-2">
        <div class="col-md-3"><asp:TextBox ID="txtPartyName" runat="server" CssClass="form-control" placeholder="Party Name"/></div>
        <div class="col-md-3"><asp:TextBox ID="txtPartyEmail" runat="server" CssClass="form-control" placeholder="Email"/></div>
        <div class="col-md-2"><asp:TextBox ID="txtPartyMobile" runat="server" CssClass="form-control" placeholder="Mobile"/></div>
        <div class="col-md-2">
          <asp:DropDownList ID="ddlEmd" runat="server" CssClass="form-control">
            <asp:ListItem Value="N">EMD Not Required</asp:ListItem>
            <asp:ListItem Value="Y">EMD Required</asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="col-md-2"><asp:Button ID="btnAddParty" runat="server" Text="Add Party" CssClass="btn btn-secondary" OnClick="btnAddParty_Click"/></div>
      </div>
      <asp:GridView ID="gvParties" runat="server" CssClass="table table-bordered table-sm" AutoGenerateColumns="false" EmptyDataText="No parties added.">
        <Columns>
          <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name"/>
          <asp:BoundField DataField="PARTY_EMAIL" HeaderText="Email"/>
          <asp:BoundField DataField="PARTY_MOBILE" HeaderText="Mobile"/>
          <asp:BoundField DataField="EMD_REQD" HeaderText="EMD"/>
          <asp:TemplateField HeaderText="Remove">
            <ItemTemplate><asp:LinkButton ID="lbRemoveP" runat="server" Text="Remove" CommandName="RemoveParty" CommandArgument='<%# Container.DataItemIndex %>' OnCommand="RemoveParty_Command" CssClass="btn btn-sm btn-danger"/></ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
    </div></div>

    <asp:Button ID="btnSubmit" runat="server" Text="Submit Enquiry" CssClass="btn btn-success btn-lg" OnClick="btnSubmit_Click"/>
  </div>
</asp:Content>
