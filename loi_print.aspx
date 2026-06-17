<%@ Page Title="LOI Print" Language="C#" AutoEventWireup="true" CodeBehind="loi_print.aspx.cs" Inherits="IWPS.loi_print" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <title>Letter of Intent</title>
    <style>
        body { font-family: Arial, sans-serif; font-size: 12pt; margin: 40px; color: #000; }
        .header { text-align: center; margin-bottom: 30px; }
        .header h2 { margin: 0; font-size: 16pt; text-transform: uppercase; }
        .header h3 { margin: 4px 0; font-size: 13pt; }
        .loi-title { text-align: center; font-size: 14pt; font-weight: bold; text-decoration: underline; margin: 20px 0 30px; }
        .info-table { width: 100%; border-collapse: collapse; margin-bottom: 20px; }
        .info-table td { padding: 6px 10px; vertical-align: top; }
        .info-table td:first-child { width: 30%; font-weight: bold; }
        .notes-section { margin-top: 20px; }
        .notes-section h4 { font-size: 12pt; font-weight: bold; border-bottom: 1px solid #000; padding-bottom: 4px; margin-bottom: 10px; }
        .note-item { margin-bottom: 14px; }
        .note-item .note-title { font-weight: bold; }
        .note-item .note-text { margin-top: 4px; }
        .signature-section { margin-top: 60px; }
        .signature-table { width: 100%; }
        .signature-table td { vertical-align: bottom; }
        .sig-line { border-top: 1px solid #000; width: 200px; margin-top: 40px; }
        @media print {
            body { margin: 20px; }
            .no-print { display: none; }
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            window.print();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="no-print" style="margin-bottom:16px;">
            <button onclick="window.print(); return false;" style="padding:6px 18px; font-size:12pt; cursor:pointer;">Print</button>
            <button onclick="window.close(); return false;" style="padding:6px 18px; font-size:12pt; cursor:pointer; margin-left:8px;">Close</button>
        </div>

        <div class="header">
            <asp:Label ID="lblOrgName" runat="server" style="font-size:16pt; font-weight:bold; display:block;" />
            <asp:Label ID="lblDeptName" runat="server" style="font-size:13pt; display:block;" />
        </div>

        <div class="loi-title">LETTER OF INTENT</div>

        <table class="info-table">
            <tr>
                <td>LOI No:</td>
                <td><asp:Label ID="lblLoiNo" runat="server" /></td>
                <td style="width:30%; font-weight:bold;">LOI Date:</td>
                <td><asp:Label ID="lblLoiDate" runat="server" /></td>
            </tr>
            <tr>
                <td>Enquiry No:</td>
                <td><asp:Label ID="lblEnqNo" runat="server" /></td>
                <td style="font-weight:bold;">Validity Date:</td>
                <td><asp:Label ID="lblValidityDate" runat="server" /></td>
            </tr>
            <tr>
                <td>Party Name:</td>
                <td colspan="3"><asp:Label ID="lblPartyName" runat="server" /></td>
            </tr>
        </table>

        <p>Dear Sir/Madam,</p>
        <p>With reference to the above-mentioned enquiry, we are pleased to issue this Letter of Intent for the following scope of work:</p>

        <table class="info-table" style="border:1px solid #ccc;">
            <tr style="background:#f0f0f0;">
                <td><strong>Work Description</strong></td>
                <td><asp:Label ID="lblWorkDescription" runat="server" /></td>
            </tr>
            <tr>
                <td><strong>Contract Value (RM)</strong></td>
                <td><asp:Label ID="lblContractValue" runat="server" /></td>
            </tr>
            <tr>
                <td><strong>Payment Terms</strong></td>
                <td><asp:Label ID="lblPaymentTerms" runat="server" /></td>
            </tr>
            <tr>
                <td><strong>Security Deposit Required</strong></td>
                <td><asp:Label ID="lblSecurityDepositReqd" runat="server" /></td>
            </tr>
            <asp:Panel ID="pnlSecurityAmount" runat="server">
                <tr>
                    <td><strong>Security Deposit Amount (RM)</strong></td>
                    <td><asp:Label ID="lblSecurityAmount" runat="server" /></td>
                </tr>
            </asp:Panel>
        </table>

        <div class="notes-section">
            <h4>Terms and Conditions</h4>
            <asp:Repeater ID="rptNotes" runat="server">
                <ItemTemplate>
                    <div class="note-item">
                        <div class="note-title"><%# DataBinder.Eval(Container.DataItem, "NOTE_TITLE") %></div>
                        <div class="note-text"><%# DataBinder.Eval(Container.DataItem, "NOTE_TEXT") %></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div class="signature-section">
            <table class="signature-table">
                <tr>
                    <td>
                        <div class="sig-line"></div>
                        <div>Prepared By</div>
                        <div><asp:Label ID="lblCreatedBy" runat="server" /></div>
                    </td>
                    <td>
                        <div class="sig-line"></div>
                        <div>Approved By</div>
                    </td>
                    <td>
                        <div class="sig-line"></div>
                        <div>Acknowledged By (Contractor)</div>
                    </td>
                </tr>
            </table>
        </div>

        <div style="margin-top:30px; font-size:9pt; color:#666; text-align:center;">
            Printed on: <asp:Label ID="lblPrintDate" runat="server" />
        </div>
    </form>
</body>
</html>
