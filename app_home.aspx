<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="app_home.aspx.cs" Inherits="IWPS.app_home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
    <div class="iwps-card-header">APP Dashboard - Pending Approvals</div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-3">
                <div class="iwps-card text-center p-3">
                    <div style="font-size:36px;font-weight:bold;color:#003366;"><asp:Label ID="lblPendingIndent" runat="server" Text="0"></asp:Label></div>
                    <div style="font-size:13px;color:#666;">Pending Indent Approvals</div>
                    <div style="font-size:24px;margin-top:5px;">&#128203;</div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="iwps-card text-center p-3">
                    <div style="font-size:36px;font-weight:bold;color:#003366;"><asp:Label ID="lblPendingEnq" runat="server" Text="0"></asp:Label></div>
                    <div style="font-size:13px;color:#666;">Pending Enquiry Approvals</div>
                    <div style="font-size:24px;margin-top:5px;">&#128269;</div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="iwps-card text-center p-3">
                    <div style="font-size:36px;font-weight:bold;color:#003366;"><asp:Label ID="lblPendingLOI" runat="server" Text="0"></asp:Label></div>
                    <div style="font-size:13px;color:#666;">Pending LOI Approvals</div>
                    <div style="font-size:24px;margin-top:5px;">&#128196;</div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="iwps-card text-center p-3">
                    <div style="font-size:36px;font-weight:bold;color:#003366;"><asp:Label ID="lblPendingWO" runat="server" Text="0"></asp:Label></div>
                    <div style="font-size:13px;color:#666;">Pending WO Approvals</div>
                    <div style="font-size:24px;margin-top:5px;">&#128193;</div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-6">
        <div class="iwps-card">
            <div class="iwps-card-header">Recent Indents</div>
            <div class="card-body p-2">
                <asp:GridView ID="gvRecentIndents" runat="server" CssClass="iwps-custom-grid" AutoGenerateColumns="false" Width="100%" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="INDENT_NO" HeaderText="Indent No" />
                        <asp:BoundField DataField="INDENT_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="TITLE" HeaderText="Title" />
                        <asp:BoundField DataField="INDENT_DEPT" HeaderText="Dept" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <span class='status-badge status-<%# Eval("STATUS") != null ? Eval("STATUS").ToString().ToLower() : "" %>'><%# Eval("STATUS") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate><div class="p-3 text-center text-muted">No records found.</div></EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="iwps-card">
            <div class="iwps-card-header">Recent Work Orders</div>
            <div class="card-body p-2">
                <asp:GridView ID="gvRecentWOs" runat="server" CssClass="iwps-custom-grid" AutoGenerateColumns="false" Width="100%" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="WO_NO" HeaderText="WO No" />
                        <asp:BoundField DataField="WO_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="NATURE_OF_WORK" HeaderText="Nature of Work" />
                        <asp:BoundField DataField="DEPT" HeaderText="Dept" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <span class='status-badge status-<%# Eval("WO_STATUS") != null ? Eval("WO_STATUS").ToString().ToLower() : "" %>'><%# Eval("WO_STATUS") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate><div class="p-3 text-center text-muted">No records found.</div></EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</div>
</asp:Content>
