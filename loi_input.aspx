<%@ Page Title="Create LOI" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="loi_input.aspx.cs" Inherits="IWPS.loi_input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-section { background: #fff; padding: 20px; border-radius: 6px; box-shadow: 0 1px 4px rgba(0,0,0,.12); margin-bottom: 20px; }
        .info-box { background: #eaf4fb; border-left: 4px solid #3498db; padding: 10px 16px; margin-bottom: 10px; border-radius: 4px; }
        .notes-list { max-height: 250px; overflow-y: auto; border: 1px solid #ddd; padding: 10px; border-radius: 4px; }
        .notes-list label { display: block; margin-bottom: 6px; }
        #divSecurityAmount { display: none; }
    </style>
    <script type="text/javascript">
        function onEnqChange() {
            var ddl = document.getElementById('<%= ddlEnquiry.ClientID %>');
            var val = ddl.value;
            if (!val) {
                document.getElementById('divPartyInfo').innerHTML = '';
                return;
            }
            // Party info loaded via postback
            __doPostBack('<%= ddlEnquiry.UniqueID %>', '');
        }

        function toggleSecurityAmount() {
            var ddl = document.getElementById('<%= ddlSecurityDepositReqd.ClientID %>');
            var div = document.getElementById('divSecurityAmount');
            if (ddl.value === 'Y') {
                div.style.display = '';
            } else {
                div.style.display = 'none';
            }
        }

        window.onload = function () {
            toggleSecurityAmount();
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
    <h3 class="page-title">Create Letter of Intent (LOI)</h3>

    <div class="form-section">
        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Enquiry No <span class="text-danger">*</span></label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlEnquiry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiry_SelectedIndexChanged">
                    <asp:ListItem Value="">-- Select Enquiry --</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvEnq" runat="server" ControlToValidate="ddlEnquiry"
                    ErrorMessage="Please select an enquiry." CssClass="text-danger" Display="Dynamic" InitialValue="" />
            </div>
            <div class="col-sm-4">
                <div id="divPartyInfo">
                    <asp:Panel ID="pnlPartyInfo" runat="server" Visible="false">
                        <div class="info-box">
                            <strong>Awarded Party:</strong> <asp:Label ID="lblPartyName" runat="server" /><br />
                            <strong>Enquiry Value:</strong> <asp:Label ID="lblEnqValue" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>

        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">LOI Date <span class="text-danger">*</span></label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtLoiDate" runat="server" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator ID="rfvLoiDate" runat="server" ControlToValidate="txtLoiDate"
                    ErrorMessage="LOI Date is required." CssClass="text-danger" Display="Dynamic" />
            </div>
            <label class="col-sm-2 col-form-label">Validity Date <span class="text-danger">*</span></label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtValidityDate" runat="server" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator ID="rfvValidity" runat="server" ControlToValidate="txtValidityDate"
                    ErrorMessage="Validity Date is required." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>

        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Work Description <span class="text-danger">*</span></label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtWorkDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                <asp:RequiredFieldValidator ID="rfvWorkDesc" runat="server" ControlToValidate="txtWorkDescription"
                    ErrorMessage="Work Description is required." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>

        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Contract Value <span class="text-danger">*</span></label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtContractValue" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvContractValue" runat="server" ControlToValidate="txtContractValue"
                    ErrorMessage="Contract Value is required." CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revContractValue" runat="server" ControlToValidate="txtContractValue"
                    ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Enter valid amount." CssClass="text-danger" Display="Dynamic" />
            </div>
            <label class="col-sm-2 col-form-label">Payment Terms <span class="text-danger">*</span></label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtPaymentTerms" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPaymentTerms" runat="server" ControlToValidate="txtPaymentTerms"
                    ErrorMessage="Payment Terms is required." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>

        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Security Deposit Reqd</label>
            <div class="col-sm-2">
                <asp:DropDownList ID="ddlSecurityDepositReqd" runat="server" CssClass="form-control" onchange="toggleSecurityAmount()">
                    <asp:ListItem Value="N">No</asp:ListItem>
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-4" id="divSecurityAmount">
                <label class="col-form-label">Security Amount</label>
                <asp:TextBox ID="txtSecurityAmount" runat="server" CssClass="form-control" placeholder="0.00" />
            </div>
        </div>

        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Notes / Clauses</label>
            <div class="col-sm-8">
                <div class="notes-list">
                    <asp:CheckBoxList ID="cblNotes" runat="server" DataTextField="NOTE_TITLE" DataValueField="NOTE_CODE" />
                </div>
            </div>
        </div>

        <div class="form-group row">
            <div class="col-sm-10 offset-sm-2">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit LOI" CssClass="btn btn-primary me-2" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
