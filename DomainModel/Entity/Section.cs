using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModel.Entity
{
    public class Section
    {
        public int Id { get; set; }
        [Display(Name ="Section")]
        public string SectionName { get; set; }

        public ICollection<Thread> Threads { get; set; }
    }
}
