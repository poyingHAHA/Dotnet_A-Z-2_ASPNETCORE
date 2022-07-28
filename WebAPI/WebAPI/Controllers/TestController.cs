using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet]
        public Person GetPerson()
        {
            return new Person("Edwin", 18);
        }

        [HttpPost]
        public string[] SaveNote(SaveNoteRequest req)
        {
            System.IO.File.WriteAllText(req.Title+".txt", req.Content);
            return new string[] {"OK, ", req.Title};
        }
    }
}
