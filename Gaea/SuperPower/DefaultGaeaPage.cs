using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaea.SuperPower
{
    public abstract class DefaultGaeaPage<T> : IGaeaPage where T : class
    {
        protected static ConcurrentDictionary<RuntimeTypeHandle, PropertyInfo[]> ConcurrentProperties = new ConcurrentDictionary<RuntimeTypeHandle, PropertyInfo[]>();
        protected static ConcurrentDictionary<RuntimeTypeHandle, GaeaPageAttribute> ConcurrentNoticeProperties = new ConcurrentDictionary<RuntimeTypeHandle, GaeaPageAttribute>();
        protected int _pageIndex;
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
        }

        protected int _pageSize;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
        }

        public DefaultGaeaPage()
        {

        }

        public DefaultGaeaPage(int pageIndex, int pageSize)
        {
            _pageIndex = pageIndex;
            _pageSize = pageSize;
        }

        public abstract string BuildForm();
        public abstract string BuildSelect();
        public abstract string BuildSort();
        public abstract string BuildPage();

        public virtual string BuildWhere()
        {
            PropertyInfo[] propertyInfos;
            if (ConcurrentProperties.TryGetValue(typeof(T).TypeHandle, out propertyInfos))
            {

            }
            return string.Empty;
        }

        protected virtual PropertyInfo[] FastReflectionProperties()
        {
            return null;
        }

        public virtual IEnumerable<IGaeaPage> Page(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
