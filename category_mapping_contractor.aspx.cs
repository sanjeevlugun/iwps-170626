using System;
using System.Data;
using System.Web.UI.WebControls;
namespace IWPS {
    public partial class category_mapping_contractor : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataTable dt = DBHelper.ExecuteQuery("SELECT CATEGORY_CODE, CATEGORY_NAME FROM MATPASS.TBL_IWPS_CATEGORY_MASTER WHERE STATUS='ACTIVE' ORDER BY CATEGORY_NAME");
                ddlCategory.DataSource = dt; ddlCategory.DataTextField = "CATEGORY_NAME"; ddlCategory.DataValueField = "CATEGORY_CODE"; ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", ""));
            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlCategory.SelectedValue == "") { pnlContractors.Visible = false; return; }
            LoadContractors();
        }
        void LoadContractors() {
            try {
                string catCode = ddlCategory.SelectedValue;
                DataTable dt = DBHelper.ExecuteQuery(@"SELECT c.CONTRACTOR_ID, c.CONTRACTOR_NAME, c.CONTRACTOR_TYPE, CASE WHEN m.CATEGORY_CODE IS NOT NULL THEN 1 ELSE 0 END IS_MAPPED FROM MATPASS.TBL_IWPS_CONTRACTOR_MASTER c LEFT JOIN MATPASS.TBL_IWPS_CATEGORY_CONTRACTOR_MAPPING m ON m.CONTRACTOR_ID=c.CONTRACTOR_ID AND m.CATEGORY_CODE=:cc ORDER BY c.CONTRACTOR_NAME", DBHelper.P("cc", catCode));
                gvContractors.DataSource = dt; gvContractors.DataBind(); pnlContractors.Visible = true;
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "err", $"toastr.error('{ex.Message.Replace("'","\\'")}');", true);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e) {
            string catCode = ddlCategory.SelectedValue;
            try {
                DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_CATEGORY_CONTRACTOR_MAPPING WHERE CATEGORY_CODE=:cc", DBHelper.P("cc", catCode));
                foreach (GridViewRow row in gvContractors.Rows) {
                    CheckBox chk = (CheckBox)row.FindControl("chkMap");
                    if (chk != null && chk.Checked) {
                        string contId = gvContractors.DataKeys[row.RowIndex].Value.ToString();
                        DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_CATEGORY_CONTRACTOR_MAPPING (CATEGORY_CODE,CONTRACTOR_ID,MAPPED_BY,MAPPED_DATE) VALUES (:cc,:cid,:mb,SYSTIMESTAMP)",
                            DBHelper.P("cc", catCode), DBHelper.P("cid", contId), DBHelper.P("mb", SessionHelper.StaffNo(Session)));
                    }
                }
                ClientScript.RegisterStartupScript(GetType(), "ok", "toastr.success('Mapping saved.');", true);
                LoadContractors();
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "err", $"toastr.error('{ex.Message.Replace("'","\\'")}');", true);
            }
        }
    }
}
