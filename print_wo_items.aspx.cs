using System;
using System.Data;
using System.Text;
using System.Web.UI;
namespace IWPS {
    public partial class print_wo_items : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            string woNo = Request.QueryString["wono"];
            if (string.IsNullOrEmpty(woNo)) { Response.Write("No WO_NO provided."); return; }
            try {
                var sb = new StringBuilder();
                sb.Append($"<p><b>Work Order No:</b> {woNo}</p>");
                sb.Append("<table><tr><th>#</th><th>Item Code</th><th>Description</th><th>UOM</th><th>Qty</th><th>Unit Rate</th><th>Amount</th></tr>");
                DataTable dt = DBHelper.ExecuteQuery("SELECT ITEM_CODE, ITEM_DESCRIPTION, UOM, QTY, UNIT_RATE FROM MATPASS.TBL_IWPS_WO_ITEMS WHERE WO_NO=:w ORDER BY ITEM_CODE", DBHelper.P("w", woNo));
                int i = 1; decimal total = 0;
                foreach (DataRow r in dt.Rows) {
                    decimal qty = r["QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(r["QTY"]);
                    decimal rate = r["UNIT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(r["UNIT_RATE"]);
                    decimal amt = qty * rate; total += amt;
                    sb.Append($"<tr><td>{i++}</td><td>{r["ITEM_CODE"]}</td><td>{r["ITEM_DESCRIPTION"]}</td><td>{r["UOM"]}</td><td>{qty}</td><td>{rate:N2}</td><td>{amt:N2}</td></tr>");
                }
                sb.Append($"<tr><td colspan='6' style='text-align:right'><b>Total</b></td><td><b>{total:N2}</b></td></tr>");
                sb.Append("</table>");
                phContent.Controls.Add(new LiteralControl(sb.ToString()));
            } catch (Exception ex) { Response.Write("Error: " + ex.Message); }
        }
    }
}
