using System;
using System.Collections.Generic;

#nullable disable

namespace TestCrud.Models.DBModels
{
    public partial class TbSkillType
    {
        public TbSkillType()
        {
            TbUsers = new HashSet<TbUser>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Display { get; set; }
        public bool IsArchive { get; set; }

        public virtual ICollection<TbUser> TbUsers { get; set; }
    }
}
