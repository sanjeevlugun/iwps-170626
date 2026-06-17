using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class initiate_indent : System.Web.UI.Page
    {
        private DataTable GetOrCreateItemsDT()
        {
            if (ViewState["IndentItems"] is DataTable dt) return dt;
            dt = new DataTable();
            dt.Columns.Add("SL_NO", typeof(int));
            dt.Columns.Add("ITEM_CODE", typeof(string));
            dt.Columns.Add("ITEM_DESCRIPTION", typeof(string));
            dt.Columns.Add("QTY", typeof(decimal));
            dt.Columns.Add("RATE", typeof(decimal));
            dt.Columns.Add("VALUE", typeof(decimal));
            ViewState["IndentItems"] = dt;
            return dt;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                txtIndentDept.Text = SessionHelper.Dept(Session);
                LoadCategories();
                LoadItems();
                string id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                    LoadExistingIndent(id);
                else
                    txtIndentNo.Text = GenerateIndentNo();
            }
        }

        private string GenerateIndentNo()
        {
            string dept = SessionHelper.Dept(Session);
            try
            {
                object max = DBHelper.ExecuteScalar("SELECT MAX(INDENT_NO) FROM MATPASS.TBL_IWPS_INDENT_HEADER WHERE INDENT_NO LIKE 'IND-" + dept + "-%'", new OracleParameter[0]);
                int seq = 1;
                if (max != null && max != DBNull.Value)
                {
                    string maxStr = max.ToString();
                    string[] parts = maxStr.Split('-');
                    if (parts.Length >= 4 && int.TryParse(parts[parts.Length - 1], out int n)) seq = n + 1;
                }
                return string.Format("IND-{0}-{1}-{2}", dept, DateTime.Today.ToString("yyyyMMdd"), seq.ToString("D3"));
            }
            catch
            {
                return string.Format("IND-{0}-{1}-001", dept, DateTime.Today.ToString("yyyyMMdd"));
            }
        }

        private void LoadCategories()
        {
            try
            {
                DataTable dt = DBHelper.ExecuteQuery("SELECT CAT_CODE, CAT_TITLE FROM MATPASS.TBL_IWPS_CATEGORY_MASTER WHERE STATUS='ACTIVE' ORDER BY CAT_TITLE", new OracleParameter[0]);
                ddlCategory.Items.Clear();
                ddlCategory.Items.Add(new ListItem("-- Select Category --", ""));
                foreach (DataRow row in dt.Rows)
                    ddlCategory.Items.Add(new ListItem(row["CAT_CODE"] + " - " + row["CAT_TITLE"], row["CAT_CODE"].ToString()));
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("LoadCategories: " + ex.Message); }
        }

        private void LoadItems()
        {
            try
            {
                DataTable dt = DBHelper.ExecuteQuery("SELECT ITEM_CODE, ITEM_DESC, UNIT, RATE FROM MATPASS.TBL_IWPS_ITEM_MASTER WHERE STATUS='ACTIVE' ORDER BY ITEM_DESC", new OracleParameter[0]);
                ddlItem.Items.Clear();
                ddlItem.Items.Add(new ListItem("-- Select Item --", ""));
                foreach (DataRow row in dt.Rows)
                    ddlItem.Items.Add(new ListItem(row["ITEM_CODE"] + " - " + row["ITEM_DESC"].ToString().Substring(0, Math.Min(40, row["ITEM_DESC"].ToString().Length)), row["ITEM_CODE"].ToString()));
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("LoadItems: " + ex.Message); }
        }

        private void LoadExistingIndent(string indentNo)
        {
            try
            {
                var p = new OracleParameter[] { new OracleParameter("ino", indentNo) };
                DataTable dt = DBHelper.ExecuteQuery("SELECT * FROM MATPASS.TBL_IWPS_INDENT_HEADER WHERE INDENT_NO=:ino", p);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtIndentNo.Text = indentNo;
                    txtTitle.Text = row["TITLE"].ToString();
                    ddlCategory.SelectedValue = row["CAT_CODE"].ToString();
                    txtRemarks.Text = row["REMARKS"].ToString();
                    txtTotalEstimate.Text = row["TOTAL_ESTIMATE"].ToString();
                    hfIndentNo.Value = indentNo;
                    // Load existing items
                    DataTable dtItems = DBHelper.ExecuteQuery("SELECT ii.SL_NO, ii.ITEM_CODE, ii.ITEM_DESCRIPTION, ii.QTY, ii.RATE, ii.VALUE FROM MATPASS.TBL_IWPS_INDENT_ITEM ii WHERE ii.INDENT_NO=:ino ORDER BY ii.SL_NO", p);
                    DataTable vsDT = GetOrCreateItemsDT();
                    foreach (DataRow ir in dtItems.Rows)
                        vsDT.Rows.Add(ir["SL_NO"], ir["ITEM_CODE"], ir["ITEM_DESCRIPTION"], ir["QTY"], ir["RATE"], ir["VALUE"]);
                    ViewState["IndentItems"] = vsDT;
                    gvItems.DataSource = vsDT;
                    gvItems.DataBind();
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("LoadExistingIndent: " + ex.Message); }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlItem.SelectedValue)) return;
            decimal qty;
            if (!decimal.TryParse(txtQty.Text, out qty) || qty <= 0)
            {
                lblError.Text = "Please enter a valid quantity.";
                lblError.Visible = true;
                return;
            }
            decimal rate;
            decimal.TryParse(txtRate.Text, out rate);

            string itemCode = ddlItem.SelectedValue;
            string itemDesc = "";
            // Get item details
            try
            {
                var p = new OracleParameter[] { new OracleParameter("ic", itemCode) };
                DataTable dt = DBHelper.ExecuteQuery("SELECT ITEM_DESC, RATE FROM MATPASS.TBL_IWPS_ITEM_MASTER WHERE ITEM_CODE=:ic", p);
                if (dt.Rows.Count > 0)
                {
                    itemDesc = dt.Rows[0]["ITEM_DESC"].ToString();
                    if (rate == 0) decimal.TryParse(dt.Rows[0]["RATE"].ToString(), out rate);
                }
            }
            catch { }

            DataTable itemsDT = GetOrCreateItemsDT();
            decimal value = qty * rate;
            itemsDT.Rows.Add(itemsDT.Rows.Count + 1, itemCode, itemDesc, qty, rate, value);
            ViewState["IndentItems"] = itemsDT;
            RecalcTotal(itemsDT);
            gvItems.DataSource = itemsDT;
            gvItems.DataBind();
            lblError.Visible = false;
        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "REMOVE")
            {
                int idx = Convert.ToInt32(e.CommandArgument);
                DataTable dt = GetOrCreateItemsDT();
                if (idx < dt.Rows.Count) dt.Rows.RemoveAt(idx);
                // Renumber
                for (int i = 0; i < dt.Rows.Count; i++) dt.Rows[i]["SL_NO"] = i + 1;
                ViewState["IndentItems"] = dt;
                RecalcTotal(dt);
                gvItems.DataSource = dt;
                gvItems.DataBind();
            }
        }

        private void RecalcTotal(DataTable dt)
        {
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                decimal v;
                if (decimal.TryParse(row["VALUE"].ToString(), out v)) total += v;
            }
            txtTotalEstimate.Text = total.ToString("N2");
        }

        private void SaveIndent(string status)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                lblError.Text = "Title is required.";
                lblError.Visible = true;
                return;
            }
            string staffNo = SessionHelper.StaffNo(Session);
            string indentNo = txtIndentNo.Text.Trim();
            DataTable itemsDT = GetOrCreateItemsDT();
            decimal totalEst;
            decimal.TryParse(txtTotalEstimate.Text, out totalEst);
            try
            {
                if (string.IsNullOrEmpty(hfIndentNo.Value))
                {
                    string sql = @"INSERT INTO MATPASS.TBL_IWPS_INDENT_HEADER
                        (INDENT_NO, INDENT_DATE, TITLE, INDENT_DEPT, ENQ_DEPT, CAT_CODE, TOTAL_ESTIMATE, REMARKS, STATUS, INITIATOR, INITIATION_DATE)
                        VALUES (:ino, SYSDATE, :title, :idept, :edept, :cat, :est, :rem, :status, :init, SYSDATE)";
                    DBHelper.ExecuteNonQuery(sql, new OracleParameter[] {
                        new OracleParameter("ino", indentNo),
                        new OracleParameter("title", txtTitle.Text.Trim()),
                        new OracleParameter("idept", txtIndentDept.Text.Trim()),
                        new OracleParameter("edept", ddlEnqDept.SelectedValue),
                        new OracleParameter("cat", ddlCategory.SelectedValue),
                        new OracleParameter("est", totalEst),
                        new OracleParameter("rem", txtRemarks.Text.Trim()),
                        new OracleParameter("status", status),
                        new OracleParameter("init", staffNo)
                    });
                    foreach (DataRow item in itemsDT.Rows)
                    {
                        DBHelper.ExecuteNonQuery(@"INSERT INTO MATPASS.TBL_IWPS_INDENT_ITEM (INDENT_NO, SL_NO, ITEM_CODE, ITEM_DESCRIPTION, QTY, RATE, VALUE) VALUES (:ino,:sl,:ic,:idesc,:qty,:rate,:val)",
                            new OracleParameter[] {
                                new OracleParameter("ino", indentNo),
                                new OracleParameter("sl", item["SL_NO"]),
                                new OracleParameter("ic", item["ITEM_CODE"]),
                                new OracleParameter("idesc", item["ITEM_DESCRIPTION"]),
                                new OracleParameter("qty", item["QTY"]),
                                new OracleParameter("rate", item["RATE"]),
                                new OracleParameter("val", item["VALUE"])
                            });
                    }
                    hfIndentNo.Value = indentNo;
                }
                else
                {
                    DBHelper.ExecuteNonQuery(@"UPDATE MATPASS.TBL_IWPS_INDENT_HEADER SET TITLE=:title, ENQ_DEPT=:edept, CAT_CODE=:cat, TOTAL_ESTIMATE=:est, REMARKS=:rem, STATUS=:status WHERE INDENT_NO=:ino",
                        new OracleParameter[] {
                            new OracleParameter("title", txtTitle.Text.Trim()),
                            new OracleParameter("edept", ddlEnqDept.SelectedValue),
                            new OracleParameter("cat", ddlCategory.SelectedValue),
                            new OracleParameter("est", totalEst),
                            new OracleParameter("rem", txtRemarks.Text.Trim()),
                            new OracleParameter("status", status),
                            new OracleParameter("ino", indentNo)
                        });
                    DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_INDENT_ITEM WHERE INDENT_NO=:ino", new OracleParameter[] { new OracleParameter("ino", indentNo) });
                    foreach (DataRow item in itemsDT.Rows)
                    {
                        DBHelper.ExecuteNonQuery(@"INSERT INTO MATPASS.TBL_IWPS_INDENT_ITEM (INDENT_NO, SL_NO, ITEM_CODE, ITEM_DESCRIPTION, QTY, RATE, VALUE) VALUES (:ino,:sl,:ic,:idesc,:qty,:rate,:val)",
                            new OracleParameter[] {
                                new OracleParameter("ino", indentNo), new OracleParameter("sl", item["SL_NO"]),
                                new OracleParameter("ic", item["ITEM_CODE"]), new OracleParameter("idesc", item["ITEM_DESCRIPTION"]),
                                new OracleParameter("qty", item["QTY"]), new OracleParameter("rate", item["RATE"]), new OracleParameter("val", item["VALUE"])
                            });
                    }
                }
                // Log
                DBHelper.ExecuteNonQuery(@"INSERT INTO MATPASS.TBL_IWPS_INDENT_PROCESS_LOGS (INDENT_NO, ACTION, ACTION_BY, ACTION_DATE, REMARKS) VALUES (:ino,:act,:ab,SYSDATE,:rem)",
                    new OracleParameter[] {
                        new OracleParameter("ino", indentNo),
                        new OracleParameter("act", status == "DRAFT" ? "SAVE DRAFT" : "SUBMIT"),
                        new OracleParameter("ab", staffNo),
                        new OracleParameter("rem", status)
                    });
                string msg = status == "DRAFT" ? "toastr.success('Indent saved as Draft!');" : "toastr.success('Indent submitted successfully!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "toast", msg, true);
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e) => SaveIndent("DRAFT");
        protected void btnSubmit_Click(object sender, EventArgs e) => SaveIndent("INITIATED");

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfIndentNo.Value = "";
            txtTitle.Text = "";
            txtRemarks.Text = "";
            txtTotalEstimate.Text = "";
            ViewState["IndentItems"] = null;
            gvItems.DataSource = GetOrCreateItemsDT();
            gvItems.DataBind();
            txtIndentNo.Text = GenerateIndentNo();
            lblError.Visible = false;
        }
    }
}
