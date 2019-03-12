using AutoMapper;
using TestingSystem.Models;
using TestingSystem.ViewModels;

namespace TestingSystem.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<QuestionViewModel, Question>()
                .ForMember(g => g.Content, map => map.MapFrom(vm => vm.Content))
                .ForMember(g => g.Image, map => map.MapFrom(vm => vm.Image))
                .ForMember(g => g.Level, map => map.MapFrom(vm => vm.Level))
                .ForMember(g => g.CategoryID, map => map.MapFrom(vm => vm.CategoryID))
                .ForMember(g => g.IsActive, map => map.MapFrom(vm => vm.IsActive));

                
        }
    }
}