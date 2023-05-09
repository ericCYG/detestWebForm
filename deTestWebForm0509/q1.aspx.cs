using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace deTestWebForm0509
{
    public partial class q1 : System.Web.UI.Page
    {
        string getFrontString;
        List<int> ListNeedSort = new List<int>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
     
            
        }

        protected void sortStart(object sender, EventArgs e)
        {

            getFrontString = resourceStringTextBox.Text.Replace(" ", "");

            ListNeedSort = getFrontString.Split(',').ToList().ConvertAll(int.Parse);

            for (int i = 0; i < ListNeedSort.Count-1; i++)
            {
                for (int j = i+1; j < ListNeedSort.Count; j++)
                {
                    if (ListNeedSort[i] > ListNeedSort[j])
                    {
                        ListNeedSort[i] = ListNeedSort[i] ^ ListNeedSort[j];
                        ListNeedSort[j] = ListNeedSort[i] ^ ListNeedSort[j];
                        ListNeedSort[i] = ListNeedSort[i] ^ ListNeedSort[j];
                    }
                }
            }
            getFrontString = "";
            foreach (int item in ListNeedSort)
            {
                getFrontString += item + ",";
            }
            sortAns.Text = getFrontString.Remove(getFrontString.Length - 1, 1);



        }
    }
}