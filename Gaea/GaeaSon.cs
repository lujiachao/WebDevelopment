using Dapper.Contrib.Extensions;
using Gaea.SuperPower;

namespace Gaea
{
    public abstract class GaeaSon : IGaeaSon
    {
        [Key]
        public int Id
        {
            get; set;
        }

        public string CallName()
        {
            return GaeaSuperPower.BuildName(this.GetType());
        }

        public static string CallName<T>(bool doSymbol = true) where T : IGaeaSon
        {
            return GaeaSuperPower.BuildName(typeof(T), doSymbol);
        }
    }
}
