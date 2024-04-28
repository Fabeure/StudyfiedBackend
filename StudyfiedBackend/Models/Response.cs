namespace StudyfiedBackend.Models
{
    public class Response
    {
        public string answer { get; set; }
        public bool status { get; set; }
        public Response(string answer, bool status)
        {
            this.answer = answer;
            this.status = status;
        }
    }
}
