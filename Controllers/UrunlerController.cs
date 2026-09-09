using AkilliDepo.Data;
using AkilliDepo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AkilliDepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunlerController : ControllerBase
    {
        private readonly DepoContext _context;

        public UrunlerController(DepoContext context)
        {
            _context = context;
        }

        // GET: api/Urunler
        [HttpGet]
        public IActionResult GetUrunler()
        {
            var urunler = _context.Urunler.ToList();
            return Ok(urunler);
        }

        // GET: api/Urunler/5
        [HttpGet("{id}")]
        public IActionResult GetUrun(int id)
        {
            var urun = _context.Urunler.Find(id);

            if (urun == null)
                return NotFound();

            return Ok(urun);
        }

        // POST: api/Urunler
        [HttpPost]
        public IActionResult UrunEkle(Urun urun)
        {
            _context.Urunler.Add(urun);
            _context.SaveChanges();

            return Ok(urun);
        }

        // PUT: api/Urunler/5
        [HttpPut("{id}")]
        public IActionResult UrunGuncelle(int id, Urun urun)
        {
            if (id != urun.Id)
                return BadRequest();

            _context.Entry(urun).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(urun);
        }

        // DELETE: api/Urunler/5
        [HttpDelete("{id}")]
        public IActionResult UrunSil(int id)
        {
            var urun = _context.Urunler.Find(id);

            if (urun == null)
                return NotFound(); var hareketler = _context.Hareketler
    .Where(h => h.UrunId == id)
    .ToList();

            _context.Hareketler.RemoveRange(hareketler);

            _context.Urunler.Remove(urun);
            _context.SaveChanges();

            return Ok();


        }
        // GET: api/Urunler/hareketler
        [HttpGet("hareketler")]
        public IActionResult HareketleriGetir()
        {
            var hareketler = _context.Hareketler
                .OrderByDescending(x => x.Tarih)
                .ToList();

            return Ok(hareketler);
        }

        // POST: api/Urunler/1/hareket
        [HttpPost("{id}/hareket")]
        public IActionResult HareketEkle(int id, [FromBody] HareketRequest request)
        {
            var urun = _context.Urunler.Find(id);

            if (urun == null)
                return NotFound();

            if (request.Tip == "giris")
                urun.Adet += request.Miktar;
            else if (request.Tip == "cikis")
                urun.Adet -= request.Miktar;
            else
                return BadRequest("Tip giris veya cikis olmalı.");

            var kritik = urun.Adet < urun.MinAdet;
            var komut = new Komut
            {
                Led = kritik ? "kirmizi" : "yesil",
                Buzzer = kritik ? 1 : 0,
                ServoAci = null,
                Durum = "bekliyor"
            };




            _context.Komutlar.Add(komut);
            var hareket = new Hareket
            {
                UrunId = urun.Id,
                Tip = request.Tip,
                Miktar = request.Miktar,
                Tarih = DateTime.Now,
                UrunAdi = urun.Ad,
                KullaniciAdi = request.kullaniciAdi
            };

            _context.Hareketler.Add(hareket);

            _context.SaveChanges();

            return Ok(new
            {
                adet = urun.Adet,
                kritik = kritik
            });
        }

        // POST: api/Urunler/1/urunver
        [HttpPost("{id}/urunver")]
        public IActionResult UrunVer(int id, [FromBody] HareketRequest request)
        {
            var urun = _context.Urunler.Find(id);

            if (urun == null)
                return NotFound();

            if (urun.Adet <= 0)
                return BadRequest("Stokta ürün yok.");

            // Stoktan 1 adet düş
            urun.Adet -= 1;

            // Servo 90 derece komutu
            var komut = new Komut
            {
                Led = "",
                Buzzer = 0,
                ServoAci = 90,
                Durum = "bekliyor"
            };

            _context.Komutlar.Add(komut);

            // Hareket geçmişine kaydet
            var hareket = new Hareket
            {
                UrunId = urun.Id,
                Tip = "cikis",
                Miktar = 1,
                Tarih = DateTime.Now,
                UrunAdi = urun.Ad,
                KullaniciAdi=request.kullaniciAdi
            };

            _context.Hareketler.Add(hareket);

            _context.SaveChanges();

            return Ok(new
            {
                adet = urun.Adet
            });
        }
        // POST: api/Urunler/barkod
        [HttpPost("barkod")]
        public IActionResult BarkodIslem([FromBody] BarkodRequest request)
        {
            var urun = _context.Urunler
                .FirstOrDefault(x => x.Barkod == request.Barkod);

            if (urun == null)
                return NotFound("Bu barkoda ait ürün bulunamadı.");

            if (request.Miktar <= 0)
                return BadRequest("Miktar 0'dan büyük olmalıdır.");

            if (request.Tip == "giris")
            {
                urun.Adet += request.Miktar;
            }
            else if (request.Tip == "cikis")
            {
                if (urun.Adet < request.Miktar)
                    return BadRequest("Stokta yeterli ürün yok.");

                urun.Adet -= request.Miktar;
            }
            else
            {
                return BadRequest("Tip giris veya cikis olmalı.");
            }

            var kritik = urun.Adet < urun.MinAdet;

            var komut = new Komut
            {
                Led = kritik ? "kirmizi" : "yesil",
                Buzzer = kritik ? 1 : 0,
                ServoAci = request.Tip == "cikis" ? 90 : null,
                Durum = "bekliyor"
            };

            _context.Komutlar.Add(komut);

            var hareket = new Hareket
            {
                UrunId = urun.Id,
                Tip = request.Tip,
                Miktar = request.Miktar,
                Tarih = DateTime.Now,
                UrunAdi = urun.Ad,
                KullaniciAdi = request.KullaniciAdi
            };

            _context.Hareketler.Add(hareket);

            _context.SaveChanges();

            return Ok(new
            {
                urun = urun.Ad,
                adet = urun.Adet
            });
        }
        public class BarkodRequest
        {
            public string Barkod { get; set; } = "";
            public string Tip { get; set; } = "";
            public int Miktar { get; set; }
            public string KullaniciAdi { get; set; } = "";
        }
    }
    }
