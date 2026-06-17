<%@ Page Title="LOI List" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="print_loi_list.aspx.cs" Inherits="IWPS.print_loi_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .filter-section { background: #fff; padding: 16px 20px; border-radius: 6px; box-shadow: 0 1px 4px rgba(0,0,0,.12); margin-bottom: 20px; }
        .grid-section { background: #fff; padding: 20px; border-radius: 6px; box-shadow: 0 1px 4px rgba(0,0,0,.12); }
        .grid-table { width: 100%; border-collapse: collapse; }
        .grid-table th { background: #2c3e50; color: #fff; padding: 8px 12px; text-align: left; }
        .grid-table td { padding: 8px 12px; border-bottom: 1px solid #e0e0e0; }
        .grid-table tr:hover td { background: #f5f5f5; }
        .badge-draft { background:#f0ad4e; color:#fff; padding:2px 8px; border-radius:10px; font-size:.8em; }
        .badge-approved { background:#5cb85c; color:#fff; padding:2px 8px; border-radius:10px; font-size:.8em; }
        .badge-rejected { background:#d9534f; color:#fff; padding:2px 8px; border-radius:10px; font-size:.8em; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
    <h3 class="page-title">LOI List</h3>

    <div class="filter-section">
        <div class="row align-items-end">
            <div class="col-sm-3 mb-2">
                <label>LOI Number</label>
                <asp:TextBox ID="txtFilterLoiNo" runat="server" CssClass="form-control" placeholder="Search LOI No..." />
            </div>
            <div class="col-sm-2 mb-2">
                <label>Status</label>
                <asp:DropDownList ID="ddlFilterStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">All</asp:ListItem>
                    <asp:ListItem Value="DRAFT">Draft</asp:ListItem>
                    <asp:ListItem Value="APPROVED">Approved</asp:ListItem>
                    <asp:ListItem Value="REJECTED">Rejected</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-2 mb-2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" CausesValidation="false" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary ms-1" OnClick="btnClear_Click" CausesValidation="false" />
            </div>
            <div class="col-sm-5 mb-2 text-end">
                <a href="loi_input.aspx" class="btn btn-success">+ Create LOI</a>
            </div>
        </div>
    </div>

    <div class="grid-section">
        <asp:GridView ID="gvLoi" runat="server" AutoGenerateColumns="false" CssClass="grid-table"
            EmptyDataText="No LOIs found." AllowPaging="true" PageSize="20" OnPageIndexChanging="gvLoi_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="LOI_NO" HeaderText="LOI No" />
                <asp:BoundField DataField="ENQ_NO" HeaderText="Enquiry No" />
                <asp:BoundField DataField="CONTRACT_VALUE" HeaderText="Contract Value" DataFormatString="{0:N2}" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span class='<%# GetStatusBadge(Eval("STATUS").ToString()) %>'><%# Eval("STATUS") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LOI_DATE" HeaderText="LOI Date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" />
                <asp:TemplateField HeaderText="Print">
                    <ItemTemplate>
                        <a href='<%# "loi_print.aspx?id=" + Eval("LOI_NO") %>' target="_blank" class="btn btn-sm btn-info">Print</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
