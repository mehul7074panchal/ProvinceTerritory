using System;

namespace PT.Model.CommonModelLib
{
    public class ModelPropertyAttribute : Attribute
    {
        public string ParameterName { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsNullableField { get; set; }
        public bool IsInsertParameter { get; set; }
        public bool IsUpdateParameter { get; set; }
        public bool IsDeleteParameter { get; set; }
        public bool IsSelectParameter { get; set; }

        public ModelPropertyAttribute(string ParameterName)
        {
            this.ParameterName = ParameterName;
            this.IsIdentity = false;
            this.IsNullableField = false;
            this.IsInsertParameter = false;
            this.IsUpdateParameter = false;
            this.IsSelectParameter = false;
        }

        public ModelPropertyAttribute(string ParameterName, string OtherProperties) //"identity-insert-update-select"
        {
            this.ParameterName = ParameterName;
            if (OtherProperties.ToUpper().Contains("IDENTITY"))
            {
                this.IsIdentity = true;
            }
            if (OtherProperties.ToUpper().Contains("INSERT") && OtherProperties.ToUpper().Contains("NULLABLE"))
            {
                this.IsNullableField = true;
            }
            if (OtherProperties.ToUpper().Contains("INSERT"))
            {
                this.IsInsertParameter = true;
            }
            if (OtherProperties.ToUpper().Contains("UPDATE"))
            {
                this.IsUpdateParameter = true;
            }
            if (OtherProperties.ToUpper().Contains("DELETE"))
            {
                this.IsDeleteParameter = true;
            }
            if (OtherProperties.ToUpper().Contains("SELECT"))
            {
                this.IsSelectParameter = true;
            }
        }

        public ModelPropertyAttribute(string ParameterName, bool IsIdentity, bool IsNullableField, bool IsInsertParameter, bool IsUpdateParameter, bool IsSelectParameter)
        {
            this.ParameterName = ParameterName;
            this.IsIdentity = IsIdentity;
            this.IsNullableField = IsNullableField;
            this.IsInsertParameter = IsInsertParameter;
            this.IsUpdateParameter = IsUpdateParameter;
            this.IsSelectParameter = IsSelectParameter;
        }
    }
}
