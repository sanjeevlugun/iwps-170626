<%@ Page Title="WO Items Print" Language="C#" AutoEventWireup="true" CodeFile="print_wo_items.aspx.cs" Inherits="IWPS.print_wo_items" %>
<!DOCTYPE html><html><head><title>WO Items</title>
<style>body{font-family:Arial,sans-serif;font-size:12px;}table{border-collapse:collapse;width:100%;}th,td{border:1px solid #000;padding:4px;}h3{text-align:center;}</style>
</head><body>
<h3>BHEL BHOPAL - IWPS<br/>Work Order Items</h3>
<asp:PlaceHolder ID="phContent" runat="server"/>
<script>window.onload=function(){window.print();};</script>
</body></html>
