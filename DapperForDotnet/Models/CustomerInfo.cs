using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperForDotnet.Models
{
    [Table("T_Customer")]
    public class CustomerInfo
    {
        //Table：指定实体对应地数据库表名，如果类名和数据库表名不同，需要设置（如案例所示）
        //Key：指定此列为自动增长主键
        //ExplicitKey：指定此列为非自动增长主键（例如guid，字符串列）
        //Computed：计算属性，此列不作为更新
        //Write：指定列是否可写
        [ExplicitKey] //非自增长的用此标识
        public virtual string ID { get; set; }
        public virtual string Name { get; set; }

        public virtual int Age { get; set; }

        public virtual string Creator { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}
