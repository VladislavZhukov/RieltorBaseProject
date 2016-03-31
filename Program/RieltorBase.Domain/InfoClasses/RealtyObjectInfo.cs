namespace RieltorBase.Domain.InfoClasses
{
    using System.Collections.Generic;
    using System.Linq;

    using VI_EF;

    public class RealtyObjectInfo
    {
        internal RealtyObjectInfo()
        {
            
        }

        internal RealtyObjectInfo(RealtyObject realtyObject)
        {
            this.Id = realtyObject.RealtyObjectId;

            this.Type = realtyObject.RealtyObjectType.TypeName;

            this.Properties = realtyObject.PropertyValues.ToDictionary(
                value => value.PropertyType.PropertyName, value => value.StringValue);
        }

        public int Id { get;  set; }

        public string Type { get;  set; }

        public Dictionary<string, string> Properties { get;  set; }
    }
}
