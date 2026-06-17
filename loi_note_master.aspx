<%@ Page Title="LOI Note Master" Language="C#" MasterPageFile="~/iwps.Master" AutoEventWireup="true" CodeBehind="loi_note_master.aspx.cs" Inherits="IWPS.loi_note_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-section { background: #fff; padding: 20px; border-radius: 6px; box-shadow: 0 1px 4px rgba(0,0,0,.12); margin-bottom: 20px; }
        .grid-section { background: #fff; padding: 20px; border-radius: 6px; box-shadow: 0 1px 4px rgba(0,0,0,.12); }
        .grid-table { width: 100%; border-collapse: collapse; }
        .grid-table th { background: #2c3e50; color: #fff; padding: 8px 12px; text-align: left; }
        .grid-table td { padding: 8px 12px; border-bottom: 1px solid #e0e0e0; vertical-align: top; }
        .grid-table tr:hover td { background: #f5f5f5; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_maindiv" runat="server">
    <h3 class="page-title">LOI Note Master</h3>

    <div class="form-section">
        <asp:HiddenField ID="hfNoteCode" runat="server" />
        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Note Code</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtNoteCode" runat="server" CssClass="form-control" ReadOnly="true" placeholder="Auto-generated" />
            </div>
        </div>
        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Note Title <span class="text-danger">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtNoteTitle" runat="server" CssClass="form-control" MaxLength="200" />
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtNoteTitle"
                    ErrorMessage="Note Title is required." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>
        <div class="form-group row mb-3">
            <label class="col-sm-2 col-form-label">Note Text <span class="text-danger">*</span></label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtNoteText" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                <asp:RequiredFieldValidator ID="rfvText" runat="server" ControlToValidate="txtNoteText"
                    ErrorMessage="Note Text is required." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>
        <div class="form-group row">
            <div class="col-sm-10 offset-sm-2">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary me-2" OnClick="btnSave_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btnClear_Click" CausesValidation="false" />
            </div>
        </div>
    </div>

    <div class="grid-section">
        <h5>Notes List</h5>
        <asp:GridView ID="gvNotes" runat="server" AutoGenerateColumns="false" CssClass="grid-table"
            OnRowCommand="gvNotes_RowCommand" EmptyDataText="No notes found.">
            <Columns>
                <asp:BoundField DataField="NOTE_CODE" HeaderText="Note Code" />
                <asp:BoundField DataField="NOTE_TITLE" HeaderText="Note Title" />
                <asp:BoundField DataField="NOTE_TEXT" HeaderText="Note Text" />
                <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" />
                <asp:BoundField DataField="CREATED_DATE" HeaderText="Created Date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" runat="server" CommandName="EditNote"
                            CommandArgument='<%# Eval("NOTE_CODE") %>' CssClass="btn btn-sm btn-warning">Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
