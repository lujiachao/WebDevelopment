using Gaea;
using Gaea.SuperPower;
using System;

namespace WD.SQL.DTOs
{
    [GaeaName("authentication")]
    public class Authentication : GaeaSon
    {
        public int Id_Admin
        {
            get; set;
        }

        public int Id_Merchant
        {
            get; set;
        }

        public DateTime Time_Create
        {
            get; set;
        }

        public DateTime Time_Invalid
        {
            get; set;
        }

        public string Token
        {
            get; set;
        }
    }
}
