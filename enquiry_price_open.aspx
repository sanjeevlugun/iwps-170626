<%@ Page Title="Enquiry Price Open" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="enquiry_price_open.aspx.cs" Inherits="IWPS.enquiry_price_open" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cph_maindiv" runat="server">
<div class="iwps-card">
  <div class="iwps-card-header">ENQUIRY - PRICE OPEN / PART-II</div>
  <div class="p-3">
    <asp:Label ID="lblMsg" runat="server" style="color:green;font-weight:bold;font-size:13px;display:block;margin-bottom:8px;"></asp:Label>
    <div class="row mb-2">
      <div class="col-md-4">
        <label class="form-label-custom">Select Enquiry</label>
        <asp:DropDownList ID="ddlEnq" runat="server" CssClass="iwps-input-edit" style="width:100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlEnq_Changed">
          <asp:ListItem Value="">-- Select Enquiry --</asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-md-2 d-flex align-items-end">
        <asp:Button ID="btnOpenPrice" runat="server" Text="OPEN PRICE" CssClass="btn-iwps-primary" OnClick="btnOpenPrice_Click" />
      </div>
    </div>
    <div id="divPrice" runat="server" visible="false">
      <div class="section-title">PARTY QUOTATIONS</div>
      <asp:GridView ID="gvParties" runat="server" CssClass="table table-bordered iwps-custom-grid mb-0"
        AutoGenerateColumns="false" Width="100%" DataKeyNames="PARTY_CODE">
        <Columns>
          <asp:BoundField DataField="PARTY_CODE" HeaderText="Party Code" />
          <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name" />
          <asp:BoundField DataField="QUALIFIED_STATUS" HeaderText="Qualified" />
          <asp:TemplateField HeaderText="Quoted Amount">
            <ItemTemplate><asp:TextBox ID="txtAmt" runat="server" CssClass="grid-input-slim" Text='<%# Eval("QUOTED_AMOUNT") %>' Width="120px"></asp:TextBox></ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Counter Offer">
            <ItemTemplate>
              <asp:DropDownList ID="ddlCO" runat="server" CssClass="grid-input-slim">
                <asp:ListItem Value="NO">NO</asp:ListItem>
                <asp:ListItem Value="YES">YES</asp:ListItem>
                <asp:ListItem Value="NA">NA</asp:ListItem>
              </asp:DropDownList>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:BoundField DataField="RANK" HeaderText="L1 Rank" />
          <asp:TemplateField HeaderText="Award">
            <ItemTemplate>
              <asp:Button ID="btnAward" runat="server" Text="AWARD" CssClass="btn-slim-success"
                CommandName="AWARD" CommandArgument='<%# Eval("PARTY_CODE") %>' />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate><div style="padding:10px;text-align:center;color:#999;">No parties</div></EmptyDataTemplate>
      </asp:GridView>
      <div style="margin-top:10px;">
        <asp:Button ID="btnSaveQuotes" runat="server" Text="SAVE QUOTES &amp; RANK" CssClass="btn-iwps-primary" OnClick="btnSaveQuotes_Click" />
      </div>
    </div>
  </div>
</div>
</asp:Content>
