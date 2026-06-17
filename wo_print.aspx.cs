using System;
using System.Data;
using System.Text;
using System.Web.UI;
namespace IWPS {
    public partial class wo_print : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            string woNo = Request.QueryString["id"];
            if (string.IsNullOrEmpty(woNo)) { Response.Write("No WO_NO"); return; }
            try {
                DataTable hdr = DBHelper.ExecuteQuery("SELECT * FROM MATPASS.TBL_IWPS_WO_HEADER WHERE WO_NO=:w", DBHelper.P("w", woNo));
                DataTable items = DBHelper.ExecuteQuery("SELECT * FROM MATPASS.TBL_IWPS_WO_ITEMS WHERE WO_NO=:w", DBHelper.P("w", woNo));
                DataTable notes = DBHelper.ExecuteQuery("SELECT n.NOTE_TITLE, n.NOTE_TEXT FROM MATPASS.TBL_IWPS_WO_NOTES wn JOIN MATPASS.TBL_IWPS_LOI_NOTE_MASTER n ON n.NOTE_CODE=wn.NOTE_CODE WHERE wn.WO_NO=:w", DBHelper.P("w", woNo));
                var sb = new StringBuilder();
                if (hdr.Rows.Count > 0) {
                    DataRow r = hdr.Rows[0];
                    sb.Append("<table class='nb'>");
                    sb.Append($"<tr><td><b>WO No:</b> {r["WO_NO"]}</td><td><b>Date:</b> {(r["WO_DATE"]==DBNull.Value?"-":Convert.ToDateTime(r["WO_DATE"]).ToString("dd-MMM-yyyy"))}</td></tr>");
                    sb.Append($"<tr><td><b>Party:</b> {r["PARTY_NAME"]}</td><td><b>Contract Value:</b> {r["CONTRACT_VALUE"]}</td></tr>");
                    sb.Append($"<tr><td><b>LOI No:</b> {r["LOI_NO"]}</td><td><b>ENQ No:</b> {r["ENQ_NO"]}</td></tr>");
                    sb.Append($"<tr><td><b>Start:</b> {(r["WO_START_DATE"]==DBNull.Value?"-":Convert.ToDateTime(r["WO_START_DATE"]).ToString("dd-MMM-yyyy"))}</td><td><b>End:</b> {(r["WO_END_DATE"]==DBNull.Value?"-":Convert.ToDateTime(r["WO_END_DATE"]).ToString("dd-MMM-yyyy"))}</td></tr>");
                    sb.Append($"<tr><td colspan='2'><b>Description:</b> {r["WO_DESCRIPTION"]}</td></tr>");
                    sb.Append("</table><br/>");
                }
                sb.Append("<b>Items:</b><table><tr><th>#</th><th>Item Code</th><th>Description</th><th>UOM</th><th>Qty</th><th>Unit Rate</th><th>Amount</th></tr>");
                int i = 1; decimal total = 0;
                foreach (DataRow r in items.Rows) {
                    decimal qty = r["QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(r["QTY"]);
                    decimal rate = r["UNIT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(r["UNIT_RATE"]);
                    decimal amt = qty * rate; total += amt;
                    sb.Append($"<tr><td>{i++}</td><td>{r["ITEM_CODE"]}</td><td>{r["ITEM_DESCRIPTION"]}</td><td>{r["UOM"]}</td><td>{qty}</td><td>{rate:N2}</td><td>{amt:N2}</td></tr>");
                }
                sb.Append($"<tr><td colspan='6' style='text-align:right'><b>Total</b></td><td><b>{total:N2}</b></td></tr>");
                sb.Append("</table><br/>");
                if (notes.Rows.Count > 0) {
                    sb.Append("<b>Terms and Conditions:</b><ol>");
                    foreach (DataRow r in notes.Rows)
                        sb.Append($"<li><b>{r["NOTE_TITLE"]}</b><br/>{r["NOTE_TEXT"]}</li>");
                    sb.Append("</ol>");
                }
                phContent.Controls.Add(new LiteralControl(sb.ToString()));
            } catch (Exception ex) { Response.Write("Error: " + ex.Message); }
        }
    }
}
