<%@ Page Title="Category Master" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="category_master.aspx.cs" Inherits="IWPS.category_master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
    <div class="iwps-card-header">Category Master</div>
    <div class="card-body">
        <asp:HiddenField ID="hfCatCode" runat="server" Value="" />
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" Text="" Visible="false"></asp:Label>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="form-label-custom" Text="Category Code:"></asp:Label>
                    <asp:TextBox ID="txtCatCode" runat="server" CssClass="iwps-input-readonly" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-8">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="iwps-label-required" Text="Category Title"></asp:Label>
                    <asp:TextBox ID="txtCatTitle" runat="server" CssClass="iwps-input-edit" Width="100%"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="form-label-custom" Text="Category Description:"></asp:Label>
                    <asp:TextBox ID="txtCatDesc" runat="server" CssClass="iwps-input-edit" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="iwps-label-required" Text="Type of Work"></asp:Label>
                    <asp:DropDownList ID="ddlTypeOfWork" runat="server" CssClass="form-control iwps-input-edit" Width="100%">
                        <asp:ListItem Value="">-- Select --</asp:ListItem>
                        <asp:ListItem Value="S">S - Service</asp:ListItem>
                        <asp:ListItem Value="W">W - Work</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="iwps-label-required" Text="Work Service Type"></asp:Label>
                    <asp:DropDownList ID="ddlWorkServiceType" runat="server" CssClass="form-control iwps-input-edit" Width="100%">
                        <asp:ListItem Value="">-- Select --</asp:ListItem>
                        <asp:ListItem Value="J">J - Jobwork</asp:ListItem>
                        <asp:ListItem Value="L">L - Labour</asp:ListItem>
                        <asp:ListItem Value="C">C - Civil</asp:ListItem>
                        <asp:ListItem Value="E">E - Electrical</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="form-label-custom" Text="Department:"></asp:Label>
                    <asp:TextBox ID="txtDept" runat="server" CssClass="iwps-input-readonly" ReadOnly="true" Width="100%"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="mt-3">
            <asp:Button ID="btnSaveDraft" runat="server" Text="SAVE AS DRAFT" CssClass="btn btn-sm btn-iwps-primary me-2 mr-2" OnClick="btnSaveDraft_Click" />
            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn btn-sm btn-success me-2 mr-2" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnClear" runat="server" Text="CLEAR" CssClass="btn btn-sm btn-secondary" OnClick="btnClear_Click" />
        </div>
    </div>
</div>
</asp:Content>
