using Dapper.Contrib.Extensions;

namespace PrivilegeManagement.Models
{
    [Table("User_Local")]
    public class UserLocal
    {
        [Key]
        public virtual string ID { get; set; }

        public virtual string Password { get; set; }

        public virtual string UserName { get; set; }

        public virtual string PickName { get; set; }

        public virtual string MobilePhone { get; set; }

        public virtual int Status {get; set;}
    }
}
