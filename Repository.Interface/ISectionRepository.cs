using DomainModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface ISectionRepository
    {
        void CreateSection(Section section);
        void EditSection(Section section);
        void DeleteSection(Section section);
        Section GetSectionById(int id);
        IEnumerable<Section> SectionList();
    }
}
