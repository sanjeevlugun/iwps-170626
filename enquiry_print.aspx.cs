using System;
using System.Data;
using System.Text;
using System.Web.UI;
namespace IWPS {
    public partial class enquiry_print : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            string enqNo = Request.QueryString["id"];
            if (string.IsNullOrEmpty(enqNo)) { Response.Write("No ENQ_NO"); return; }
            try {
                DataTable hdr = DBHelper.ExecuteQuery("SELECT * FROM MATPASS.TBL_IWPS_ENQUIRY_HEADER WHERE ENQ_NO=:e", DBHelper.P("e", enqNo));
                DataTable items = DBHelper.ExecuteQuery("SELECT * FROM MATPASS.TBL_IWPS_ENQUIRY_ITEMS WHERE ENQ_NO=:e", DBHelper.P("e", enqNo));
                DataTable parties = DBHelper.ExecuteQuery("SELECT * FROM MATPASS.TBL_IWPS_ENQUIRY_PARTY WHERE ENQ_NO=:e", DBHelper.P("e", enqNo));
                var sb = new StringBuilder();
                if (hdr.Rows.Count > 0) {
                    DataRow r = hdr.Rows[0];
                    sb.Append("<table class='no-border'>");
                    sb.Append($"<tr><td><b>Enquiry No:</b> {r["ENQ_NO"]}</td><td><b>Date:</b> {Convert.ToDateTime(r["ENQ_DATE"]):dd-MMM-yyyy}</td></tr>");
                    sb.Append($"<tr><td><b>Type:</b> {r["ENQ_TYPE"]}</td><td><b>Status:</b> {r["STATUS"]}</td></tr>");
                    sb.Append($"<tr><td colspan='2'><b>Description:</b> {r["DESCRIPTION"]}</td></tr>");
                    sb.Append($"<tr><td><b>Est. Value:</b> {r["EST_VALUE"]}</td><td><b>Dept:</b> {r["DEPT"]}</td></tr>");
                    sb.Append("</table><br/>");
                }
                sb.Append("<b>Items:</b><table><tr><th>#</th><th>Item Code</th><th>Description</th><th>UOM</th><th>Qty</th></tr>");
                int i = 1;
                foreach (DataRow r in items.Rows)
                    sb.Append($"<tr><td>{i++}</td><td>{r["ITEM_CODE"]}</td><td>{r["ITEM_DESCRIPTION"]}</td><td>{r["UOM"]}</td><td>{r["QTY"]}</td></tr>");
                sb.Append("</table><br/><b>Parties:</b><table><tr><th>#</th><th>Party Name</th><th>Email</th><th>Mobile</th><th>EMD Reqd</th></tr>");
                i = 1;
                foreach (DataRow r in parties.Rows)
                    sb.Append($"<tr><td>{i++}</td><td>{r["PARTY_NAME"]}</td><td>{r["PARTY_EMAIL"]}</td><td>{r["PARTY_MOBILE"]}</td><td>{r["EMD_REQD"]}</td></tr>");
                sb.Append("</table>");
                phContent.Controls.Add(new LiteralControl(sb.ToString()));
            } catch (Exception ex) {
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}
