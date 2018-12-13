using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaea.SuperPower
{

    public enum GaeaPageSymbol
    {
        Equal,
        EqualGreater,
        EqualLess,
        Greater,
        Less,
        Like,
        LikeRight,
        LikeLeft,
        In,
    }

    public class GaeaPageAttribute : Attribute
    {
        public string Prefix { get; set; }

        public GaeaPageSymbol Symbol { get; set; }

        public string FieldName { get; set; }
    }
}
