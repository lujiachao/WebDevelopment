using System;
using System.Linq;

using Dapper;
using Dapper.Contrib.Extensions;
namespace Gaea.SuperPower
{
    public static class GaeaSuperPower
    {
        private static char _leftSymbol;

        private static char _rightSymbol;

        public static void InitNameSymbol(string symbol)
        {
            if (symbol.Length == 1)
            {
                _leftSymbol = _rightSymbol = symbol[0];
            }
            else
            {
                _leftSymbol = symbol[0];
                _rightSymbol = symbol[1];
            }
        }

        public static void EnableGaeaSuperName()
        {
            SqlMapperExtensions.TableNameMapper = type =>
            {
                return BuildName(type);
            };
        }

        public static void MatchNamesWithUnderscores(bool result)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = result;
        }

        public static string BuildName(Type type, bool doSymbol = true)
        {
            var nameAttribute = type.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "GaeaNameAttribute");
            if (nameAttribute != null && nameAttribute is IGaeaName)
            {
                var eaeaName = nameAttribute as IGaeaName;
                return eaeaName.BuildName(type);
            }
            var name = type.Name;
            if (type.IsInterface && name.StartsWith("I"))
                return name.Substring(1);
            if (doSymbol)
            {
                return _leftSymbol + name + _rightSymbol;
            }
            return name;
        }
    }
}
