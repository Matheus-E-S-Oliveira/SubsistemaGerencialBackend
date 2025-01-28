
using System.Globalization;
using System.Text;

namespace SubsistemaGerencialBackend.Models.Clientes
{
    public class ClienteRepository : IClienteRepository
    {
        public string RemoverAcentos(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return texto;
            }

            // Normaliza a string para decompor caracteres com acento
            var textoNormalizado = texto.Normalize(NormalizationForm.FormD);

            // Filtra os caracteres que são acentos e os remove
            var textoSemAcento = new string(textoNormalizado
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            return textoSemAcento;
        }
    }
}
