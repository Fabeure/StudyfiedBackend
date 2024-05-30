namespace StudyfiedBackend.Models
{
    public class Answer
    {
        public string content { get; set; }
        public bool status { get; set; }
        public Answer(string content, bool status)
        {
            this.content = content;
            this.status = status;
        }
    }
}
