using System;
using System.Data;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class item_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                string id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                    LoadRecord(id);
                else
                    txtItemCode.Text = GenerateItemCode();
            }
        }

        private void LoadCategories()
        {
            try
            {
                DataTable dt = DBHelper.ExecuteQuery("SELECT CAT_CODE, CAT_TITLE FROM MATPASS.TBL_IWPS_CATEGORY_MASTER WHERE STATUS='ACTIVE' ORDER BY CAT_TITLE", new OracleParameter[0]);
                ddlCategory.Items.Clear();
                ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem("-- Select Category --", ""));
                foreach (DataRow row in dt.Rows)
                    ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem(row["CAT_CODE"] + " - " + row["CAT_TITLE"], row["CAT_CODE"].ToString()));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadCategories error: " + ex.Message);
            }
        }

        private string GenerateItemCode()
        {
            try
            {
                return DBHelper.GetNextSequence("ITM-", "MATPASS.TBL_IWPS_ITEM_MASTER", "ITEM_CODE");
            }
            catch
            {
                return "ITM-0001";
            }
        }

        private void LoadRecord(string itemCode)
        {
            try
            {
                var p = new OracleParameter[] { new OracleParameter("ic", itemCode) };
                DataTable dt = DBHelper.ExecuteQuery("SELECT * FROM MATPASS.TBL_IWPS_ITEM_MASTER WHERE ITEM_CODE=:ic", p);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtItemCode.Text = row["ITEM_CODE"].ToString();
                    ddlCategory.SelectedValue = row["CAT_CODE"].ToString();
                    txtDSRCode.Text = row["DSR_CODE"].ToString();
                    txtDesc.Text = row["ITEM_DESC"].ToString();
                    ddlUnit.SelectedValue = row["UNIT"].ToString();
                    txtRate.Text = row["RATE"].ToString();
                    hfItemCode.Value = itemCode;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Load Item error: " + ex.Message);
            }
        }

        private void SaveItem(string status)
        {
            if (string.IsNullOrEmpty(txtDesc.Text.Trim()))
            {
                lblError.Text = "Description is required.";
                lblError.Visible = true;
                return;
            }
            string staffNo = SessionHelper.StaffNo(Session);
            string itemCode = txtItemCode.Text.Trim();
            try
            {
                if (string.IsNullOrEmpty(hfItemCode.Value))
                {
                    string sql = @"INSERT INTO MATPASS.TBL_IWPS_ITEM_MASTER
                        (ITEM_CODE, CAT_CODE, DSR_CODE, ITEM_DESC, UNIT, RATE, STATUS, INITIATOR, INITIATION_DATE)
                        VALUES (:ic, :cc, :dc, :desc, :unit, :rate, :status, :init, SYSDATE)";
                    decimal rate = 0;
                    decimal.TryParse(txtRate.Text, out rate);
                    var p = new OracleParameter[] {
                        new OracleParameter("ic", itemCode),
                        new OracleParameter("cc", ddlCategory.SelectedValue),
                        new OracleParameter("dc", txtDSRCode.Text.Trim()),
                        new OracleParameter("desc", txtDesc.Text.Trim()),
                        new OracleParameter("unit", ddlUnit.SelectedValue),
                        new OracleParameter("rate", rate),
                        new OracleParameter("status", status),
                        new OracleParameter("init", staffNo)
                    };
                    DBHelper.ExecuteNonQuery(sql, p);
                    hfItemCode.Value = itemCode;
                }
                else
                {
                    decimal rate = 0;
                    decimal.TryParse(txtRate.Text, out rate);
                    string sql = @"UPDATE MATPASS.TBL_IWPS_ITEM_MASTER SET
                        CAT_CODE=:cc, DSR_CODE=:dc, ITEM_DESC=:desc, UNIT=:unit, RATE=:rate, STATUS=:status
                        WHERE ITEM_CODE=:ic";
                    var p = new OracleParameter[] {
                        new OracleParameter("cc", ddlCategory.SelectedValue),
                        new OracleParameter("dc", txtDSRCode.Text.Trim()),
                        new OracleParameter("desc", txtDesc.Text.Trim()),
                        new OracleParameter("unit", ddlUnit.SelectedValue),
                        new OracleParameter("rate", rate),
                        new OracleParameter("status", status),
                        new OracleParameter("ic", itemCode)
                    };
                    DBHelper.ExecuteNonQuery(sql, p);
                }

                string logSql = @"INSERT INTO MATPASS.TBL_IWPS_ITEM_MASTER_LOGS
                    (ITEM_CODE, ACTION, ACTION_BY, ACTION_DATE, REMARKS)
                    VALUES (:ic, :act, :ab, SYSDATE, :rem)";
                DBHelper.ExecuteNonQuery(logSql, new OracleParameter[] {
                    new OracleParameter("ic", itemCode),
                    new OracleParameter("act", status == "DRAFT" ? "SAVE DRAFT" : "SUBMIT"),
                    new OracleParameter("ab", staffNo),
                    new OracleParameter("rem", status)
                });

                string msg = status == "DRAFT" ? "toastr.success('Saved as Draft!');" : "toastr.success('Item submitted successfully!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "toast", msg, true);
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e) => SaveItem("DRAFT");
        protected void btnSubmit_Click(object sender, EventArgs e) => SaveItem("INITIATED");

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfItemCode.Value = "";
            txtDesc.Text = "";
            txtDSRCode.Text = "";
            txtRate.Text = "";
            ddlCategory.SelectedIndex = 0;
            ddlUnit.SelectedIndex = 0;
            txtItemCode.Text = GenerateItemCode();
            lblError.Visible = false;
        }
    }
}
