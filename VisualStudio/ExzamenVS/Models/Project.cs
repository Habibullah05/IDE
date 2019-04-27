using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ExzamenVS.Models
{
    [Serializable]
     public class Project
    {
        // [XmlAttribute(AttributeName = "Name")]
      
        public string Name { get; set; }
        // [XmlAttribute(AttributeName="path")]
       
        public string Path { get; set; }
        //[XmlElement("Name")]
        public List<CS> csfile { get; set; } = new List<CS>();

    }
  

    public class CS
    {
        public string Name { get; set; }
        public string Path { get; set;}
        [NonSerialized]
        public string Text;
    }
}
