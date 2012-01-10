using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI.WebControls;
using Sporthub.Model;

namespace Sporthub.Utils
{
    public class Helpers
    {
        public static bool IsAdmin()
        {
            //if (Sporthub.Utilities.FacebookConnectAuthentication.isConnected())
            if (UserContext.UserIsAdmin())
            {
                return true;
            }
            return false;
        }

        public static void PopulateDropDownList(
            DropDownList list, 
            bool showUnselectedItem, 
            IList<ConfigData> listItems, 
            string unselectedText, 
            string selectedValue)
        {
            list.Items.Clear();

            if (showUnselectedItem)
            {
                ListItem li = new ListItem(string.Format("-- Select {0} --", unselectedText), "-1");
                list.Items.Add(li);
            }
            if (listItems != null)
            {
                foreach (var item in listItems)
                {
                    ListItem li = new ListItem(item.Text, item.Value);
                    list.Items.Add(li);
                }
            }
            if (showUnselectedItem)
            {
                list.SelectedIndex = 0;
            }
            else
            {
                list.SelectedValue = selectedValue;
            }
        }
    }
}
