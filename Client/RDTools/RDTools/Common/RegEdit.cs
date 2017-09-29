using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace RDTools.Common
{
    public class RegEdit
    {
        public static void RegisterOver(string Name,object Value)
        {
            object DefVal;
            DefVal = 0;
            try
            {
                RegistryKey Key = Registry.CurrentUser.CreateSubKey("Software\\Rdsafe");
                Key.SetValue(Name, Value);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public static string Query(string Name)
        {
            object DefVal = null;
            try
            {
                RegistryKey Key = Registry.CurrentUser.CreateSubKey("Software\\Rdsafe");
                DefVal = Key.GetValue(Name, DefVal);
                return DefVal.ToString();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
