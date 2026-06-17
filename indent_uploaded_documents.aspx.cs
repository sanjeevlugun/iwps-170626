using System;

namespace IWPS
{
    public partial class indent_uploaded_documents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string indno = Request.QueryString["indno"];
                if (!string.IsNullOrEmpty(indno)) { txtIndNo.Text = indno; LoadData(indno); }
            }
        }

        void LoadData(string indno)
        {
            try
            {
                gvDocs.DataSource = DBHelper.ExecuteQuery(
                    "SELECT INDENT_NO,DOC_NAME,DOC_TYPE,UPLOADED_BY,UPLOADED_DATE FROM MATPASS.TBL_IWPS_INDENT_DOCUMENT WHERE INDENT_NO LIKE :i ORDER BY UPLOADED_DATE DESC",
                    DBHelper.P("i", "%" + indno + "%"));
                gvDocs.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { LoadData(txtIndNo.Text.Trim()); }
    }
}
