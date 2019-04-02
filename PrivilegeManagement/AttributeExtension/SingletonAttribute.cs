using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.AttributeExtension
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DIUserAttribute : Attribute
    {
        public DIUserAttribute(int enumDIUserImport)
        {
            EnumDIUserImport = enumDIUserImport;
        }

        public int EnumDIUserImport { get; }
    }
}
