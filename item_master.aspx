<%@ Page Title="Item Master" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="item_master.aspx.cs" Inherits="IWPS.item_master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
    <div class="iwps-card-header">Item Master</div>
    <div class="card-body">
        <asp:HiddenField ID="hfItemCode" runat="server" Value="" />
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" Text="" Visible="false"></asp:Label>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="form-label-custom" Text="Item Code:"></asp:Label>
                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="iwps-input-readonly" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="iwps-label-required" Text="Category"></asp:Label>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control iwps-input-edit" Width="100%"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="form-label-custom" Text="DSR Code:"></asp:Label>
                    <asp:TextBox ID="txtDSRCode" runat="server" CssClass="iwps-input-edit" Width="100%"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="iwps-label-required" Text="Description"></asp:Label>
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="iwps-input-edit" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="iwps-label-required" Text="Unit"></asp:Label>
                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control iwps-input-edit" Width="100%">
                        <asp:ListItem Value="">-- Select --</asp:ListItem>
                        <asp:ListItem Value="Nos">Nos</asp:ListItem>
                        <asp:ListItem Value="Sqm">Sqm</asp:ListItem>
                        <asp:ListItem Value="Cum">Cum</asp:ListItem>
                        <asp:ListItem Value="Rmt">Rmt</asp:ListItem>
                        <asp:ListItem Value="Job">Job</asp:ListItem>
                        <asp:ListItem Value="Month">Month</asp:ListItem>
                        <asp:ListItem Value="Set">Set</asp:ListItem>
                        <asp:ListItem Value="Hrs">Hrs</asp:ListItem>
                        <asp:ListItem Value="Kg">Kg</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="form-label-custom" Text="Rate:"></asp:Label>
                    <asp:TextBox ID="txtRate" runat="server" CssClass="iwps-input-edit" Width="100%"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="mt-3">
            <asp:Button ID="btnSaveDraft" runat="server" Text="SAVE DRAFT" CssClass="btn btn-sm btn-iwps-primary me-2 mr-2" OnClick="btnSaveDraft_Click" />
            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn btn-sm btn-success me-2 mr-2" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnClear" runat="server" Text="CLEAR" CssClass="btn btn-sm btn-secondary" OnClick="btnClear_Click" />
        </div>
    </div>
</div>
</asp:Content>
