using System;

namespace Gaea.SuperPower
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GaeaNameAttribute : Attribute, IGaeaName
    {
        public string Name
        {
            get; set;
        }

        public string Format
        {
            get; set;
        }

        public GaeaNamePattern Pattern
        {
            get; set;
        }


        public GaeaNameAttribute() : this(GaeaNamePattern.PascalChangeUnderline)
        {

        }

        public GaeaNameAttribute(GaeaNamePattern pattern)
        {
            Pattern = pattern;
        }

        public GaeaNameAttribute(string name)
        {
            Name = name;
            Pattern = GaeaNamePattern.NoFormat;
        }

        public GaeaNameAttribute(GaeaNamePattern pattern, string name, string format)
        {
            Pattern = pattern;
            Name = name;
            Format = format;
        }

        public string BuildName(Type type)
        {
            var name = string.Empty;
            switch (Pattern)
            {
                case GaeaNamePattern.StringFormat:
                    name = string.Format(Format, Name);
                    break;
                case GaeaNamePattern.TimeFormat:
                    name = $"{name}{DateTime.Now.ToString(Format)}";
                    break;
                case GaeaNamePattern.PascalChangeUnderline:
                    name = type.Name[0].ToString().ToLower();
                    for (int i = 1; i < type.Name.Length; i++)
                    {
                        if (char.IsUpper(type.Name[i]))
                        {
                            name += "_";
                        }
                        name += type.Name[i].ToString().ToLower();
                    }
                    break;
                default:
                    name = Name;
                    break;
            }
            return name;
        }
    }
}
