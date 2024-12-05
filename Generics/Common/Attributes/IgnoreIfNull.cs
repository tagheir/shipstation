using System;

namespace Generics.Common.Attributes
{
    public class IgnoreIfNull : Attribute
    {
    }
    public class Ignore : Attribute
    {
    }
    public class IgnoreOnUpdate : Attribute
    {
    }
    public class IgnoreView : Attribute
    {

    }
    public class Hidden : Attribute
    {

    }
    public class Picture : Attribute
    {

    }
    public class DbGenerated : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnName : Attribute
    {
        string value;

        public ColumnName(string value)
        {
            this.value = value;
        }

        public string Value
        {
            get { return value; }
        }
    }
}
