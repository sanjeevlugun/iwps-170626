<%@ Page Title="Final WO Approval" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="App_wo.aspx.cs" Inherits="IWPS.App_wo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .detail-section { background:#fff; padding:20px; border-radius:6px; box-shadow:0 1px 4px rgba(0,0,0,.12); margin-bottom:20px; }
        .detail-table { width:100%; border-collapse:collapse; }
        .detail-table td { padding:8px 12px; border-bottom:1px solid #eee; }
        .detail-table td:first-child { width:25%; font-weight:bold; background:#f8f9fa; }
        .items-table { width:100%; border-collapse:collapse; }
        .items-table th { background:#34495e; color:#fff; padding:8px 10px; }
        .items-table td { padding:7px 10px; border:1px solid #ddd; }
        .log-table { width:100%; border-collapse:collapse; }
        .log-table th { background:#7f8c8d; color:#fff; padding:7px 10px; text-align:left; }
        .log-table td { padding:7px 10px; border-bottom:1px solid #eee; }
        .badge-approved { background:#5cb85c; color:#fff; padding:2px 8px; border-radius:10px; font-size:.85em; }
        .badge-pending { background:#f0ad4e; color:#fff; padding:2px 8px; border-radius:10px; font-size:.85em; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
    <h3 class="page-title">Final Work Order Approval</h3>
    <asp:HiddenField ID="hfWoNo" runat="server" />

    <div class="detail-section">
        <h5>Work Order Details</h5>
        <table class="detail-table">
            <tr><td>WO Number</td><td><asp:Label ID="lblWoNo" runat="server" style="font-weight:bold;" /></td>
                <td style="font-weight:bold;">Status</td><td><asp:Label ID="lblStatus" runat="server" /></td></tr>
            <tr><td>LOI No</td><td><asp:Label ID="lblLoiNo" runat="server" /></td>
                <td style="font-weight:bold;">Enquiry No</td><td><asp:Label ID="lblEnqNo" runat="server" /></td></tr>
            <tr><td>Party Name</td><td colspan="3"><asp:Label ID="lblPartyName" runat="server" /></td></tr>
            <tr><td>WO Date</td><td><asp:Label ID="lblWoDate" runat="server" /></td>
                <td style="font-weight:bold;">Start Date</td><td><asp:Label ID="lblWoStartDate" runat="server" /></td></tr>
            <tr><td>End Date</td><td><asp:Label ID="lblWoEndDate" runat="server" /></td>
                <td style="font-weight:bold;">Contract Value</td><td><asp:Label ID="lblContractValue" runat="server" /></td></tr>
            <tr><td>Tax Applicable</td><td><asp:Label ID="lblTaxApplicable" runat="server" /></td>
                <td style="font-weight:bold;">Tax Amount</td><td><asp:Label ID="lblTaxAmount" runat="server" /></td></tr>
            <tr><td>WO Description</td><td colspan="3"><asp:Label ID="lblWoDescription" runat="server" /></td></tr>
            <tr><td>Department</td><td><asp:Label ID="lblDept" runat="server" /></td>
                <td style="font-weight:bold;">Created By</td><td><asp:Label ID="lblCreatedBy" runat="server" /></td></tr>
        </table>
    </div>

    <div class="detail-section">
        <h5>Work Items</h5>
        <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="false" CssClass="items-table"
            EmptyDataText="No items found.">
            <Columns>
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                <asp:BoundField DataField="ITEM_DESCRIPTION" HeaderText="Description" />
                <asp:BoundField DataField="UOM" HeaderText="UOM" />
                <asp:BoundField DataField="QTY" HeaderText="Qty" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="UNIT_RATE" HeaderText="Unit Rate" DataFormatString="{0:N2}" />
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <%# (Convert.ToDecimal(Eval("QTY")) * Convert.ToDecimal(Eval("UNIT_RATE"))).ToString("N2") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div class="detail-section">
        <h5>Process Log</h5>
        <asp:GridView ID="gvLogs" runat="server" AutoGenerateColumns="false" CssClass="log-table"
            EmptyDataText="No process log entries.">
            <Columns>
                <asp:BoundField DataField="ACTION" HeaderText="Action" />
                <asp:BoundField DataField="ACTION_BY" HeaderText="By" />
                <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />
                <asp:BoundField DataField="ACTION_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
            </Columns>
        </asp:GridView>
    </div>

    <div class="detail-section">
        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Remarks</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter final approval remarks..." />
            </div>
        </div>
        <asp:Panel ID="pnlActions" runat="server">
            <asp:Button ID="btnFinalApprove" runat="server" Text="Final Approve" CssClass="btn btn-success me-2"
                OnClick="btnFinalApprove_Click"
                OnClientClick="return confirm('Final approve this Work Order?');" />
            <a href="javascript:history.back();" class="btn btn-secondary">Back</a>
        </asp:Panel>
        <asp:Panel ID="pnlAlreadyApproved" runat="server" Visible="false">
            <div class="alert alert-success">This Work Order has already been finally approved.</div>
            <a href="javascript:history.back();" class="btn btn-secondary">Back</a>
        </asp:Panel>
    </div>
</asp:Content>
