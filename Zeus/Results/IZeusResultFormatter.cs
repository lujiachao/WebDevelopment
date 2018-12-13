using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeus.Results
{
    public interface IZeusResultFormatter
    {
        object Formatter(ActionContext actionContext, object data);
    }
}
