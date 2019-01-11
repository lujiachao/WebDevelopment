using MiddleWareStudy.Common.Enum;
using System;

namespace MiddleWareStudy.Middleware
{
    public class PrivilegeException : Exception
    {
        public EnumPrivilegeException EnumiTPSEx
        {
            get;
        }

        private int _code;

        public int Code
        {
            get
            {
                if (_code > 0)
                {
                    return _code;
                }
                return (int)EnumiTPSEx;
            }
        }

        public PrivilegeException(int Code, string message) : base(message)
        {
            _code = Code;
        }

        public PrivilegeException(EnumPrivilegeException enumiTPSEx) : base(enumiTPSEx.ToString())
        {
            EnumiTPSEx = enumiTPSEx;
        }

        public PrivilegeException(EnumPrivilegeException enumiTPSEx, string message) : base(message)
        {
            EnumiTPSEx = enumiTPSEx;
        }

        public PrivilegeException(EnumPrivilegeException enumiTPSEx, Exception ex) : base(enumiTPSEx.ToString(), ex)
        {
            EnumiTPSEx = enumiTPSEx;
        }

        public PrivilegeException(EnumPrivilegeException enumiTPSEx, string message, Exception ex) : base(message, ex)
        {
            EnumiTPSEx = enumiTPSEx;
        }

        public override string ToString()
        {
            return $"[Code={Code}][Message={Message}][InnerMessage={(InnerException == null ? string.Empty : InnerException.Message)}]";
        }
    }
}
