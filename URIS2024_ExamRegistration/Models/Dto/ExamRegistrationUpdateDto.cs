namespace URIS2024_ExamRegistration.Models.Dto
{
    public class ExamRegistrationUpdateDto
    {
        public Guid Id { get; set; } // on je neophodan u update-u
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
