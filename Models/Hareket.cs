namespace AkilliDepo.Models
{
    public class Hareket
    {
        public int Id { get; set; }

        public int UrunId { get; set; }

        public string UrunAdi { get; set; } = "";

        public string KullaniciAdi { get; set; } = "";

        public string Tip { get; set; } = "";

        public int Miktar { get; set; }

        public DateTime Tarih { get; set; }
    }
}