<%@ Page Title="Enquiry Print" Language="C#" AutoEventWireup="true" CodeFile="enquiry_print.aspx.cs" Inherits="IWPS.enquiry_print" %>
<!DOCTYPE html>
<html>
<head><title>Enquiry</title>
<style>body{font-family:Arial,sans-serif;font-size:12px;} table{border-collapse:collapse;width:100%;} th,td{border:1px solid #000;padding:4px;} h3{text-align:center;} .no-border td{border:none;}</style>
</head>
<body>
<h3>BHEL BHOPAL - IWPS<br/>ENQUIRY DOCUMENT</h3>
<asp:PlaceHolder ID="phContent" runat="server"></asp:PlaceHolder>
<script>window.onload=function(){window.print();};</script>
</body></html>
