using System.ComponentModel.DataAnnotations;

namespace ActionFilterDemo
{
    public class Person
    {
        public long Id { get; set; }
        [MaxLength(3)]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
