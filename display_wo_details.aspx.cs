using System;
using System.Data;

namespace IWPS
{
    public partial class display_wo_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string wono = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(wono)) Load(wono);
            }
        }

        void Load(string wono)
        {
            try
            {
                DataTable dt = DBHelper.ExecuteQuery("SELECT * FROM MATPASS.TBL_IWPS_WO_HEADER WHERE WO_NO=:w", DBHelper.P("w", wono));
                if (dt.Rows.Count > 0)
                {
                    var r = dt.Rows[0];
                    lblWONo.Text = r["WO_NO"].ToString();
                    lblEnqNo.Text = r["ENQ_NO"].ToString();
                    lblLoiNo.Text = r["LOI_NO"].ToString();
                    lblStatus.Text = r["STATUS"].ToString();
                    lblCV.Text = string.Format("{0:N2}", r["CONTRACT_VALUE"]);
                    lblDate.Text = Convert.ToDateTime(r["WO_DATE"]).ToString("dd-MMM-yyyy");
                    lblStart.Text = r["WO_START_DATE"] == DBNull.Value ? "" : Convert.ToDateTime(r["WO_START_DATE"]).ToString("dd-MMM-yyyy");
                    lblEnd.Text = r["WO_END_DATE"] == DBNull.Value ? "" : Convert.ToDateTime(r["WO_END_DATE"]).ToString("dd-MMM-yyyy");
                    lblDesc.Text = r["WO_DESCRIPTION"].ToString();
                }
                gvItems.DataSource = DBHelper.ExecuteQuery("SELECT ITEM_CODE,DESCRIPTION,UOM,QTY,UNIT_RATE,AMOUNT FROM MATPASS.TBL_IWPS_WO_ITEMS WHERE WO_NO=:w", DBHelper.P("w", wono));
                gvItems.DataBind();
                gvLog.DataSource = DBHelper.ExecuteQuery("SELECT ACTION,REMARKS,ACTIONED_BY,ACTION_DATE FROM MATPASS.TBL_IWPS_WO_PROCESS_LOGS WHERE WO_NO=:w ORDER BY ACTION_DATE", DBHelper.P("w", wono));
                gvLog.DataBind();
            }
            catch { }
        }
    }
}
