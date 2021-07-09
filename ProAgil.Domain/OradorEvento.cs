namespace ProAgil.Domain
{
    public class OradorEvento
    {
        public int OradorId { get; set; }
        public Orador Orador { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}