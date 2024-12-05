namespace URIS2024_ExamRegistration.Models.Dto
{
    /// <summary>
    /// DTO klasa za kreiranje prijave ispita
    /// </summary>


    public class ExamRegistrationCreateDto
    {

        /// <summary>
        /// Identifikaciono obelezje studenta
        /// </summary>

        public Guid StudentId { get; set; }

        /// <summary>
        /// Identifikaciono obelezje predmeta
        /// </summary>

        public Guid SubjectId { get; set; }

        /// <summary>
        /// Datum i vreme ispita
        /// </summary>
        public DateTime ExamDate { get; set; }
    }
}
