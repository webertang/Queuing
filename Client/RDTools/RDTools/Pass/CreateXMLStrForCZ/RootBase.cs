using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Pass.CreateXML
{
    public class RootBase
    {
        public List<ICDBase> ICDBaseLst = null;
        public List<DrugBase> DrugBaseLst = null;
        public PreBase _PreBase { get; set; }

        public RootBase()
        {
            ICDBaseLst = new List<ICDBase>();
            DrugBaseLst = new List<DrugBase>();
        }

        public string ConvertFunction()
        {
            string ICD = "<ICD>";
            foreach (ICDBase _ICDBase in ICDBaseLst)
            {
                ICD += _ICDBase.ConvertFunction();
            }
            ICD += "</ICD>";

            string Drug = "<Drug>";
            foreach (DrugBase _DrugBase in DrugBaseLst)
            {
                Drug += _DrugBase.ConvertFunction();
            }
            Drug += "</Drug>";

            return string.Format("<root>" + _PreBase.ConvertFunction() + ICD + Drug + "</root>");
        }
    }
}
