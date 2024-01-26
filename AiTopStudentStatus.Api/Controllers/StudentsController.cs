using AiTopStudentStatus.Api.Cache;
using AiTopStudentStatus.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace AiTopStudentStatus.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly CacheTemp _cache;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
            _cache = new CacheTemp();
        }

        [HttpGet("GetAll")]
        public IEnumerable<Student> GetAll()
            => _cache.GetAll();

        [HttpGet("{classId}")]
        public IEnumerable<Student> Get(string classId)
            => _cache.GetValues(classId);

        [HttpGet("CheckForUpdate/{classId}")]
        public bool CheckForUpdate([FromRoute] string classId)
        {
            var re = Request;
            var headers = re.Headers;
            var listHash = headers["ListHash"];

            return _cache.CheckForUpdate(classId, listHash);
        }

        [HttpPost]
        public void Set(Student student)
            => _cache.SetObject(student);

        [HttpDelete("{studentId}")]
        public bool Delete(string studentId)
            => _cache.DeleteObject(studentId);

        [HttpDelete("DeleteAll")]
        public bool DeleteAll()
            => _cache.DeleteAll();    
    }
}