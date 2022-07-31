using System.ComponentModel.DataAnnotations;

namespace ActionFilterDemo
{
    public class Book
    {
        public long Id { get; set; }
        [MaxLength(3)]
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
