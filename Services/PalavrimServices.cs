using System.Globalization;
using System.Text;

namespace PalavrimAPI.Services
{
    public class PalavrimService
    {
        private readonly List<string> _palavras;
        private readonly int _tamanhoPalavra;

        public PalavrimService(int tamanhoPalavra = 5)
        {
            var caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PalavrimWordList", "portuguese.txt");
            _palavras = File.ReadAllLines(caminhoArquivo)
                            .Select(p => RemoverAcentos(p.ToLower().Trim()))
                            .Where(p => p.Length == tamanhoPalavra)
                            .Distinct()
                            .ToList();


            _tamanhoPalavra = tamanhoPalavra;
        }

        public int TamanhoPalavra => _tamanhoPalavra;

        public bool PalavraValida(string palavra)
        {
            return _palavras.Contains(RemoverAcentos(palavra.ToLower()));
        }

        public string PalavraDoDia()
        {
            var seed = DateTime.UtcNow.Date.ToString("yyyyMMdd").GetHashCode();
            var rand = new Random(seed);
            return _palavras[rand.Next(_palavras.Count)];
        }

        public string PalavraAleatoria()
        {
            var rand = new Random();
            return _palavras[rand.Next(_palavras.Count)];
        }

        public List<string> BuscarPorPrefixo(string prefixo)
        {
            prefixo = RemoverAcentos(prefixo.ToLower());
            return _palavras.Where(p => p.StartsWith(prefixo)).ToList();
        }

        public List<string> BuscarPorSufixo(string sufixo)
        {
            sufixo = RemoverAcentos(sufixo.ToLower());
            return _palavras.Where(p => p.EndsWith(sufixo)).ToList();
        }

        public List<string> TodasPalavras() => _palavras;

        private string RemoverAcentos(string texto)
        {
            return new string(texto.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }
    }
}
