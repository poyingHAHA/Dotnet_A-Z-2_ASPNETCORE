namespace WebAPI2_async.Services
{
    public class BigService
    {
        private string[] files;

        public BigService()
        {
            this.files = Directory.GetFiles("e:/Complete_Nodejs", ".exe", SearchOption.AllDirectories);
        }

        public int Coutnt { 
            get
            {
                return this.files.Length;
            } 
        }
    }
}
