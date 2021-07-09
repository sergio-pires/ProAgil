using System.Collections.Generic;

namespace ProAgil.Domain
{
    public class Orador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<RedeSocial> RedesSociais { get; set; }
        public List<OradorEvento> OradoresEventos { get; set; }
    }
}