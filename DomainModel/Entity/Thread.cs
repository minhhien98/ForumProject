using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Entity
{
    public class Thread
    {
        public int Id { get; set; }
        public string ThreadName { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}
