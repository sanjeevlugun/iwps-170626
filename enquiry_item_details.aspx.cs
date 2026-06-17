using System;

namespace IWPS
{
    public partial class enquiry_item_details : System.Web.UI.Page
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
                gvItems.DataSource = DBHelper.ExecuteQuery(
                    "SELECT ENQ_NO,ITEM_CODE,DESCRIPTION,UOM,QTY FROM MATPASS.TBL_IWPS_ENQUIRY_ITEMS WHERE ENQ_NO LIKE :e ORDER BY ITEM_CODE",
                    DBHelper.P("e", "%" + enqno + "%"));
                gvItems.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { LoadData(txtEnqNo.Text.Trim()); }
    }
}
