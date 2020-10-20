using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel.Entity;
using ForumProject.Models.ViewModel.Section;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace ForumProject.Controllers
{
    public class SectionController : Controller
    {
        private ISectionService _sectionService;
        private IMapper _mapper;
        public SectionController(ISectionService sectionService,IMapper mapper)
        {
            _sectionService = sectionService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var list = _sectionService.SectionList();
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(SectionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var section = _mapper.Map<Section>(vm);
                _sectionService.CreateSection(section);
                return RedirectToAction("List", "Section");
            }
            return View(vm);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            var list = _sectionService.SectionList();
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var section = _sectionService.GetSectionById(id);
            var vm = _mapper.Map<SectionViewModel>(section);
            return View(vm);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(SectionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var section = _mapper.Map<Section>(vm);
                _sectionService.EditSection(section);
                return RedirectToAction("List", "Section");
            }
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            var section = _sectionService.GetSectionById(id);
            return View(section);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Delete(SectionViewModel vm)
        {
            var section = _sectionService.GetSectionById(vm.Id);
            _sectionService.DeleteSection(section);
            return RedirectToAction("List", "Section");
        }
    }
}
