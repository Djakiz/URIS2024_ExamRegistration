using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using URIS2024_ExamRegistration.Data;
using URIS2024_ExamRegistration.Models;
using URIS2024_ExamRegistration.Models.Dto;

namespace URIS2024_ExamRegistration.Controllers
{
    [ApiController]
    [Produces("application/json")] // kao Content-type u Postmanu
    [Route("api/[controller]")]
    public class ExamRegistrationController : Controller
    {
        private readonly IExamRegistrationRepository _examRegistrationRepository;
        private readonly IMapper _mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ExamRegistrationController(IExamRegistrationRepository examRegistrationRepository, IMapper mapper)
        {
            _examRegistrationRepository = examRegistrationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Vraca sve prijave ispita
        /// </summary>
        /// <returns>Lista svih prijavljenih ispita</returns>
        /// <response code="200">Vraca listu prijavljenih ispita</response>
        /// <response code="204">Nije pronadjena nijedna prijava ispita.</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<ExamRegistrationDto>> GetExamRegistrations()
        {
            var examRegistrations = _examRegistrationRepository.GetExamRegistrations();
            if (examRegistrations == null || examRegistrations.Count() == 0)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<List<ExamRegistrationDto>>(examRegistrations)); // vraca se lista ExamRegistrationEntity, ovo je potrebno da mapiramo u listu ExamRegistrationDto
        }

        [HttpGet("{examRegistrationId}")]
        public ActionResult<ExamRegistrationDto> GetExamRegistration(Guid examRegistrationId)
        {
            ExamRegistrationEntity examRegistrationModel = _examRegistrationRepository.GetExamRegistrationById(examRegistrationId);
            if (examRegistrationModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ExamRegistrationDto>(examRegistrationModel));
        }

        // ako hocemo enter kucamo \, ovo radi kao konzola (remarks)

        /// <summary>
        /// Kreira novu prijavu ispita
        /// </summary>
        /// <param name="examRegistration">Model za kreiranje prijave ispita</param>
        /// <returns>Potvrda uspesno kreirane prijave ispita</returns>
        /// <response code="201">Vraca potvrdu o uspesno kreiranoj prijavi ispita</response>
        /// <response code="500">Doslo je do greske na serveru prilikom procesiranja zahteva za kreiranje prijave ispita</response>
        /// <remarks>
        /// Primer zahteva za kreiranje prijave ispita: \
        /// { \
        ///     "StudentId" : "044f3de0-a9dd-4c2e-b745-89976a1b2a36",\
        ///     "SubjectId" : "21ad52f8-0281-4241-98b0-481566d25e4f, \
        ///     "ExamDate" : "2024-12-15T09:00:00" \
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        public ActionResult<ExamRegistrationConfirmationDto> CreateExamRegistration(
            [FromBody] ExamRegistrationCreateDto examRegistration )
        {
            try
            {
                ExamRegistrationConfirmationDto confirmation = _examRegistrationRepository.CreateExamRegistration(_mapper.Map<ExamRegistrationEntity>(examRegistration));
                
                return StatusCode(StatusCodes.Status201Created, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{examRegistrationId}")]
        public IActionResult DeleteExamRegistration(Guid examRegistrationId)
        {
            try
            {
                ExamRegistrationEntity examRegistrationModel = _examRegistrationRepository.GetExamRegistrationById(examRegistrationId);
                if (examRegistrationModel == null)
                {
                    return NotFound();
                }
                _examRegistrationRepository.DeleteExamRegistration(examRegistrationId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
