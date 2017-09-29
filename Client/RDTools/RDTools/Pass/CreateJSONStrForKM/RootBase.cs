using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Pass.CreateJSONStrForKM
{
    public class RootBase
    {
        public List<DrugBase> DrugBaseLst = null;
        public List<ItemBase> ItemBaseLst = null;
        public List<CMDrugBase> CMDrugBaseLst = null;
        public PreBase _PreBase { get; set; }

        public RootBase()
        {
            DrugBaseLst = new List<DrugBase>();
            ItemBaseLst = new List<ItemBase>();
            CMDrugBaseLst = new List<CMDrugBase>();
        }
        public string ConvertFunction()
        {
            string drugs = string.Empty;
            foreach (DrugBase db in DrugBaseLst)
            {
                drugs += "{";
                drugs += db.ConvertFunction();
                drugs += "},";
            }
            if (drugs.Length > 0)
                drugs = drugs.Substring(0, drugs.Length - 1);
            string str_PreBase = _PreBase.ConvertFunction();
            return "{" + str_PreBase + "\"drugs\":[" + drugs + "]}";
        }
    }
}
