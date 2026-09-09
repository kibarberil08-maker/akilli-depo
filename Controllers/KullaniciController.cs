using AkilliDepo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AkilliDepo.Controllers.CihazController;

namespace AkilliDepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly DepoContext _context;

        public KullaniciController(DepoContext context)
        {
            _context = context;
        }

        [HttpPost("giris")]
        public IActionResult Giris([FromBody] GirisRequest request)
        {
            var kullanici = _context.Kullanicilar
                .FirstOrDefault(x =>
                    x.KullaniciAdi == request.KullaniciAdi &&
                    x.Sifre == request.Sifre);

            if (kullanici == null)
            {
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");
            }

            return Ok(new
            {
                basarili = true,
                kullaniciAdi = kullanici.KullaniciAdi
            });
        }
        [HttpPost("rfid")]
        public IActionResult RfidGiris([FromBody] RfidRequest request)
        {
            var kullanici = _context.Kullanicilar
                .FirstOrDefault(x => x.RfidUid == request.Uid);

            if (kullanici == null)
            {
                return Unauthorized("Bu RFID kart kayıtlı değil.");
            }

            return Ok(new
            {
                basarili = true,
                kullaniciAdi = kullanici.KullaniciAdi

            });
        }
        [HttpPost("rfid-kaydet")]
        public IActionResult RfidKaydet([FromBody] RfidKayitRequest request)
        {
            var kullanici = _context.Kullanicilar
                .FirstOrDefault(x => x.KullaniciAdi == request.KullaniciAdi);

            if (kullanici == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            kullanici.RfidUid = request.Uid.Trim().ToUpper();

            _context.SaveChanges();

            return Ok(new
            {
                mesaj = "RFID kart kullanıcıya tanımlandı.",
                kullaniciAdi = kullanici.KullaniciAdi,
                uid = kullanici.RfidUid
            });
        }
    }

    public class GirisRequest
    {
        public string KullaniciAdi { get; set; } = "";
        public string Sifre { get; set; } = "";
    }
    public class RfidKayitRequest
{
    public string KullaniciAdi { get; set; } = "";
    public string Uid { get; set; } = "";
}
    
}