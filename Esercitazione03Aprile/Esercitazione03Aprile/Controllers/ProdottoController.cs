using Esercitazione03Aprile.Models;
using Esercitazione03Aprile.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Esercitazione03Aprile.Controllers
{

    [ApiController]
    [Route("api/prodotto")]

    public class ProdottoController : Controller
    {
        [HttpPost]
        public IActionResult InserisciProdotto(Prodotto objPro)
        {
            if (ProdottoRepository.GetIstanza().Insert(objPro))
                return Ok();

            return BadRequest();
        }

        [HttpPost("modifica")]
        public IActionResult ModificaProdotto(Prodotto objPro)
        {
            if (ProdottoRepository.GetIstanza().Update(objPro))
                return Ok();

            return BadRequest();
        }

        [HttpPost("elimina/{varCodice}")]
        public IActionResult EliminaPerCodice(string varCodice)
        {
            Prodotto? p = ProdottoRepository.GetIstanza().GetByCodice(varCodice);
            if (p is not null)
                if(ProdottoRepository.GetIstanza().DeleteByCodice(varCodice))
                    return Ok();

            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(ProdottoRepository.GetIstanza().GetAll());
        }

        [HttpGet("codice/{varCodice}")]
        public IActionResult GetByCodice(string varCodice)
        {
            return Ok(ProdottoRepository.GetIstanza().GetByCodice(varCodice));
        }

        [HttpPost ("aggiungi/{codice}")]
        public IActionResult AggiungiQuantita (string codice)
        {
            if (ProdottoRepository.GetIstanza().PiuQuantita(codice))
                return Ok();
            return BadRequest();
        }

        [HttpPost("rimuovi/{codice}")]
        public IActionResult RimuoviQuantita(string codice)
        {
            if (ProdottoRepository.GetIstanza().MenoQuantita(codice))
                return Ok();
            return BadRequest();
        }

        [HttpGet("ricerca/{stringa}")]
        public IActionResult RicercaGenerica(string stringa)
        {
            return Ok(ProdottoRepository.GetIstanza().RicercaGenerica(stringa));
        }
    }
}
