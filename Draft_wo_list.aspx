<%@ Page Title="Work Order List" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="Draft_wo_list.aspx.cs" Inherits="IWPS.Draft_wo_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .filter-section { background:#fff; padding:16px 20px; border-radius:6px; box-shadow:0 1px 4px rgba(0,0,0,.12); margin-bottom:20px; }
        .grid-section { background:#fff; padding:20px; border-radius:6px; box-shadow:0 1px 4px rgba(0,0,0,.12); }
        .grid-table { width:100%; border-collapse:collapse; }
        .grid-table th { background:#2c3e50; color:#fff; padding:8px 12px; text-align:left; }
        .grid-table td { padding:8px 12px; border-bottom:1px solid #e0e0e0; }
        .grid-table tr:hover td { background:#f5f5f5; }
        .badge { padding:2px 8px; border-radius:10px; font-size:.8em; color:#fff; }
        .badge-draft { background:#f0ad4e; }
        .badge-initiated { background:#5bc0de; }
        .badge-rejected { background:#d9534f; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
    <h3 class="page-title">Work Order List</h3>

    <div class="filter-section">
        <div class="row align-items-end">
            <div class="col-sm-2 mb-2">
                <label>Status</label>
                <asp:DropDownList ID="ddlFilterStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">All</asp:ListItem>
                    <asp:ListItem Value="DRAFT">Draft</asp:ListItem>
                    <asp:ListItem Value="INITIATED">Initiated</asp:ListItem>
                    <asp:ListItem Value="REJECTED">Rejected</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-3 mb-2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" CausesValidation="false" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary ms-1" OnClick="btnClear_Click" CausesValidation="false" />
            </div>
            <div class="col-sm-7 mb-2 text-end">
                <a href="create_wo.aspx" class="btn btn-success">+ Create Work Order</a>
            </div>
        </div>
    </div>

    <div class="grid-section">
        <asp:GridView ID="gvWo" runat="server" AutoGenerateColumns="false" CssClass="grid-table"
            EmptyDataText="No work orders found." AllowPaging="true" PageSize="20"
            OnPageIndexChanging="gvWo_PageIndexChanging" OnRowCommand="gvWo_RowCommand"
            DataKeyNames="WO_NO">
            <Columns>
                <asp:BoundField DataField="WO_NO" HeaderText="WO No" />
                <asp:BoundField DataField="ENQ_NO" HeaderText="Enquiry No" />
                <asp:BoundField DataField="LOI_NO" HeaderText="LOI No" />
                <asp:BoundField DataField="CONTRACT_VALUE" HeaderText="Contract Value" DataFormatString="{0:N2}" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span class='<%# "badge badge-" + Eval("STATUS").ToString().ToLower() %>'><%# Eval("STATUS") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="WO_DATE" HeaderText="WO Date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkEdit" runat="server"
                            NavigateUrl='<%# "create_wo.aspx?wono=" + Eval("WO_NO") %>'
                            CssClass="btn btn-sm btn-warning">Edit</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CommandName="DeleteWo"
                            CommandArgument='<%# Eval("WO_NO") %>'
                            CssClass='<%# Eval("STATUS").ToString() == "DRAFT" ? "btn btn-sm btn-danger" : "btn btn-sm btn-danger disabled" %>'
                            Visible='<%# Eval("STATUS").ToString() == "DRAFT" %>'
                            OnClientClick="return confirm('Delete this Work Order?');">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
