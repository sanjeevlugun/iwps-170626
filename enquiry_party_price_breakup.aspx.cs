using System;

namespace IWPS
{
    public partial class enquiry_party_price_breakup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string enqno = Request.QueryString["enqno"];
                if (!string.IsNullOrEmpty(enqno)) { txtEnqNo.Text = enqno; LoadData(enqno); }
            }
        }

        void LoadData(string enqno)
        {
            try
            {
                gvPrice.DataSource = DBHelper.ExecuteQuery(
                    "SELECT ENQ_NO,PARTY_NAME,QUOTED_AMOUNT,RANK,AWARD_STATUS,QUALIFIED_STATUS FROM MATPASS.TBL_IWPS_ENQUIRY_PARTY WHERE ENQ_NO LIKE :e ORDER BY RANK NULLS LAST,PARTY_NAME",
                    DBHelper.P("e", "%" + enqno + "%"));
                gvPrice.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { LoadData(txtEnqNo.Text.Trim()); }
    }
}
