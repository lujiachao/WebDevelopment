using System.Collections.Generic;

namespace Gaea.SuperPower
{
    public interface IGaeaPage
    {
        int PageIndex { get; }

        int PageSize { get; }

        string BuildSelect();

        string BuildForm();

        string BuildWhere();

        string BuildSort();

        IEnumerable<IGaeaPage> Page(int pageIndex, int pageSize);
    }
}
