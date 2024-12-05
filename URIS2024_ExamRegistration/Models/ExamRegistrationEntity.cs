namespace URIS2024_ExamRegistration.Models
{
    public class ExamRegistrationEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
