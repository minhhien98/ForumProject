using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModel.Entity
{
    public class Thread
    {
        public int Id { get; set; }
        [Display(Name ="Thread")]
        public string ThreadName { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
