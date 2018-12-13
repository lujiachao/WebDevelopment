using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Nereus.Power
{
    public class NMethod
    {
        public string ActionCode
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

        public string Method
        {
            get; set;
        }

        public string Uri
        {
            get; set;
        }

        public List<NProperty> ArugmentProperties
        {
            get; set;
        }

        public string ResultExplain
        {
            get; set;
        }

        public List<NProperty> ResultProperties
        {
            get; set;
        }

        public static List<NMethod> BuildNMethod(Type classType)
        {
            var classMethods = classType.GetMethods();
            var nMethods = new List<NMethod>();
            foreach (var method in classMethods)
            {
                var nMethodAttribute = method.GetNAttribute<NMethodAttribute>();
                if (nMethodAttribute == null)
                {
                    continue;
                }
                if (nMethodAttribute.Enable)
                {
                    if (string.IsNullOrWhiteSpace(nMethodAttribute.Uri))
                    {
                        var route = method.GetNAttribute<RouteAttribute>();
                        if (route != null)
                        {
                            nMethodAttribute.Uri = route.Template;
                        }
                    }

                    var nAction = new NMethod()
                    {
                        ActionCode = nMethodAttribute.Code,
                        ArugmentProperties = NProperty.BuildNProperties(nMethodAttribute.Argument),
                        Description = nMethodAttribute.Description,
                        Method = nMethodAttribute.Method,
                        Name = nMethodAttribute.Name,
                        ResultExplain = nMethodAttribute.ResultExplain,
                        ResultProperties = NProperty.BuildNProperties(nMethodAttribute.Result),
                        Uri = nMethodAttribute.Uri
                    };
                    nMethods.Add(nAction);
                }
            }
            return nMethods;
        }
    }

    public class NMethodAttribute : Attribute
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

        public string Method
        {
            get; set;
        }

        public bool Enable
        {
            get; set;
        }

        public string Uri
        {
            get; set;
        }

        public Type Argument
        {
            get; set;
        }

        public string ResultExplain
        {
            get; set;
        }

        public Type Result
        {
            get; set;
        }
    }
}
