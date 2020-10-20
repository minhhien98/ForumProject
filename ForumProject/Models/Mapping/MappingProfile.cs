using AutoMapper;
using DomainModel.Entity;
using ForumProject.Models.ViewModel.Post;
using ForumProject.Models.ViewModel.Section;
using ForumProject.Models.ViewModel.Thread;
using ForumProject.Models.ViewModel.User;

namespace ForumProject.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRoleViewModel,UserRole>().ReverseMap();
            CreateMap<UserViewModel, User>().ForSourceMember(s=>s.UserRole,opt=>opt.DoNotValidate()).ForMember(d=>d.UserRole,opt=>opt.Ignore()).ReverseMap();
            CreateMap<RegisterViewModel, User>().ReverseMap();
            CreateMap<SectionViewModel, Section>().ReverseMap();
            CreateMap<ThreadViewModel, Thread>().ForSourceMember(t => t.Section, opt => opt.DoNotValidate()).ForMember(x => x.Section, opt => opt.Ignore()).ReverseMap();
            CreateMap<PostViewModel, Post>().ReverseMap();
        }
    }
}
