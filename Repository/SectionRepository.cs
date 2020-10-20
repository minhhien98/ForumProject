using DomainModel.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class SectionRepository : ISectionRepository
    {
        private DatabaseContext _dbContext;
        public SectionRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateSection(Section section)
        {
            _dbContext.Sections.Add(section);
            _dbContext.SaveChanges();
        }

        public void DeleteSection(Section section)
        {
            _dbContext.Sections.Remove(section);
            _dbContext.SaveChanges();
        }

        public void EditSection(Section section)
        {
            _dbContext.Entry(section).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public Section GetSectionById(int id)
        {
            return _dbContext.Sections.Find(id);
        }

        public IEnumerable<Section> SectionList()
        {
            return _dbContext.Sections.ToList();
        }
    }
}
