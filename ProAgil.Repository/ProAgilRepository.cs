using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
            // Por defeito não bloqueia as tabelas que utiliza
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        // GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        { 
            return ( await _context.SaveChangesAsync()) > 0;
        }

        // EVENTOS

        public async Task<Evento[]> GetAllEventosAsync(bool includeOradores = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includeOradores)
            {
                query = query
                    .Include(oe => oe.OradoresEventos)
                    .ThenInclude(o => o.Orador);
            }

            query = query
                .AsNoTracking() // Evita que a tabela fique presa
                .OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includeOradores)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includeOradores)
            {
                query = query
                    .Include(oe => oe.OradoresEventos)
                    .ThenInclude(o => o.Orador);
            }

            query = query
                .AsNoTracking() // Evita que a tabela fique presa
                .OrderByDescending(c => c.DataEvento)
                .Where(c => c.Tema.Contains(tema));

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoAsyncById(int eventoId, bool includeOradores)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includeOradores)
            {
                query = query
                    .Include(oe => oe.OradoresEventos)
                    .ThenInclude(o => o.Orador);
            }

            query = query
                .AsNoTracking() // Evita que a tabela fique presa
                .OrderByDescending(c => c.DataEvento)
                .Where(c => c.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        // ORADORES
        public async Task<Orador[]> GetAllOradoresAsyncByName(string nome, bool includeEventos = false)
        {
            IQueryable<Orador> query = _context.Oradores
                .Include(c => c.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(oe => oe.OradoresEventos)
                    .ThenInclude(e => e.Evento);
            }

            query = query
                .AsNoTracking() // Evita que a tabela fique presa
                .OrderBy(c => c.Nome)
                .Where(c => c.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        
        public async Task<Orador> GetOradorAsyncById(int oradorId, bool includeEventos = false)
        {
            IQueryable<Orador> query = _context.Oradores
                .Include(c => c.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(oe => oe.OradoresEventos)
                    .ThenInclude(e => e.Evento);
            }

            query = query
                .AsNoTracking() // Evita que a tabela fique presa
                .OrderBy(c => c.Nome)
                .Where(c => c.Id == oradorId);

            return await query.FirstOrDefaultAsync();
        }
    }
}