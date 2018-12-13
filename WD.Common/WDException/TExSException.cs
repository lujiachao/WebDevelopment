using System;
using System.Collections.Generic;
using WD.Common.WDEnum;

namespace WD.Common.WDException
{
    public class TExSException : Exception
    {
        public EnumErrorStatus EnumTExSEx
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
                return (int)EnumTExSEx;
            }
        }

        public TExSException(int Code, string message) : base(message)
        {
            _code = Code;
        }

        public TExSException(EnumErrorStatus enumTExSEx) : base(enumTExSEx.ToString())
        {
            EnumTExSEx = enumTExSEx;
        }

        public TExSException(EnumErrorStatus enumTExSEx, string message) : base(message)
        {
            EnumTExSEx = enumTExSEx;
        }

        public TExSException(EnumErrorStatus enumTExSEx, Exception ex) : base(enumTExSEx.ToString(), ex)
        {
            EnumTExSEx = enumTExSEx;
        }

        public TExSException(EnumErrorStatus enumTExSEx, string message, Exception ex) : base(message, ex)
        {
            EnumTExSEx = enumTExSEx;
        }

        public override string ToString()
        {
            return $"[Code={Code}][Message={Message}][InnerMessage={(InnerException == null ? string.Empty : InnerException.Message)}]";
        }
    }
}
