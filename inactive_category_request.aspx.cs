using System;
using System.Data;
namespace IWPS {
    public partial class inactive_category_request : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataTable dt = DBHelper.ExecuteQuery("SELECT CATEGORY_CODE, CATEGORY_NAME FROM MATPASS.TBL_IWPS_CATEGORY_MASTER WHERE STATUS='ACTIVE' ORDER BY CATEGORY_NAME");
                ddlCategory.DataSource = dt; ddlCategory.DataTextField = "CATEGORY_NAME"; ddlCategory.DataValueField = "CATEGORY_CODE"; ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Category --", ""));
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e) {
            if (ddlCategory.SelectedValue == "" || string.IsNullOrWhiteSpace(txtReason.Text)) {
                ClientScript.RegisterStartupScript(GetType(), "w", "toastr.warning('Select category and enter reason.');", true); return;
            }
            try {
                DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_CATEGORY_INACTIVE_REQ (CATEGORY_CODE,REASON,STATUS,REQUESTED_BY,REQUEST_DATE) VALUES (:cc,:reason,'INITIATED',:rb,SYSTIMESTAMP)",
                    DBHelper.P("cc", ddlCategory.SelectedValue), DBHelper.P("reason", txtReason.Text.Trim()), DBHelper.P("rb", SessionHelper.StaffNo(Session)));
                ClientScript.RegisterStartupScript(GetType(), "ok", "toastr.success('Inactivation request submitted.');", true);
                ddlCategory.SelectedIndex = 0; txtReason.Text = "";
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "err", $"toastr.error('{ex.Message.Replace("'","\\'")}');", true);
            }
        }
    }
}
