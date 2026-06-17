<%@ Page Title="WO Print" Language="C#" AutoEventWireup="true" CodeFile="wo_print.aspx.cs" Inherits="IWPS.wo_print" %>
<!DOCTYPE html><html><head><title>Work Order</title>
<style>body{font-family:Arial,sans-serif;font-size:12px;}table{border-collapse:collapse;width:100%;}th,td{border:1px solid #000;padding:4px;}h3{text-align:center;}.nb td{border:none;}</style>
</head><body>
<h3>BHEL BHOPAL - IWPS<br/>WORK ORDER</h3>
<asp:PlaceHolder ID="phContent" runat="server"/>
<script>window.onload=function(){window.print();};</script>
</body></html>
