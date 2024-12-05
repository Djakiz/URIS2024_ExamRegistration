using URIS2024_ExamRegistration.Models;
using URIS2024_ExamRegistration.Models.Dto;

namespace URIS2024_ExamRegistration.Data
{
    public interface IExamRegistrationRepository
    {
        List<ExamRegistrationEntity> GetExamRegistrations();
        ExamRegistrationEntity GetExamRegistrationById(Guid id);
        ExamRegistrationConfirmationDto CreateExamRegistration(ExamRegistrationEntity examRegistration);
        ExamRegistrationConfirmationDto UpdateExamRegistration(ExamRegistrationEntity examRegistration);
        void DeleteExamRegistration(Guid id);
    }
}
