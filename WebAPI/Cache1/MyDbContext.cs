using Cache1.Entity;

namespace Cache1
{
    public class MyDbContext
    {
        public static Task<Book?> GetByIdAsync(long id)
        {
            var result = GetById(id);
            return Task.FromResult(result);
        }

        public static Book GetById(long id)
        {
            switch(id)
            {
                case 0:
                    return new Book(0, "Java");
                case 1:
                    return new Book(1, "C#");
                case 2:
                    return new Book(2, "SQL");
                default:
                    return null;

            }
        }
    }
}
