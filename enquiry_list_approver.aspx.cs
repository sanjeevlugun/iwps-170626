using System; using System.Web.UI.WebControls;
namespace IWPS {
    public partial class enquiry_list_approver : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) Load(); }
        void Load() { try { gvList.DataSource = DBHelper.ExecuteQuery("SELECT ENQUIRY_NO,ENQUIRY_DATE,TITLE,CATEGORY_CODE,BID_EVALUATION_METHOD,TOTAL_ESTIMATE,STATUS FROM MATPASS.TBL_IWPS_ENQUIRY_HEADER WHERE STATUS='INITIATED' ORDER BY ENQUIRY_DATE DESC"); gvList.DataBind(); } catch { } }
        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e) {
            string no = e.CommandArgument.ToString(); int idx = Convert.ToInt32(e.CommandIndex);
            string rem = ""; if (idx >= 0 && idx < gvList.Rows.Count) { var tb = gvList.Rows[idx].FindControl("txtRemarks") as System.Web.UI.WebControls.TextBox; if (tb != null) rem = tb.Text; }
            string s = e.CommandName == "APP" ? "APPROVED" : "REJECTED";
            try {
                DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_ENQUIRY_HEADER SET STATUS=:s,APPROVER=:sno WHERE ENQUIRY_NO=:n", DBHelper.P(":s",s), DBHelper.P(":sno",SessionHelper.StaffNo(Session)), DBHelper.P(":n",no));
                DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_ENQUIRY_PROCESS_LOGS(ENQUIRY_NO,STAFFNO,STATUS,TRANSACTION_DT,PCNAME_IPADDRESS,LOGIN_STAFFNO,REMARKS) VALUES(:n,:sno,:s,SYSTIMESTAMP,:ip,:sno,:r)", DBHelper.P(":n",no), DBHelper.P(":sno",SessionHelper.StaffNo(Session)), DBHelper.P(":s",s), DBHelper.P(":ip",Request.UserHostAddress), DBHelper.P(":r",rem));
                lblMsg.Text=$"Enquiry {no} {s}."; ScriptManager.RegisterStartupScript(this,GetType(),"t",$"toastr.success('{s}!');",true);
            } catch (Exception ex) { lblMsg.Text="Error: "+ex.Message; }
            Load();
        }
    }
}
