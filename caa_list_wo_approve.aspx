<%@ Page Title="CAA Work Order Approval" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="caa_list_wo_approve.aspx.cs" Inherits="IWPS.caa_list_wo_approve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .grid-section { background:#fff; padding:20px; border-radius:6px; box-shadow:0 1px 4px rgba(0,0,0,.12); }
        .grid-table { width:100%; border-collapse:collapse; }
        .grid-table th { background:#2c3e50; color:#fff; padding:8px 12px; text-align:left; }
        .grid-table td { padding:8px 12px; border-bottom:1px solid #e0e0e0; vertical-align:middle; }
        .grid-table tr:hover td { background:#f5f5f5; }
        .remarks-input { width:180px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
    <h3 class="page-title">CAA - Work Order Approval (Initiated)</h3>

    <div class="grid-section">
        <asp:GridView ID="gvWo" runat="server" AutoGenerateColumns="false" CssClass="grid-table"
            OnRowCommand="gvWo_RowCommand" EmptyDataText="No initiated work orders pending."
            DataKeyNames="WO_NO" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvWo_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="WO_NO" HeaderText="WO No" />
                <asp:BoundField DataField="ENQ_NO" HeaderText="Enquiry No" />
                <asp:BoundField DataField="LOI_NO" HeaderText="LOI No" />
                <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name" />
                <asp:BoundField DataField="CONTRACT_VALUE" HeaderText="Contract Value" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="WO_DATE" HeaderText="WO Date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" />
                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control remarks-input" placeholder="Enter remarks..." />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbForward" runat="server" CommandName="ForwardWo"
                            CommandArgument='<%# Eval("WO_NO") %>' CssClass="btn btn-sm btn-primary me-1"
                            OnClientClick="return confirm('Forward this WO to HOD?');">Forward</asp:LinkButton>
                        <asp:LinkButton ID="lbReject" runat="server" CommandName="RejectWo"
                            CommandArgument='<%# Eval("WO_NO") %>' CssClass="btn btn-sm btn-danger"
                            OnClientClick="return confirm('Reject this Work Order?');">Reject</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
