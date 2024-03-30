namespace StudyfiedBackend.Models
{
    public class FlashCard
    {
        public string[] items { get; set; }

        public FlashCard(string[] items)
        {
            this.items = items;
        }   
    }
}
