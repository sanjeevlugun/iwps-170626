<%@ Page Title="HOD Category Approval" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="hod_category_master_approval.aspx.cs" Inherits="IWPS.hod_category_master_approval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
    <div class="iwps-card-header">HOD Category Master Approval</div>
    <div class="card-body p-2">
        <asp:GridView ID="gvList" runat="server" CssClass="iwps-custom-grid" AutoGenerateColumns="false" Width="100%" GridLines="None" OnRowCommand="gvList_RowCommand">
            <Columns>
                <asp:BoundField DataField="CATEGORY_CODE" HeaderText="Cat Code" />
                <asp:BoundField DataField="CATEGORY_TITLE" HeaderText="Title" />
                <asp:BoundField DataField="TYPE_OF_WORK" HeaderText="Type of Work" />
                <asp:BoundField DataField="WORK_SERVICE_TYPE" HeaderText="Work Service Type" />
                <asp:BoundField DataField="DEPT" HeaderText="Dept" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate><span class='status-badge status-<%# Eval("STATUS").ToString().ToLower() %>'><%# Eval("STATUS") %></span></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate><asp:TextBox ID="txtRemarks" runat="server" CssClass="iwps-input-edit" Width="150px"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button runat="server" Text="FORWARD TO WCD" CssClass="btn btn-xs btn-iwps-primary" CommandName="FORWARD" CommandArgument='<%# Eval("CATEGORY_CODE") %>' />
                        <asp:Button runat="server" Text="REJECT" CssClass="btn btn-xs btn-danger ml-1" CommandName="REJECT" CommandArgument='<%# Eval("CATEGORY_CODE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><div class="p-3 text-center text-muted">No initiated categories pending approval.</div></EmptyDataTemplate>
        </asp:GridView>
    </div>
</div>
</asp:Content>
