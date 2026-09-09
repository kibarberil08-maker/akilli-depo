using AkilliDepo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AkilliDepo.Controllers
{
    [Route("api/cihaz")]
    [ApiController]
    public class CihazController : ControllerBase
    {
        private readonly DepoContext _context;
        private static string sonRfid = "";

        public CihazController(DepoContext context)
        {
            _context = context;
        }

        // GET: api/cihaz/komut
        [HttpGet("komut")]
        public IActionResult KomutGetir()
        {
            var komut = _context.Komutlar
                .Where(x => x.Durum == "bekliyor")
                .OrderBy(x => x.Id)
                .FirstOrDefault();

            if (komut == null)
            {
                return Ok(new
                {
                    komut_var = false
                });
            }

            komut.Durum = "gonderildi";

            _context.SaveChanges();

            return Ok(new
            {
                komut_var = true,
                led = komut.Led,
                buzzer = komut.Buzzer,
                servo_aci = komut.ServoAci
            });
        }
        // POST: api/cihaz/rfid
        [HttpPost("rfid")]
        public IActionResult RfidOku([FromBody] RfidRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Uid))
            {
                return BadRequest("RFID UID boş olamaz.");
            }

            sonRfid = request.Uid.Trim().ToUpper();
            Console.WriteLine("RFID Kart Okundu: " + sonRfid);

            return Ok(new
            {
                mesaj = "RFID kart okundu.",
                uid = request.Uid
            });
        }
        
        [HttpGet("rfid")]
        public IActionResult SonRfid()
        {
            if (string.IsNullOrEmpty(sonRfid))
            {
                return Ok(new
                {
                    uid = ""
                });
            }

            string uid = sonRfid;

            // Kullanıldıktan sonra temizle
            sonRfid = "";

            return Ok(new
            {
                uid = uid
            });
        }


        public class RfidRequest
        {
            public string Uid { get; set; } = "";
        }
    }
}