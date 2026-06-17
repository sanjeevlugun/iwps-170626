using System;

namespace IWPS
{
    public partial class enquiry_uploaded_documents : System.Web.UI.Page
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
                gvDocs.DataSource = DBHelper.ExecuteQuery(
                    "SELECT ENQ_NO,DOC_NAME,DOC_TYPE,UPLOADED_BY,UPLOADED_DATE FROM MATPASS.TBL_IWPS_ENQUIRY_DOCUMENT WHERE ENQ_NO LIKE :e ORDER BY UPLOADED_DATE DESC",
                    DBHelper.P("e", "%" + enqno + "%"));
                gvDocs.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { LoadData(txtEnqNo.Text.Trim()); }
    }
}
