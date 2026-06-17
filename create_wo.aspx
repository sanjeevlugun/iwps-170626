<%@ Page Title="Create Work Order" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="create_wo.aspx.cs" Inherits="IWPS.create_wo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-section { background:#fff; padding:20px; border-radius:6px; box-shadow:0 1px 4px rgba(0,0,0,.12); margin-bottom:20px; }
        .info-box { background:#eaf4fb; border-left:4px solid #3498db; padding:10px 16px; border-radius:4px; }
        .items-table { width:100%; border-collapse:collapse; }
        .items-table th { background:#34495e; color:#fff; padding:8px; }
        .items-table td { padding:6px 8px; border:1px solid #ddd; }
        .notes-list { max-height:220px; overflow-y:auto; border:1px solid #ddd; padding:10px; border-radius:4px; }
        #divTaxAmount { display:none; }
    </style>
    <script type="text/javascript">
        function toggleTaxAmount() {
            var ddl = document.getElementById('<%= ddlTaxApplicable.ClientID %>');
            var div = document.getElementById('divTaxAmount');
            div.style.display = (ddl.value === 'Y') ? '' : 'none';
        }
        window.onload = function () { toggleTaxAmount(); };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
    <h3 class="page-title">Create Work Order</h3>
    <asp:HiddenField ID="hfWoNo" runat="server" />

    <div class="form-section">
        <h5>LOI / Enquiry Details</h5>
        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Approved LOI <span class="text-danger">*</span></label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlLoi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLoi_SelectedIndexChanged">
                    <asp:ListItem Value="">-- Select LOI --</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvLoi" runat="server" ControlToValidate="ddlLoi"
                    ErrorMessage="Please select a LOI." CssClass="text-danger" Display="Dynamic" InitialValue="" />
            </div>
            <div class="col-sm-5">
                <asp:Panel ID="pnlLoiInfo" runat="server" Visible="false">
                    <div class="info-box">
                        <strong>Enquiry No:</strong> <asp:Label ID="lblEnqNo" runat="server" /><br />
                        <strong>Party Name:</strong> <asp:Label ID="lblPartyName" runat="server" /><br />
                        <strong>Contract Value:</strong> RM <asp:Label ID="lblContractValue" runat="server" />
                    </div>
                </asp:Panel>
            </div>
        </div>

        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">WO Date <span class="text-danger">*</span></label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtWoDate" runat="server" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator ID="rfvWoDate" runat="server" ControlToValidate="txtWoDate"
                    ErrorMessage="WO Date required." CssClass="text-danger" Display="Dynamic" />
            </div>
            <label class="col-sm-2 col-form-label">WO Start Date <span class="text-danger">*</span></label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtWoStartDate" runat="server" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator ID="rfvWoStart" runat="server" ControlToValidate="txtWoStartDate"
                    ErrorMessage="Start Date required." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>

        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">WO End Date <span class="text-danger">*</span></label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtWoEndDate" runat="server" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator ID="rfvWoEnd" runat="server" ControlToValidate="txtWoEndDate"
                    ErrorMessage="End Date required." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>

        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">WO Description <span class="text-danger">*</span></label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtWoDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                <asp:RequiredFieldValidator ID="rfvWoDesc" runat="server" ControlToValidate="txtWoDescription"
                    ErrorMessage="Description required." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>

        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Contract Value <span class="text-danger">*</span></label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtWoContractValue" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvWoCv" runat="server" ControlToValidate="txtWoContractValue"
                    ErrorMessage="Contract Value required." CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revWoCv" runat="server" ControlToValidate="txtWoContractValue"
                    ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Enter valid amount." CssClass="text-danger" Display="Dynamic" />
            </div>
            <label class="col-sm-2 col-form-label">Tax Applicable</label>
            <div class="col-sm-2">
                <asp:DropDownList ID="ddlTaxApplicable" runat="server" CssClass="form-control" onchange="toggleTaxAmount()">
                    <asp:ListItem Value="N">No</asp:ListItem>
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-3" id="divTaxAmount">
                <label class="col-form-label">Tax Amount</label>
                <asp:TextBox ID="txtTaxAmount" runat="server" CssClass="form-control" placeholder="0.00" />
            </div>
        </div>
    </div>

    <div class="form-section">
        <h5>Work Items</h5>
        <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="false" CssClass="items-table"
            EmptyDataText="No items. Select a LOI first.">
            <Columns>
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                <asp:BoundField DataField="ITEM_DESCRIPTION" HeaderText="Description" />
                <asp:BoundField DataField="UOM" HeaderText="UOM" />
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("QTY") %>' CssClass="form-control" style="width:80px;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Rate">
                    <ItemTemplate>
                        <asp:TextBox ID="txtUnitRate" runat="server" Text='<%# Eval("UNIT_RATE") %>' CssClass="form-control" style="width:100px;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Ref" Visible="false" />
            </Columns>
        </asp:GridView>
    </div>

    <div class="form-section">
        <h5>Notes / Clauses</h5>
        <div class="notes-list">
            <asp:CheckBoxList ID="cblNotes" runat="server" DataTextField="NOTE_TITLE" DataValueField="NOTE_CODE" />
        </div>
    </div>

    <div class="form-section">
        <asp:Button ID="btnSaveDraft" runat="server" Text="Save as Draft" CssClass="btn btn-warning me-2" OnClick="btnSaveDraft_Click" />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary me-2" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
    </div>
</asp:Content>
