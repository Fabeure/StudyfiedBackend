namespace StudyfiedBackend.Models
{
    public class Question
    {
        public string content { get; set; }
        //public int score { get; set; }
        /*public Question(string question, int score)
        {
            this.question = question;
            this.score = score;
        }*/

        public Question(string content)
        {
            this.content = content;
        }
    }
}
