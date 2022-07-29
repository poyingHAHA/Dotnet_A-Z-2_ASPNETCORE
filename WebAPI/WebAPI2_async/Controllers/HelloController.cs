using Microsoft.AspNetCore.Mvc;

namespace WebAPI2_async.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public int Add(int i, int j)
        {
            return i + j;
        }

        [HttpGet]
        public async Task<string> Add2()
        {
            string s = await System.IO.File.ReadAllTextAsync(@"E:\Dotnet_a-z\Dotnet_A-Z-2_ASPNETCOR\WebAPI\WebAPI2_async\test.txt");
            return s.Substring(0, 20);
        }

        [HttpGet]
        public Person Test2()
        {
            return new Person {
                Id = 5,
                Name = "edwin",
                Children = new Child[] { new Child { Name = "aaa" }, new Child { Name = "bbb" } }
            };
        }

        [HttpGet("{i}/{j}")]
        public int Test3(int i, int j)
        {
            return i + j;
        }

        [HttpGet("{i}/{j}")]
        public int Test4([FromRoute(Name ="i")] int x, int j)
        {
            return x * j;
        }

        [HttpPost]
        public string AddPerson(Person p)
        {
            return "Saved " + p.Id + ". " + p.Name;
        }

        [HttpPut("{id}")]
        public string UpdatePerson([FromRoute] int id, Person p)
        {
            return "update " + id + " success " + p.Name;
        }
    }
    public class Child
    {
        public string Name { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public Child[] Children { get; set; }
    }

}
