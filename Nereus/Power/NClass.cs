using System;
using System.Reflection;
using System.Collections.Generic;

namespace Nereus.Power
{
    public class NClass
    {
        public string Code
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public List<NMethod> Methods
        {
            get; set;
        }

        public static List<NClass> BuildNClass()
        {
            var nClasses = new List<NClass>();
            var allTypes = Assembly.GetEntryAssembly().GetTypes();

            foreach (var type in allTypes)
            {
                var nClassAttributes = type.GetCustomAttributes<NClassAttribute>();
                foreach (var nClassAttribute in nClassAttributes)
                {
                    var nClass = new NClass()
                    {
                        Code = nClassAttribute.Code,
                        Description = nClassAttribute.Descrption,
                        Name = nClassAttribute.Name,
                        Methods = new List<NMethod>()
                    };
                    nClass.Methods.AddRange(NMethod.BuildNMethod(type));
                    nClasses.Add(nClass);
                }
            }
            return nClasses;
        }
    }

    public class NClassAttribute : Attribute
    {
        public string Code
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Descrption
        {
            get; set;
        }
    }
}
