using System;
using System.Data;
using System.Web.UI;

namespace IWPS
{
    public partial class App_wo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string woNo = Request.QueryString["id"];
                if (string.IsNullOrEmpty(woNo))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "err", "toastr.error('No Work Order specified.'); setTimeout(function(){ history.back(); }, 2000);", true);
                    return;
                }
                hfWoNo.Value = woNo;
                LoadWoDetails(woNo);
            }
        }

        private void LoadWoDetails(string woNo)
        {
            try
            {
                string sql = @"SELECT w.WO_NO, w.LOI_NO, w.ENQ_NO, w.WO_DATE, w.WO_START_DATE, w.WO_END_DATE,
                                      w.WO_DESCRIPTION, w.CONTRACT_VALUE, w.TAX_APPLICABLE, w.TAX_AMOUNT,
                                      w.STATUS, w.CREATED_BY, w.DEPT_CODE,
                                      NVL(e.PARTY_NAME,'N/A') AS PARTY_NAME
                               FROM MATPASS.TBL_IWPS_WO_HEADER w
                               LEFT JOIN MATPASS.TBL_IWPS_ENQ_HEADER e ON e.ENQ_NO = w.ENQ_NO
                               WHERE w.WO_NO = :p1";
                DataTable dt = DBHelper.ExecuteQuery(sql, DBHelper.P("p1", woNo));

                if (dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "err", "toastr.error('Work Order not found.');", true);
                    return;
                }

                DataRow row = dt.Rows[0];
                lblWoNo.Text = row["WO_NO"].ToString();
                lblLoiNo.Text = row["LOI_NO"].ToString();
                lblEnqNo.Text = row["ENQ_NO"].ToString();
                lblPartyName.Text = row["PARTY_NAME"].ToString();
                lblWoDate.Text = Convert.ToDateTime(row["WO_DATE"]).ToString("dd-MMM-yyyy");
                lblWoStartDate.Text = Convert.ToDateTime(row["WO_START_DATE"]).ToString("dd-MMM-yyyy");
                lblWoEndDate.Text = Convert.ToDateTime(row["WO_END_DATE"]).ToString("dd-MMM-yyyy");
                lblWoDescription.Text = row["WO_DESCRIPTION"].ToString();
                lblContractValue.Text = "RM " + Convert.ToDecimal(row["CONTRACT_VALUE"]).ToString("N2");
                lblTaxApplicable.Text = row["TAX_APPLICABLE"].ToString() == "Y" ? "Yes" : "No";
                lblTaxAmount.Text = "RM " + Convert.ToDecimal(row["TAX_AMOUNT"]).ToString("N2");
                lblStatus.Text = row["STATUS"].ToString();
                lblDept.Text = row["DEPT_CODE"].ToString();
                lblCreatedBy.Text = row["CREATED_BY"].ToString();

                string status = row["STATUS"].ToString();
                if (status == "FINAL-APPROVED")
                {
                    pnlActions.Visible = false;
                    pnlAlreadyApproved.Visible = true;
                }
                else
                {
                    pnlActions.Visible = true;
                    pnlAlreadyApproved.Visible = false;
                }

                // Load items
                string sqlItems = @"SELECT wi.ITEM_CODE, NVL(ei.ITEM_DESCRIPTION,'') AS ITEM_DESCRIPTION,
                                           NVL(ei.UOM,'') AS UOM, wi.QTY, wi.UNIT_RATE
                                    FROM MATPASS.TBL_IWPS_WO_ITEMS wi
                                    LEFT JOIN MATPASS.TBL_IWPS_ENQ_ITEMS ei ON ei.ITEM_CODE = wi.ITEM_CODE AND ei.ENQ_NO = :p2
                                    WHERE wi.WO_NO = :p1 ORDER BY wi.ITEM_CODE";
                DataTable dtItems = DBHelper.ExecuteQuery(sqlItems,
                    DBHelper.P("p1", woNo),
                    DBHelper.P("p2", row["ENQ_NO"].ToString()));
                gvItems.DataSource = dtItems;
                gvItems.DataBind();

                // Load process logs
                string sqlLogs = @"SELECT ACTION, ACTION_BY, REMARKS, ACTION_DATE
                                   FROM MATPASS.TBL_IWPS_WO_PROCESS_LOGS
                                   WHERE WO_NO = :p1 ORDER BY ACTION_DATE";
                DataTable dtLogs = DBHelper.ExecuteQuery(sqlLogs, DBHelper.P("p1", woNo));
                gvLogs.DataSource = dtLogs;
                gvLogs.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading WO: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnFinalApprove_Click(object sender, EventArgs e)
        {
            string woNo = hfWoNo.Value;
            string staffNo = SessionHelper.StaffNo(Session);
            string remarks = txtRemarks.Text.Trim();

            try
            {
                DBHelper.ExecuteNonQuery(
                    "UPDATE MATPASS.TBL_IWPS_WO_HEADER SET STATUS='FINAL-APPROVED', UPDATED_BY=:p1, UPDATED_DATE=SYSTIMESTAMP WHERE WO_NO=:p2",
                    DBHelper.P("p1", staffNo), DBHelper.P("p2", woNo));

                DBHelper.ExecuteNonQuery(
                    "INSERT INTO MATPASS.TBL_IWPS_WO_PROCESS_LOGS (WO_NO, ACTION, REMARKS, ACTION_BY, ACTION_DATE) VALUES (:p1,'FINAL-APPROVED',:p2,:p3,SYSTIMESTAMP)",
                    DBHelper.P("p1", woNo), DBHelper.P("p2", remarks), DBHelper.P("p3", staffNo));

                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    $"toastr.success('Work Order {woNo} has been finally approved.');", true);

                pnlActions.Visible = false;
                pnlAlreadyApproved.Visible = true;
                lblStatus.Text = "FINAL-APPROVED";
                LoadWoDetails(woNo);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error approving WO: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }
    }
}
