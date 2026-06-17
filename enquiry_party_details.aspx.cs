using System;

namespace IWPS
{
    public partial class enquiry_party_details : System.Web.UI.Page
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
                gvParty.DataSource = DBHelper.ExecuteQuery(
                    "SELECT ENQ_NO,PARTY_NAME,PARTY_EMAIL,PARTY_MOBILE,EMD_REQD,EMD_STATUS,QUALIFIED_STATUS,CLC_ID FROM MATPASS.TBL_IWPS_ENQUIRY_PARTY WHERE ENQ_NO LIKE :e ORDER BY PARTY_NAME",
                    DBHelper.P("e", "%" + enqno + "%"));
                gvParty.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { LoadData(txtEnqNo.Text.Trim()); }
    }
}
