using System;
namespace IWPS {
    public partial class draft_enquiry_list : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) Load(); }
        void Load() {
            string s = ddlStatus.SelectedValue;
            string sql = "SELECT ENQUIRY_NO,ENQUIRY_DATE,TITLE,CATEGORY_CODE,ENQUIRY_DEPT,TOTAL_ESTIMATE,STATUS FROM MATPASS.TBL_IWPS_ENQUIRY_HEADER";
            try {
                gvList.DataSource = string.IsNullOrEmpty(s) ? DBHelper.ExecuteQuery(sql+" ORDER BY ENQUIRY_DATE DESC") : DBHelper.ExecuteQuery(sql+" WHERE STATUS=:s ORDER BY ENQUIRY_DATE DESC", DBHelper.P(":s",s));
                gvList.DataBind();
            } catch { }
        }
        protected void ddlStatus_Changed(object sender, EventArgs e) { Load(); }
    }
}
