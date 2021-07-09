using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        
        // GERAL
        void Add<T> (T entity) where T: class;
        void Update<T> (T entity) where T: class;
        void Delete<T> (T entity) where T: class;
        Task<bool> SaveChangesAsync();

        // EVENTOS
        Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includeOradores);
        Task<Evento[]> GetAllEventosAsync(bool includeOradores);
        Task<Evento> GetEventoAsyncById(int eventoId, bool includeOradores);

        // ORADORES
        Task<Orador[]> GetAllOradoresAsyncByName(string none, bool includeEventos);
        Task<Orador> GetOradorAsyncById(int oradorId, bool includeEventos);
    }
}