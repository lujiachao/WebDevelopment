using System;
using System.Reflection;
using System.Collections.Generic;

namespace Nereus.Power
{
    public class NProperty
    {
        public string Name
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public List<NProperty> SubProperties
        {
            get; set;
        }

        public static List<NProperty> BuildNProperties(Type mainPropertyType)
        {
            if (mainPropertyType == null) {
                return new List<NProperty>();
            }
            var nActionProperties = mainPropertyType.GetProperties();
            var nProperties = new List<NProperty>();
            foreach (var property in nActionProperties)
            {
                var nPropertyAttribute = property.GetCustomAttribute<NPropertyAttribute>();
                if (nPropertyAttribute == null)
                {
                    continue;
                }
                var nProperty = new NProperty()
                {
                    Description = nPropertyAttribute.Description,
                    Name = property.Name,
                    Type = property.PropertyType.Name,
                };
                if (property.PropertyType.IsGenericType)
                {
                    nProperty.SubProperties = new List<NProperty>();
                    nProperty.SubProperties.AddRange(BuildNProperties(property.PropertyType.GetGenericArguments()[0]));
                }
                nProperties.Add(nProperty);
            }
            return nProperties;
        }
    }


    public class NPropertyAttribute : Attribute
    {
        public string Description
        {
            get; set;
        }

        public NPropertyAttribute(string description)
        {
            Description = description;
        }
    }
}
