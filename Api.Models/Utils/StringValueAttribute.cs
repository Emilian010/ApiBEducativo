namespace Api.Models.Utils
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class StringValueAttribute : Attribute
    {
        public string StringValue { get; set; }

        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
    }
}
