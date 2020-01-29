using System;

namespace PT.Model.CommonModelLib
{
    public class ModelAttribute : Attribute
    {
        public string TableName { get; set; }
        public bool HasAutoID { get; set; }

        public ModelAttribute(string TableName)
        {
            this.TableName = TableName;
            HasAutoID = false;
        }

        public ModelAttribute(string TableName, bool HasAutoID)
        {
            this.TableName = TableName;
            this.HasAutoID = HasAutoID;
        }

    }
}
