using System;

namespace IWPS
{
    public partial class indent_items_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string indno = Request.QueryString["indno"];
                if (!string.IsNullOrEmpty(indno)) { txtIndNo.Text = indno; LoadData(indno); }
            }
        }

        void LoadData(string indno)
        {
            try
            {
                gvItems.DataSource = DBHelper.ExecuteQuery(@"SELECT ii.INDENT_NO,ii.ITEM_CODE,im.ITEM_NAME,ii.UOM,ii.QTY,ii.EST_UNIT_RATE
                    FROM MATPASS.TBL_IWPS_INDENT_ITEM ii
                    LEFT JOIN MATPASS.TBL_IWPS_ITEM_MASTER im ON im.ITEM_CODE=ii.ITEM_CODE
                    WHERE ii.INDENT_NO LIKE :i ORDER BY ii.ITEM_CODE",
                    DBHelper.P("i", "%" + indno + "%"));
                gvItems.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { LoadData(txtIndNo.Text.Trim()); }
    }
}
