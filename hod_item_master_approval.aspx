<%@ Page Title="HOD Item Approval" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="hod_item_master_approval.aspx.cs" Inherits="IWPS.hod_item_master_approval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
    <div class="iwps-card-header">HOD Item Master Approval</div>
    <div class="card-body p-2">
        <asp:GridView ID="gvList" runat="server" CssClass="iwps-custom-grid" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowCommand="gvList_RowCommand">
            <Columns>
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                <asp:BoundField DataField="ITEM_CATEGORY" HeaderText="Category" />
                <asp:BoundField DataField="ITEM_DESCRIPTION" HeaderText="Description" />
                <asp:BoundField DataField="ITEM_UNIT" HeaderText="Unit" />
                <asp:BoundField DataField="ITEM_RATE" HeaderText="Rate" DataFormatString="{0:N2}" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate><span class='status-badge status-<%# Eval("STATUS").ToString().ToLower() %>'><%# Eval("STATUS") %></span></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate><asp:TextBox ID="txtRemarks" runat="server" CssClass="iwps-input-edit" Width="150px"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button runat="server" Text="APPROVE" CssClass="btn btn-xs btn-success mr-1" CommandName="APPROVE" CommandArgument='<%# Eval("ITEM_CODE") %>' />
                        <asp:Button runat="server" Text="REJECT" CssClass="btn btn-xs btn-danger" CommandName="REJECT" CommandArgument='<%# Eval("ITEM_CODE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><div class="p-3 text-center text-muted">No initiated items pending approval.</div></EmptyDataTemplate>
        </asp:GridView>
    </div>
</div>
</asp:Content>
