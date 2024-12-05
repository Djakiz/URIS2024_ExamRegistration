using URIS2024_ExamRegistration.Models;
using URIS2024_ExamRegistration.Models.Dto;

namespace URIS2024_ExamRegistration.Data
{
    public class ExamRegistrationRepository : IExamRegistrationRepository
    {
        private List<ExamRegistrationEntity> ExamRegistrations { get; set; }
        private List<StudentEntity> Students { get; set; }
        private List<SubjectEntity> Subjects { get; set; }
        public ExamRegistrationRepository()
        {
            ExamRegistrations = new List<ExamRegistrationEntity>();
            Students = new List<StudentEntity>();
            Subjects = new List<SubjectEntity>();

            FillData();
        }

        private void FillData()
        {
            ExamRegistrations.AddRange(new List<ExamRegistrationEntity>
            {
                new ExamRegistrationEntity
                {
                    Id = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    StudentId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    SubjectId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    ExamDate = DateTime.Parse("2024-12-15T09:00:00")
                },
                new ExamRegistrationEntity
                {
                    Id = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    StudentId = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    SubjectId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    ExamDate = DateTime.Parse("2024-12-15T09:00:00")
                },
            });

            Students.AddRange(new List<StudentEntity>
            {
                new StudentEntity
                {
                    Id = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    Index = "IT 1/2022",
                    Year = 2
                },
                new StudentEntity
                {
                    Id = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    FirstName = "Marko",
                    LastName = "Markovic",
                    Index = "IT 1/2021",
                    Year = 3
                },
            });

            Subjects.AddRange(new List<SubjectEntity>
            {
                new SubjectEntity
                {
                    Id = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    Name = "Upravljanje razvojem informacionih sistema",
                    ECTS = 6
                },
                new SubjectEntity
                {
                    Id = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    Name = "Projektovanje skladista podataka",
                    ECTS = 6
                },
            });
        }
        public ExamRegistrationConfirmationDto CreateExamRegistration(ExamRegistrationEntity examRegistration)
        {
            examRegistration.Id = Guid.NewGuid();
            ExamRegistrations.Add(examRegistration);

            StudentEntity student = Students.FirstOrDefault(s => s.Id == examRegistration.StudentId)!;
            SubjectEntity subject = Subjects.FirstOrDefault(s => s.Id == examRegistration.SubjectId)!;

            return new ExamRegistrationConfirmationDto
            {
                StudentName = $"{student.FirstName} {student.LastName}",
                SubjectName = subject.Name,
                ExamDate = examRegistration.ExamDate
            };
        }

        public void DeleteExamRegistration(Guid id)
        {
            ExamRegistrations.Remove(GetExamRegistrationById(id));
        }

        public ExamRegistrationEntity GetExamRegistrationById(Guid id)
        {
            return ExamRegistrations.FirstOrDefault(e => e.Id == id)!;
        }

        public List<ExamRegistrationEntity> GetExamRegistrations()
        {
            return ExamRegistrations;
        }

        public ExamRegistrationConfirmationDto UpdateExamRegistration(ExamRegistrationEntity examRegistration)
        {
            ExamRegistrationEntity oldExamRegistration = GetExamRegistrationById(examRegistration.Id);
            oldExamRegistration.StudentId = examRegistration.StudentId;
            oldExamRegistration.SubjectId = examRegistration.SubjectId;
            oldExamRegistration.ExamDate = examRegistration.ExamDate;

            StudentEntity student = Students.FirstOrDefault(s => s.Id == examRegistration.StudentId)!;
            SubjectEntity subject = Subjects.FirstOrDefault(s => s.Id == examRegistration.SubjectId)!;

            return new ExamRegistrationConfirmationDto
            {
                StudentName = $"{student.FirstName} {student.LastName}",
                SubjectName = subject.Name,
                ExamDate = oldExamRegistration.ExamDate
            };
        }
    }
}
