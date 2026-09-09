namespace AkilliDepo.Models
{
    public class Kullanici
    {
        public int Id { get; set; }

        public string KullaniciAdi { get; set; } = "";

        public string Sifre { get; set; } = "";

        public string RfidUid { get; set; } = "";
    }
}
