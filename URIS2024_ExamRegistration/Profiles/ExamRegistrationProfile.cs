using AutoMapper;
using URIS2024_ExamRegistration.Models;
using URIS2024_ExamRegistration.Models.Dto;

namespace URIS2024_ExamRegistration.Profiles
{
    public class ExamRegistrationProfile : Profile
    {
        public ExamRegistrationProfile() 
        {
            CreateMap<ExamRegistrationEntity, ExamRegistrationDto>()
             //   .ForMember() ako se obelezja ne poklapaju, unutar zagrade ide lambda izraz
                .ReverseMap();

            CreateMap<ExamRegistrationEntity,ExamRegistrationCreateDto>()
                .ReverseMap();

            CreateMap<ExamRegistrationEntity,ExamRegistrationUpdateDto>()
                .ReverseMap();
        }
    }
}
