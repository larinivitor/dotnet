using System.Collections.Generic;

namespace Crescer.Spotify.Dominio.Servicos
{
    public class AvaliacaoService
    {
        public List<string> Validar(int nota)
        {
            List<string> mensagens = new List<string>();

            if (nota < 1 || nota > 5)
                mensagens.Add("É necessário informar uma nota com valores de 1 a 5.");
            
            return mensagens;
        }
    }
}