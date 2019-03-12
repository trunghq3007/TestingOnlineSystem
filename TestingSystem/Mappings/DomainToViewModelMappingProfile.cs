using AutoMapper;
using TestingSystem.Models;
using TestingSystem.ViewModels;

namespace TestingSystem.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Question, QuestionViewModel>();
        }
    }
}