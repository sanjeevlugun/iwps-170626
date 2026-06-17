<%@ Page Title="LOI Approval" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="loi_list_approver.aspx.cs" Inherits="IWPS.loi_list_approver" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .grid-section { background: #fff; padding: 20px; border-radius: 6px; box-shadow: 0 1px 4px rgba(0,0,0,.12); }
        .grid-table { width: 100%; border-collapse: collapse; }
        .grid-table th { background: #2c3e50; color: #fff; padding: 8px 12px; text-align: left; }
        .grid-table td { padding: 8px 12px; border-bottom: 1px solid #e0e0e0; vertical-align: middle; }
        .grid-table tr:hover td { background: #f5f5f5; }
        .remarks-input { width: 200px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
    <h3 class="page-title">LOI Approval - Pending LOIs</h3>

    <div class="grid-section">
        <asp:GridView ID="gvLoi" runat="server" AutoGenerateColumns="false" CssClass="grid-table"
            OnRowCommand="gvLoi_RowCommand" EmptyDataText="No pending LOIs for approval."
            DataKeyNames="LOI_NO">
            <Columns>
                <asp:BoundField DataField="LOI_NO" HeaderText="LOI No" />
                <asp:BoundField DataField="ENQ_NO" HeaderText="Enquiry No" />
                <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name" />
                <asp:BoundField DataField="CONTRACT_VALUE" HeaderText="Contract Value" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="LOI_DATE" HeaderText="LOI Date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="CREATED_BY" HeaderText="Submitted By" />
                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control remarks-input" placeholder="Enter remarks..." />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbApprove" runat="server" CommandName="ApproveLoi"
                            CommandArgument='<%# Eval("LOI_NO") %>' CssClass="btn btn-sm btn-success me-1"
                            OnClientClick="return confirm('Approve this LOI?');">Approve</asp:LinkButton>
                        <asp:LinkButton ID="lbReject" runat="server" CommandName="RejectLoi"
                            CommandArgument='<%# Eval("LOI_NO") %>' CssClass="btn btn-sm btn-danger"
                            OnClientClick="return confirm('Reject this LOI?');">Reject</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View">
                    <ItemTemplate>
                        <a href='<%# "loi_print.aspx?id=" + Eval("LOI_NO") %>' target="_blank" class="btn btn-sm btn-info">View</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
