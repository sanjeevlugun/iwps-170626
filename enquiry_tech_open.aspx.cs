using System; using System.Data; using System.Web.UI.WebControls;
namespace IWPS {
    public partial class enquiry_tech_open : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) Load(); }
        void Load() {
            try {
                gvEnq.DataSource = DBHelper.ExecuteQuery("SELECT ENQUIRY_NO,ENQUIRY_DATE,TITLE,BID_EVALUATION_METHOD,STATUS FROM MATPASS.TBL_IWPS_ENQUIRY_HEADER WHERE STATUS IN ('APPROVED','PART-I') AND (BID_EVALUATION_METHOD LIKE '%Two%' OR BID_EVALUATION_METHOD LIKE '%Three%') ORDER BY ENQUIRY_DATE DESC");
                gvEnq.DataBind();
            } catch { }
        }
        protected void gvEnq_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName != "OPEN") return;
            string no = e.CommandArgument.ToString();
            try {
                DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_ENQUIRY_HEADER SET STATUS='PART-I' WHERE ENQUIRY_NO=:n AND STATUS='APPROVED'", DBHelper.P(":n",no));
                DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_ENQUIRY_PROCESS_LOGS(ENQUIRY_NO,STAFFNO,STATUS,TRANSACTION_DT,PCNAME_IPADDRESS,LOGIN_STAFFNO) VALUES(:n,:sno,'PART-I',SYSTIMESTAMP,:ip,:sno)", DBHelper.P(":n",no), DBHelper.P(":sno",SessionHelper.StaffNo(Session)), DBHelper.P(":ip",Request.UserHostAddress));
                lblSelEnq.Text = no;
                ViewState["SelEnq"] = no;
                gvParties.DataSource = DBHelper.ExecuteQuery("SELECT ep.PARTY_CODE,NVL(pm.PARTY_NAME,ep.PARTY_CODE) PARTY_NAME,ep.EMD_STATUS,ep.QUALIFIED_STATUS FROM MATPASS.TBL_IWPS_ENQUIRY_PARTY ep LEFT JOIN MATPASS.TBL_IWPS_PARTY_MASTER pm ON ep.PARTY_CODE=pm.PARTY_CODE WHERE ep.ENQUIRY_NO=:n", DBHelper.P(":n",no));
                gvParties.DataBind();
                divTechEval.Visible = true;
                ScriptManager.RegisterStartupScript(this,GetType(),"t","toastr.success('Part-I Opened!');",true);
            } catch (Exception ex) { lblMsg.Text="Error: "+ex.Message; }
            Load();
        }
        protected void btnSaveTech_Click(object sender, EventArgs e) {
            string no = ViewState["SelEnq"]?.ToString();
            if (string.IsNullOrEmpty(no)) return;
            try {
                foreach (GridViewRow r in gvParties.Rows) {
                    string pc = gvParties.DataKeys[r.RowIndex].Value.ToString();
                    string emd = ((DropDownList)r.FindControl("ddlEMD")).SelectedValue;
                    string qual = ((DropDownList)r.FindControl("ddlQual")).SelectedValue;
                    DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_ENQUIRY_PARTY SET EMD_STATUS=:e,QUALIFIED_STATUS=:q WHERE ENQUIRY_NO=:n AND PARTY_CODE=:p", DBHelper.P(":e",emd), DBHelper.P(":q",qual), DBHelper.P(":n",no), DBHelper.P(":p",pc));
                }
                lblMsg.Text="Technical evaluation saved."; ScriptManager.RegisterStartupScript(this,GetType(),"t","toastr.success('Technical evaluation saved!');",true);
            } catch (Exception ex) { lblMsg.Text="Error: "+ex.Message; }
        }
    }
}
