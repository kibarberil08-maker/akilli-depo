namespace AkilliDepo.Models
{
    public class Komut
    {
        public int Id { get; set; }

        public string Led { get; set; } = "";

        public int Buzzer { get; set; }

        public int? ServoAci { get; set; }

        public string Durum { get; set; } = "bekliyor";

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
    }
}