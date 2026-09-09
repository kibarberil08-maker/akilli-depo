namespace AkilliDepo.Models
{
    public class Urun
    {
        public int Id { get; set; }

        public string Ad { get; set; } = "";

        public string? Barkod { get; set; }

        public string? RafNo { get; set; }

        public int Adet { get; set; } = 0;

        public int MinAdet { get; set; } = 5;

        public int HedefAdet { get; set; } = 20;
    }
}