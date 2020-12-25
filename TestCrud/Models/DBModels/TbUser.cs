using System;
using System.Collections.Generic;

#nullable disable

namespace TestCrud.Models.DBModels
{
    public partial class TbUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public int? SkillId { get; set; }

        public virtual TbSkillType Skill { get; set; }
    }
}
