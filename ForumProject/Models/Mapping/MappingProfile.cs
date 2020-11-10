using AutoMapper;
using DomainModel.Entity;
using ForumProject.Models.ViewModel.Post;
using ForumProject.Models.ViewModel.Section;
using ForumProject.Models.ViewModel.Thread;
using ForumProject.Models.ViewModel.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForumProject.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRoleViewModel,UserRole>().ReverseMap();
            CreateMap<UserViewModel, User>().ForSourceMember(vm=>vm.UserRole,opt=>opt.DoNotValidate()).ForMember(u=>u.UserRole,opt=>opt.Ignore()).ReverseMap();
            CreateMap<UserRole, SelectListItem>().ForMember(dest=>dest.Value,opt=>opt.MapFrom(u=>u.Id)).ForMember(dest=>dest.Text,opt=>opt.MapFrom(u=>u.RoleName)).ReverseMap();
            //CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<RegisterViewModel, User>().ReverseMap();
            CreateMap<SectionViewModel, Section>().ReverseMap();
            CreateMap<ThreadViewModel, Thread>().ForSourceMember(t => t.Section, opt => opt.DoNotValidate()).ForMember(x => x.Section, opt => opt.Ignore()).ReverseMap();
            CreateMap<PostViewModel, Post>().ReverseMap();
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T, TTextProperty, TValueProperty>(this IEnumerable<T> instance, Func<T, TTextProperty> text, Func<T, TValueProperty> value, Func<T, bool> selectedItem = null)
        {
            return instance.Select(t => new SelectListItem
            {
                Text = Convert.ToString(text(t)),
                Value = Convert.ToString(value(t)),
                Selected = selectedItem != null ? selectedItem(t) : false
            });
        }
    }
}
