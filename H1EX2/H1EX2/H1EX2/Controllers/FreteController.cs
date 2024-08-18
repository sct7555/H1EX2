using H1EX2.Models;
using Microsoft.AspNetCore.Mvc;

namespace H1EX2.Controllers {
    [ApiController]
    [Route("API/Frete")]
    public class FreteController : ControllerBase {

        private readonly Dictionary<string, decimal> tarifasPorEstado = new Dictionary<string, decimal>
     {
            { "SP", 50.00m },
            { "RJ", 60.00m },
            { "MG", 55.00m },
            { "OUTROS", 70.00m }
        };

        private const decimal taxaPorCm3 = 0.01m;

        [HttpPost]
        [HttpPost("calcular-frete")]
        public IActionResult CalcularFrete(Frete _frete) {
            
            float volume = (float)(_frete.Altura * _frete.Largura * _frete.Comprimento);

        
            string uf = _frete.UF.ToUpper();
            decimal tarifaEstado = tarifasPorEstado.ContainsKey(uf) ? tarifasPorEstado[uf] : tarifasPorEstado["OUTROS"];

          
            decimal valorFrete = (decimal)volume * taxaPorCm3 + tarifaEstado;

            var resultado = new {
                NomeProduto = _frete.Nome,
                Peso = _frete.Peso,
                Volume = volume,
                UF = uf,
                TarifaEstado = tarifaEstado,
                ValorFrete = Math.Round(valorFrete, 2)
            };

            return Ok(resultado);
        }
    }
}
