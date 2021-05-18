namespace Picturesque.Models
{
    public class UserStatisticsInputModel
    {
        public string UserId { get; set; }

        public bool HasWon { get; set; }

        public int Playtime { get; set; }

        public int NumberOfMistakes { get; set; }

        public int Score { get; set; }
    }
}
