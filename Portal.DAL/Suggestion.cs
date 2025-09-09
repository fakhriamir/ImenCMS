using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Tools
{
    public class Suggestion
    {
        public static void FillAllPeople(DropDownList MyDL, string UserID)
        {
            MyDL.Items.Clear();

            SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE (UnitChart.UnitID = " +ADAL.A_CheckData.GetUnitID() + ") ");

            while (MyRead.Read())
            {
                MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 2) + '-' + MyCL.MGStr(MyRead, 0), MyCL.MGInt(MyRead, 1).ToString()));
            }
            MyRead.Close(); MyRead.Dispose();
        }
        public static string GetSecName(int  SecID)
        {
            return DAL.ExecuteData.CNTDataStr("SELECT [Title] FROM  Sugg_Sec Where SecID =" + SecID);
        }
    }
}