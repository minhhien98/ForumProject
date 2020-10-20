using DomainModel.Entity;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class SectionService : ISectionService
    {
        private ISectionRepository _sectionRepository;
        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public void CreateSection(Section section)
        {
            _sectionRepository.CreateSection(section);
        }

        public void DeleteSection(Section section)
        {
            _sectionRepository.DeleteSection(section);
        }

        public void EditSection(Section section)
        {
            _sectionRepository.EditSection(section);
        }

        public Section GetSectionById(int id)
        {
            return _sectionRepository.GetSectionById(id);
        }

        public IEnumerable<Section> SectionList()
        {
            return _sectionRepository.SectionList();
        }
    }
}
