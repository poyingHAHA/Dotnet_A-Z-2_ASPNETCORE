using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Person> GetAll()
        {
            Person p1 = new Person("Edwin", 18);
            Person p2 = new Person("John", 20);
            IEnumerable<Person> people = new Person[] { p1, p2 };
            return people;
        }

        [HttpGet]
        public Person GetPersonById(int id)
        {
            if(id == 1)
            {
                return new Person("Waku", 5);
            }
            else if(id == 2)
            {
                return new Person("Lora", 10);
            }
            return null;
        }

        [HttpPost]
        public string AddNew(Person p)
        {
            return "搞定";
        }
    }
}
