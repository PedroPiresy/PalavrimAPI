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
            var caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PalavrimWordList", "portugueseV2.txt");
            // MANTÉM os acentos aqui:
            _palavras = File.ReadAllLines(caminhoArquivo, Encoding.UTF8)
                            .Select(p => p.ToLower().Trim()) // não remove acentos
                            .Where(p => p.Length == tamanhoPalavra)
                            .Distinct()
                            .ToList();

            _tamanhoPalavra = tamanhoPalavra;
        }

        public int TamanhoPalavra => _tamanhoPalavra;

        public bool PalavraValida(string palavra)
        {
            // Valida removendo acentos de ambos os lados
            return _palavras.Any(p => RemoverAcentos(p) == RemoverAcentos(palavra.ToLower()));
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
            return _palavras.Where(p => RemoverAcentos(p).StartsWith(prefixo)).ToList();
        }

        public List<string> BuscarPorSufixo(string sufixo)
        {
            sufixo = RemoverAcentos(sufixo.ToLower());
            return _palavras.Where(p => RemoverAcentos(p).EndsWith(sufixo)).ToList();
        }

        public List<string> TodasPalavras() => _palavras;

        private string RemoverAcentos(string texto)
        {
            return new string(texto.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }
        public string? BuscarPalavraAcentuada(string palavraSemAcento)
        {
            // Remove acentos da palavra de entrada para comparação
            var palavraSemAcentoNormalizada = RemoverAcentos(palavraSemAcento.ToLower());

            // Busca na lista de palavras a versão acentuada
            var palavraAcentuada = _palavras.FirstOrDefault(p =>
                RemoverAcentos(p.ToLower()) == palavraSemAcentoNormalizada);

            return palavraAcentuada;
        }

        public Dictionary<string, string> BuscarPalavrasAcentuadas(List<string> palavras)
        {
            var resultado = new Dictionary<string, string>();

            foreach (var palavra in palavras)
            {
                var acentuada = BuscarPalavraAcentuada(palavra);
                resultado[palavra.ToLower()] = acentuada ?? palavra;
            }

            return resultado;
        }
    }
}
