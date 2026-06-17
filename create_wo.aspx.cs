using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IWPS
{
    public partial class create_wo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLois();
                LoadNotes();
                txtWoDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

                string woNo = Request.QueryString["wono"];
                if (!string.IsNullOrEmpty(woNo))
                {
                    hfWoNo.Value = woNo;
                    LoadWoForEdit(woNo);
                }
            }
        }

        private void LoadLois()
        {
            try
            {
                string dept = SessionHelper.Dept(Session);
                string sql = @"SELECT LOI_NO, LOI_NO || ' (' || ENQ_NO || ')' AS DISPLAY_TEXT
                               FROM MATPASS.TBL_IWPS_LOI_HEADER
                               WHERE STATUS = 'APPROVED' AND DEPT_CODE = :p1
                               ORDER BY LOI_NO";
                DataTable dt = DBHelper.ExecuteQuery(sql, DBHelper.P("p1", dept));
                ddlLoi.DataSource = dt;
                ddlLoi.DataTextField = "DISPLAY_TEXT";
                ddlLoi.DataValueField = "LOI_NO";
                ddlLoi.DataBind();
                ddlLoi.Items.Insert(0, new ListItem("-- Select LOI --", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading LOIs: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        private void LoadNotes()
        {
            try
            {
                string sql = "SELECT NOTE_CODE, NOTE_TITLE FROM MATPASS.TBL_IWPS_LOI_NOTE_MASTER ORDER BY NOTE_TITLE";
                DataTable dt = DBHelper.ExecuteQuery(sql);
                cblNotes.DataSource = dt;
                cblNotes.DataBind();
            }
            catch { }
        }

        protected void ddlLoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlLoiInfo.Visible = false;
            gvItems.DataSource = null;
            gvItems.DataBind();

            if (string.IsNullOrEmpty(ddlLoi.SelectedValue)) return;

            try
            {
                string sql = @"SELECT h.ENQ_NO, h.CONTRACT_VALUE, NVL(e.PARTY_NAME,'N/A') AS PARTY_NAME,
                                      h.WORK_DESCRIPTION
                               FROM MATPASS.TBL_IWPS_LOI_HEADER h
                               LEFT JOIN MATPASS.TBL_IWPS_ENQ_HEADER e ON e.ENQ_NO = h.ENQ_NO
                               WHERE h.LOI_NO = :p1";
                DataTable dt = DBHelper.ExecuteQuery(sql, DBHelper.P("p1", ddlLoi.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    lblEnqNo.Text = dt.Rows[0]["ENQ_NO"].ToString();
                    lblPartyName.Text = dt.Rows[0]["PARTY_NAME"].ToString();
                    lblContractValue.Text = Convert.ToDecimal(dt.Rows[0]["CONTRACT_VALUE"]).ToString("N2");
                    txtWoContractValue.Text = dt.Rows[0]["CONTRACT_VALUE"].ToString();
                    txtWoDescription.Text = dt.Rows[0]["WORK_DESCRIPTION"].ToString();
                    pnlLoiInfo.Visible = true;

                    // Load items from enquiry
                    string enqNo = dt.Rows[0]["ENQ_NO"].ToString();
                    string sqlItems = @"SELECT ITEM_CODE, ITEM_DESCRIPTION, UOM, QTY, UNIT_RATE
                                        FROM MATPASS.TBL_IWPS_ENQ_ITEMS
                                        WHERE ENQ_NO = :p1 ORDER BY ITEM_CODE";
                    DataTable dtItems = DBHelper.ExecuteQuery(sqlItems, DBHelper.P("p1", enqNo));
                    gvItems.DataSource = dtItems;
                    gvItems.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading LOI info: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        private void LoadWoForEdit(string woNo)
        {
            try
            {
                string sql = @"SELECT LOI_NO, WO_DATE, WO_START_DATE, WO_END_DATE, WO_DESCRIPTION,
                                      CONTRACT_VALUE, TAX_APPLICABLE, TAX_AMOUNT
                               FROM MATPASS.TBL_IWPS_WO_HEADER WHERE WO_NO = :p1";
                DataTable dt = DBHelper.ExecuteQuery(sql, DBHelper.P("p1", woNo));
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    ddlLoi.SelectedValue = row["LOI_NO"].ToString();
                    ddlLoi_SelectedIndexChanged(null, null);
                    txtWoDate.Text = Convert.ToDateTime(row["WO_DATE"]).ToString("yyyy-MM-dd");
                    txtWoStartDate.Text = Convert.ToDateTime(row["WO_START_DATE"]).ToString("yyyy-MM-dd");
                    txtWoEndDate.Text = Convert.ToDateTime(row["WO_END_DATE"]).ToString("yyyy-MM-dd");
                    txtWoDescription.Text = row["WO_DESCRIPTION"].ToString();
                    txtWoContractValue.Text = row["CONTRACT_VALUE"].ToString();
                    ddlTaxApplicable.SelectedValue = row["TAX_APPLICABLE"].ToString();
                    txtTaxAmount.Text = row["TAX_AMOUNT"].ToString();
                }

                // Load selected notes
                string sqlNotes = @"SELECT NOTE_CODE FROM MATPASS.TBL_IWPS_WO_NOTES WHERE WO_NO = :p1";
                DataTable dtNotes = DBHelper.ExecuteQuery(sqlNotes, DBHelper.P("p1", woNo));
                foreach (DataRow nr in dtNotes.Rows)
                {
                    foreach (ListItem li in cblNotes.Items)
                        if (li.Value == nr["NOTE_CODE"].ToString()) { li.Selected = true; break; }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading WO: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            SaveWo("DRAFT");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            SaveWo("INITIATED");
        }

        private void SaveWo(string status)
        {
            try
            {
                string staffNo = SessionHelper.StaffNo(Session);
                string dept = SessionHelper.Dept(Session);
                string woNo = hfWoNo.Value;
                bool isNew = string.IsNullOrEmpty(woNo);

                if (isNew)
                {
                    long seq = DBHelper.GetNextSeq("WO_SEQ");
                    woNo = $"WO-{dept}-{DateTime.Today:yyyyMMdd}-{seq:D3}";
                    hfWoNo.Value = woNo;

                    string taxApp = ddlTaxApplicable.SelectedValue;
                    string taxAmt = taxApp == "Y" ? txtTaxAmount.Text.Trim() : "0";

                    string sqlHeader = @"INSERT INTO MATPASS.TBL_IWPS_WO_HEADER
                        (WO_NO, LOI_NO, ENQ_NO, WO_DATE, WO_START_DATE, WO_END_DATE, WO_DESCRIPTION,
                         CONTRACT_VALUE, TAX_APPLICABLE, TAX_AMOUNT, STATUS, CREATED_BY, CREATED_DATE, DEPT_CODE)
                        VALUES (:p1,:p2,:p3,TO_DATE(:p4,'YYYY-MM-DD'),TO_DATE(:p5,'YYYY-MM-DD'),TO_DATE(:p6,'YYYY-MM-DD'),
                                :p7,:p8,:p9,:p10,:p11,:p12,SYSTIMESTAMP,:p13)";

                    DBHelper.ExecuteNonQuery(sqlHeader,
                        DBHelper.P("p1", woNo),
                        DBHelper.P("p2", ddlLoi.SelectedValue),
                        DBHelper.P("p3", lblEnqNo.Text),
                        DBHelper.P("p4", txtWoDate.Text.Trim()),
                        DBHelper.P("p5", txtWoStartDate.Text.Trim()),
                        DBHelper.P("p6", txtWoEndDate.Text.Trim()),
                        DBHelper.P("p7", txtWoDescription.Text.Trim()),
                        DBHelper.P("p8", txtWoContractValue.Text.Trim()),
                        DBHelper.P("p9", taxApp),
                        DBHelper.P("p10", taxAmt),
                        DBHelper.P("p11", status),
                        DBHelper.P("p12", staffNo),
                        DBHelper.P("p13", dept));

                    // Insert items
                    foreach (GridViewRow row in gvItems.Rows)
                    {
                        TextBox txtQty = (TextBox)row.FindControl("txtQty");
                        TextBox txtUnitRate = (TextBox)row.FindControl("txtUnitRate");
                        string itemCode = gvItems.DataKeys != null && gvItems.DataKeys.Count > row.RowIndex
                            ? gvItems.DataKeys[row.RowIndex].Value.ToString()
                            : row.Cells[0].Text;

                        string sqlItem = @"INSERT INTO MATPASS.TBL_IWPS_WO_ITEMS (WO_NO, ITEM_CODE, QTY, UNIT_RATE, CREATED_BY, CREATED_DATE)
                                          VALUES (:p1,:p2,:p3,:p4,:p5,SYSTIMESTAMP)";
                        DBHelper.ExecuteNonQuery(sqlItem,
                            DBHelper.P("p1", woNo),
                            DBHelper.P("p2", itemCode),
                            DBHelper.P("p3", txtQty?.Text.Trim() ?? "0"),
                            DBHelper.P("p4", txtUnitRate?.Text.Trim() ?? "0"),
                            DBHelper.P("p5", staffNo));
                    }

                    // Insert notes
                    foreach (ListItem item in cblNotes.Items)
                    {
                        if (item.Selected)
                        {
                            string sqlNote = @"INSERT INTO MATPASS.TBL_IWPS_WO_NOTES (WO_NO, NOTE_CODE, CREATED_BY, CREATED_DATE)
                                               VALUES (:p1,:p2,:p3,SYSTIMESTAMP)";
                            DBHelper.ExecuteNonQuery(sqlNote,
                                DBHelper.P("p1", woNo),
                                DBHelper.P("p2", item.Value),
                                DBHelper.P("p3", staffNo));
                        }
                    }

                    // Insert process log
                    string sqlLog = @"INSERT INTO MATPASS.TBL_IWPS_WO_PROCESS_LOGS (WO_NO, ACTION, REMARKS, ACTION_BY, ACTION_DATE)
                                      VALUES (:p1,:p2,:p3,:p4,SYSTIMESTAMP)";
                    DBHelper.ExecuteNonQuery(sqlLog,
                        DBHelper.P("p1", woNo),
                        DBHelper.P("p2", status == "DRAFT" ? "SAVED AS DRAFT" : "SUBMITTED"),
                        DBHelper.P("p3", ""),
                        DBHelper.P("p4", staffNo));
                }
                else
                {
                    // Update existing
                    string taxApp = ddlTaxApplicable.SelectedValue;
                    string taxAmt = taxApp == "Y" ? txtTaxAmount.Text.Trim() : "0";

                    string sqlUpdate = @"UPDATE MATPASS.TBL_IWPS_WO_HEADER SET
                        WO_DATE=TO_DATE(:p1,'YYYY-MM-DD'), WO_START_DATE=TO_DATE(:p2,'YYYY-MM-DD'),
                        WO_END_DATE=TO_DATE(:p3,'YYYY-MM-DD'), WO_DESCRIPTION=:p4,
                        CONTRACT_VALUE=:p5, TAX_APPLICABLE=:p6, TAX_AMOUNT=:p7,
                        STATUS=:p8, UPDATED_BY=:p9, UPDATED_DATE=SYSTIMESTAMP
                        WHERE WO_NO=:p10";
                    DBHelper.ExecuteNonQuery(sqlUpdate,
                        DBHelper.P("p1", txtWoDate.Text.Trim()),
                        DBHelper.P("p2", txtWoStartDate.Text.Trim()),
                        DBHelper.P("p3", txtWoEndDate.Text.Trim()),
                        DBHelper.P("p4", txtWoDescription.Text.Trim()),
                        DBHelper.P("p5", txtWoContractValue.Text.Trim()),
                        DBHelper.P("p6", taxApp),
                        DBHelper.P("p7", taxAmt),
                        DBHelper.P("p8", status),
                        DBHelper.P("p9", staffNo),
                        DBHelper.P("p10", woNo));

                    // Re-insert items
                    DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_WO_ITEMS WHERE WO_NO=:p1", DBHelper.P("p1", woNo));
                    foreach (GridViewRow row in gvItems.Rows)
                    {
                        TextBox txtQty = (TextBox)row.FindControl("txtQty");
                        TextBox txtUnitRate = (TextBox)row.FindControl("txtUnitRate");
                        string itemCode = row.Cells[0].Text;
                        string sqlItem = @"INSERT INTO MATPASS.TBL_IWPS_WO_ITEMS (WO_NO, ITEM_CODE, QTY, UNIT_RATE, CREATED_BY, CREATED_DATE)
                                          VALUES (:p1,:p2,:p3,:p4,:p5,SYSTIMESTAMP)";
                        DBHelper.ExecuteNonQuery(sqlItem,
                            DBHelper.P("p1", woNo),
                            DBHelper.P("p2", itemCode),
                            DBHelper.P("p3", txtQty?.Text.Trim() ?? "0"),
                            DBHelper.P("p4", txtUnitRate?.Text.Trim() ?? "0"),
                            DBHelper.P("p5", staffNo));
                    }

                    // Re-insert notes
                    DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_WO_NOTES WHERE WO_NO=:p1", DBHelper.P("p1", woNo));
                    foreach (ListItem li in cblNotes.Items)
                    {
                        if (li.Selected)
                        {
                            DBHelper.ExecuteNonQuery(
                                "INSERT INTO MATPASS.TBL_IWPS_WO_NOTES (WO_NO, NOTE_CODE, CREATED_BY, CREATED_DATE) VALUES (:p1,:p2,:p3,SYSTIMESTAMP)",
                                DBHelper.P("p1", woNo), DBHelper.P("p2", li.Value), DBHelper.P("p3", staffNo));
                        }
                    }

                    string sqlLog = @"INSERT INTO MATPASS.TBL_IWPS_WO_PROCESS_LOGS (WO_NO, ACTION, REMARKS, ACTION_BY, ACTION_DATE)
                                      VALUES (:p1,:p2,:p3,:p4,SYSTIMESTAMP)";
                    DBHelper.ExecuteNonQuery(sqlLog,
                        DBHelper.P("p1", woNo),
                        DBHelper.P("p2", status == "DRAFT" ? "UPDATED DRAFT" : "RE-SUBMITTED"),
                        DBHelper.P("p3", ""),
                        DBHelper.P("p4", staffNo));
                }

                string action = status == "DRAFT" ? "saved as draft" : "submitted";
                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    $"toastr.success('Work Order {woNo} {action} successfully.'); setTimeout(function(){{ window.location='Draft_wo_list.aspx'; }}, 2000);", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error saving Work Order: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Draft_wo_list.aspx");
        }
    }
}
