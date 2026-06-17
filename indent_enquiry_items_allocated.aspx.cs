using System;

namespace IWPS
{
    public partial class indent_enquiry_items_allocated : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) LoadData(""); }

        void LoadData(string indno)
        {
            try
            {
                string sql = @"SELECT ii.INDENT_NO,ii.ITEM_CODE,ii.QTY IND_QTY,
                    ei.ENQ_NO,ei.QTY ENQ_QTY,eh.STATUS ENQ_STATUS
                    FROM MATPASS.TBL_IWPS_INDENT_ITEM ii
                    LEFT JOIN MATPASS.TBL_IWPS_ENQUIRY_ITEMS ei ON ei.ITEM_CODE=ii.ITEM_CODE AND ei.INDENT_NO=ii.INDENT_NO
                    LEFT JOIN MATPASS.TBL_IWPS_ENQUIRY_HEADER eh ON eh.ENQ_NO=ei.ENQ_NO
                    WHERE 1=1";
                var prms = new System.Collections.Generic.List<Oracle.ManagedDataAccess.Client.OracleParameter>();
                if (!string.IsNullOrEmpty(indno)) { sql += " AND ii.INDENT_NO LIKE :i"; prms.Add(DBHelper.P("i", "%" + indno + "%")); }
                sql += " ORDER BY ii.INDENT_NO,ii.ITEM_CODE";
                gvData.DataSource = DBHelper.ExecuteQuery(sql, prms.ToArray());
                gvData.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { LoadData(txtIndNo.Text.Trim()); }
    }
}
