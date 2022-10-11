using Refit;

namespace Locaserv.Bdv.App.Models
{
    public class Test
    {
        public string deu_bom { get; set; }
        public string envireoment { get; set; }
        public Content content { get; set; }
    }

    public class Content
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime dateTimeOffset { get; set; }
        public DateTime dateTime { get; set; }
    }

    public interface ITest
    {
        [Get("")]
        Task<Test> GetTest();
    }
}