<%@ Page Title="Enquiry Documents" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeFile="enquiry_uploaded_documents.aspx.cs" Inherits="IWPS.enquiry_uploaded_documents" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="card shadow-sm">
  <div class="card-header bg-primary text-white"><h5 class="mb-0">Enquiry Uploaded Documents</h5></div>
  <div class="card-body">
    <div class="row g-2 mb-3">
      <div class="col-md-3"><asp:TextBox ID="txtEnqNo" runat="server" CssClass="form-control" placeholder="Enquiry Number"></asp:TextBox></div>
      <div class="col-md-2"><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
    </div>
    <asp:GridView ID="gvDocs" runat="server" CssClass="table table-bordered table-hover table-sm" AutoGenerateColumns="false">
      <Columns>
        <asp:BoundField DataField="ENQ_NO" HeaderText="Enq No" />
        <asp:BoundField DataField="DOC_NAME" HeaderText="Document Name" />
        <asp:BoundField DataField="DOC_TYPE" HeaderText="Doc Type" />
        <asp:BoundField DataField="UPLOADED_BY" HeaderText="Uploaded By" />
        <asp:BoundField DataField="UPLOADED_DATE" HeaderText="Upload Date" DataFormatString="{0:dd-MMM-yyyy}" />
      </Columns>
    </asp:GridView>
  </div>
</div>
</asp:Content>
