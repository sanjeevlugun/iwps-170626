<%@ Page Title="List of Categories" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="list_category_master.aspx.cs" Inherits="IWPS.list_category_master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
    <div class="iwps-card-header">List of Categories</div>
    <div class="card-body">
        <div class="row mb-3">
            <div class="col-md-3">
                <asp:Label runat="server" CssClass="form-label-custom" Text="Filter by Status:"></asp:Label>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                    <asp:ListItem Value="ALL">All Status</asp:ListItem>
                    <asp:ListItem Value="DRAFT">DRAFT</asp:ListItem>
                    <asp:ListItem Value="INITIATED">INITIATED</asp:ListItem>
                    <asp:ListItem Value="FORWARDED">FORWARDED</asp:ListItem>
                    <asp:ListItem Value="APPROVED">APPROVED</asp:ListItem>
                    <asp:ListItem Value="ACTIVE">ACTIVE</asp:ListItem>
                    <asp:ListItem Value="INACTIVE">INACTIVE</asp:ListItem>
                    <asp:ListItem Value="REJECTED">REJECTED</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-3 d-flex align-items-end">
                <a href="category_master.aspx" class="btn btn-sm btn-iwps-primary">+ New Category</a>
            </div>
        </div>
        <asp:GridView ID="gvList" runat="server" CssClass="iwps-custom-grid" AutoGenerateColumns="false" Width="100%" GridLines="None">
            <Columns>
                <asp:BoundField DataField="CAT_CODE" HeaderText="Category Code" />
                <asp:BoundField DataField="CAT_TITLE" HeaderText="Title" />
                <asp:BoundField DataField="TYPE_OF_WORK" HeaderText="Type of Work" />
                <asp:BoundField DataField="WORK_SERVICE_TYPE" HeaderText="Work Service Type" />
                <asp:BoundField DataField="DEPT" HeaderText="Dept" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span class='status-badge status-<%# Eval("STATUS") != null ? Eval("STATUS").ToString().ToLower() : "" %>'><%# Eval("STATUS") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="CAT_CODE" DataNavigateUrlFormatString="~/category_master.aspx?id={0}" ControlStyle-CssClass="btn btn-xs btn-outline-primary" />
            </Columns>
            <EmptyDataTemplate><div class="p-3 text-center text-muted">No records found.</div></EmptyDataTemplate>
        </asp:GridView>
    </div>
</div>
</asp:Content>
