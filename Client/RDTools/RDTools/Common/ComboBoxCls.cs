using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Common
{
    public class ComboBoxCls
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public ComboBoxCls()
        { 
        
        }

        public ComboBoxCls(string _id,string _name)
        {
            ID = _id;
            Name = _name;
        }

        public ComboBoxCls(object[] _obj)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
