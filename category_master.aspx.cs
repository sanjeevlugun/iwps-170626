using System;
using System.Data;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class category_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDept.Text = SessionHelper.Dept(Session);
                string id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    LoadRecord(id);
                }
                else
                {
                    txtCatCode.Text = GenerateCatCode();
                }
            }
        }

        private string GenerateCatCode()
        {
            string dept = SessionHelper.Dept(Session);
            try
            {
                return DBHelper.GetNextSequence("CAT-" + dept + "-", "MATPASS.TBL_IWPS_CATEGORY_MASTER", "CAT_CODE");
            }
            catch
            {
                return "CAT-" + dept + "-0001";
            }
        }

        private void LoadRecord(string catCode)
        {
            try
            {
                var p = new OracleParameter[] { new OracleParameter("cc", catCode) };
                DataTable dt = DBHelper.ExecuteQuery("SELECT * FROM MATPASS.TBL_IWPS_CATEGORY_MASTER WHERE CAT_CODE=:cc", p);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtCatCode.Text = row["CAT_CODE"].ToString();
                    txtCatTitle.Text = row["CAT_TITLE"].ToString();
                    txtCatDesc.Text = row["CAT_DESC"].ToString();
                    ddlTypeOfWork.SelectedValue = row["TYPE_OF_WORK"].ToString();
                    ddlWorkServiceType.SelectedValue = row["WORK_SERVICE_TYPE"].ToString();
                    txtDept.Text = row["DEPT"].ToString();
                    hfCatCode.Value = catCode;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Load Category error: " + ex.Message);
            }
        }

        private void SaveCategory(string status)
        {
            if (string.IsNullOrEmpty(txtCatTitle.Text.Trim()))
            {
                lblError.Text = "Category Title is required.";
                lblError.Visible = true;
                return;
            }

            string staffNo = SessionHelper.StaffNo(Session);
            string catCode = txtCatCode.Text.Trim();

            try
            {
                if (string.IsNullOrEmpty(hfCatCode.Value))
                {
                    // INSERT
                    string sql = @"INSERT INTO MATPASS.TBL_IWPS_CATEGORY_MASTER
                        (CAT_CODE, CAT_TITLE, CAT_DESC, TYPE_OF_WORK, WORK_SERVICE_TYPE, DEPT, STATUS, INITIATOR, INITIATION_DATE)
                        VALUES (:cc, :ct, :cd, :tow, :wst, :dept, :status, :init, SYSDATE)";
                    var p = new OracleParameter[] {
                        new OracleParameter("cc", catCode),
                        new OracleParameter("ct", txtCatTitle.Text.Trim()),
                        new OracleParameter("cd", txtCatDesc.Text.Trim()),
                        new OracleParameter("tow", ddlTypeOfWork.SelectedValue),
                        new OracleParameter("wst", ddlWorkServiceType.SelectedValue),
                        new OracleParameter("dept", txtDept.Text.Trim()),
                        new OracleParameter("status", status),
                        new OracleParameter("init", staffNo)
                    };
                    DBHelper.ExecuteNonQuery(sql, p);
                    hfCatCode.Value = catCode;
                }
                else
                {
                    // UPDATE
                    string sql = @"UPDATE MATPASS.TBL_IWPS_CATEGORY_MASTER SET
                        CAT_TITLE=:ct, CAT_DESC=:cd, TYPE_OF_WORK=:tow, WORK_SERVICE_TYPE=:wst, STATUS=:status
                        WHERE CAT_CODE=:cc";
                    var p = new OracleParameter[] {
                        new OracleParameter("ct", txtCatTitle.Text.Trim()),
                        new OracleParameter("cd", txtCatDesc.Text.Trim()),
                        new OracleParameter("tow", ddlTypeOfWork.SelectedValue),
                        new OracleParameter("wst", ddlWorkServiceType.SelectedValue),
                        new OracleParameter("status", status),
                        new OracleParameter("cc", catCode)
                    };
                    DBHelper.ExecuteNonQuery(sql, p);
                }

                // Log
                string logSql = @"INSERT INTO MATPASS.TBL_IWPS_CATEGORY_MASTER_LOGS
                    (CAT_CODE, ACTION, ACTION_BY, ACTION_DATE, REMARKS)
                    VALUES (:cc, :act, :ab, SYSDATE, :rem)";
                var lp = new OracleParameter[] {
                    new OracleParameter("cc", catCode),
                    new OracleParameter("act", status == "DRAFT" ? "SAVE DRAFT" : "SUBMIT"),
                    new OracleParameter("ab", staffNo),
                    new OracleParameter("rem", status)
                };
                DBHelper.ExecuteNonQuery(logSql, lp);

                string msg = status == "DRAFT" ? "toastr.success('Saved as Draft successfully!');" : "toastr.success('Submitted successfully!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "toast", msg, true);
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e) => SaveCategory("DRAFT");
        protected void btnSubmit_Click(object sender, EventArgs e) => SaveCategory("INITIATED");

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfCatCode.Value = "";
            txtCatTitle.Text = "";
            txtCatDesc.Text = "";
            ddlTypeOfWork.SelectedIndex = 0;
            ddlWorkServiceType.SelectedIndex = 0;
            txtCatCode.Text = GenerateCatCode();
            lblError.Visible = false;
        }
    }
}
