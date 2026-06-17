<%@ Page Title="Initiate Indent" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="initiate_indent.aspx.cs" Inherits="IWPS.initiate_indent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function fillItemRate() {
            var ddl = document.getElementById('<%= ddlItem.ClientID %>');
            var txtRate = document.getElementById('<%= txtRate.ClientID %>');
            if (ddl.selectedIndex <= 0) { txtRate.value = ''; return; }
            // Rate auto-fill is handled server-side via postback; JS placeholder
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
    <div class="iwps-card-header">Initiate Indent</div>
    <div class="card-body">

        <%-- Section 1: Indent Header --%>
        <div class="section-title">Indent Header</div>

        <div class="row mb-2">
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Indent No"></asp:Label>
                <asp:TextBox ID="txtIndentNo" runat="server" CssClass="form-control iwps-input-readonly" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Date"></asp:Label>
                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control iwps-input-readonly" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label-custom iwps-label-required" Text="Title"></asp:Label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control iwps-input-edit" MaxLength="200"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-2">
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Indent Dept"></asp:Label>
                <asp:TextBox ID="txtIndentDept" runat="server" CssClass="form-control iwps-input-readonly" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Enquiry Dept"></asp:Label>
                <asp:DropDownList ID="ddlEnqDept" runat="server" CssClass="form-control">
                    <asp:ListItem Value="CIVIL">CIVIL</asp:ListItem>
                    <asp:ListItem Value="ELECT">ELECT</asp:ListItem>
                    <asp:ListItem Value="MECH">MECH</asp:ListItem>
                    <asp:ListItem Value="IT">IT</asp:ListItem>
                    <asp:ListItem Value="HR">HR</asp:ListItem>
                    <asp:ListItem Value="ACCOUNTS">ACCOUNTS</asp:ListItem>
                    <asp:ListItem Value="ADMIN">ADMIN</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Category"></asp:Label>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Total Estimate"></asp:Label>
                <asp:TextBox ID="txtTotalEstimate" runat="server" CssClass="form-control iwps-input-readonly" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Remarks"></asp:Label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control iwps-input-edit" MaxLength="500"></asp:TextBox>
            </div>
        </div>

        <asp:HiddenField ID="hfIndentNo" runat="server" />
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

        <%-- Section 2: Indent Items --%>
        <div class="section-title mt-3">Indent Items</div>

        <div class="row mb-2 align-items-end">
            <div class="col-md-5">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Item"></asp:Label>
                <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Qty"></asp:Label>
                <asp:TextBox ID="txtQty" runat="server" CssClass="form-control iwps-input-edit" MaxLength="10"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Rate"></asp:Label>
                <asp:TextBox ID="txtRate" runat="server" CssClass="form-control iwps-input-readonly" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnAddItem" runat="server" Text="ADD ITEM" CssClass="btn btn-iwps-primary btn-sm" OnClick="btnAddItem_Click" />
            </div>
        </div>

        <asp:GridView ID="gvItems" runat="server" CssClass="iwps-custom-grid" AutoGenerateColumns="false"
            Width="100%" GridLines="None" OnRowCommand="gvItems_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="SL No">
                    <ItemTemplate><%# Eval("SL_NO") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate><%# Eval("ITEM_CODE") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate><%# Eval("ITEM_DESCRIPTION") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate><%# Eval("QTY") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate">
                    <ItemTemplate><%# Eval("RATE") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Value">
                    <ItemTemplate><%# string.Format("{0:N2}", Eval("VALUE")) %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remove">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text="Remove" CssClass="btn btn-xs btn-danger"
                            CommandName="REMOVE" CommandArgument='<%# Container.DataItemIndex %>'
                            OnClientClick="return confirm('Remove this item?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div class="p-3 text-center text-muted">No items added yet.</div>
            </EmptyDataTemplate>
        </asp:GridView>

        <div class="mt-3">
            <asp:Button ID="btnSaveDraft" runat="server" Text="SAVE DRAFT" CssClass="btn btn-secondary mr-2" OnClick="btnSaveDraft_Click" />
            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn btn-iwps-primary mr-2" OnClick="btnSubmit_Click"
                OnClientClick="return confirm('Submit this indent?');" />
            <asp:Button ID="btnClear" runat="server" Text="CLEAR" CssClass="btn btn-outline-secondary" OnClick="btnClear_Click"
                OnClientClick="return confirm('Clear all data?');" />
        </div>

    </div>
</div>
</asp:Content>
