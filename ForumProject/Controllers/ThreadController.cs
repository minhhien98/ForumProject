using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel.Entity;
using ForumProject.Models.ViewModel.Section;
using ForumProject.Models.ViewModel.Thread;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Interface;

namespace ForumProject.Controllers
{
    public class ThreadController : Controller
    {
        private IMapper _mapper;
        private IThreadService _threadService;
        private ISectionService _sectionService;
        public ThreadController(IMapper mapper,IThreadService threadService,ISectionService sectionService)
        {
            _mapper = mapper;
            _threadService = threadService;
            _sectionService = sectionService;
        }
        public IActionResult Index(int id)
        {
            ViewBag.Section = _sectionService.GetSectionById(id).SectionName;
            var list = _threadService.ThreadList().Where(t => t.SectionId == id);
            return View(list);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            ThreadViewModel vm = new ThreadViewModel()
            {
                Section = _sectionService.SectionList().Select(t=> new SelectListItem() { Value = t.Id.ToString(),Text = t.SectionName}).ToList(),
            };
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Create(ThreadViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var thread = _mapper.Map<Thread>(vm);
                _threadService.CreateThread(thread);
                return RedirectToAction("List", "Thread");
            }
            vm.Section = _sectionService.SectionList().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.SectionName }).ToList();
            return View(vm);
        }
        public IActionResult List()
        {
            var list = _threadService.ThreadList();
            return View(list);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int id)
        {
            var thread = _threadService.GetThreadById(id);
            var vm = _mapper.Map<ThreadViewModel>(thread);
            //vm.SectionId = thread.SectionId;
            vm.Section = _sectionService.SectionList().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.SectionName }).ToList();
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Edit(ThreadViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var thread = _mapper.Map<Thread>(vm);
                _threadService.EditThread(thread);
                return RedirectToAction("List", "Thread");
            }
            vm.Section = _sectionService.SectionList().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.SectionName }).ToList();
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            var thread = _threadService.GetThreadById(id);
            var vm = _mapper.Map<ThreadViewModel>(thread);
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Delete(ThreadViewModel vm)
        {
            var thread = _threadService.GetThreadById(vm.Id);
            _threadService.DeleteThread(thread);
            return RedirectToAction("List", "Thread");
        }
    }
}
