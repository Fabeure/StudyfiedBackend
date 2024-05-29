namespace StudyfiedBackend.Models
{
    public class Question
    {
        public string question { get; set; }
        //public int score { get; set; }
        /*public Question(string question, int score)
        {
            this.question = question;
            this.score = score;
        }*/

        public Question(string question)
        {
            this.question = question;
        }
    }
}
