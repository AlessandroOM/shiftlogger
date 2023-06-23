using Database;
using Models;
using shiftlogger.Interface;

namespace shiftlogger.Repositories
{
    public class LoggerRepository : IBaseRepository
    {
        private MyContext _context;
       
        public LoggerRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<Logger> Delete(int Id)
        {
           Logger innerLogger; 
           var entity = await _context.Loggers.FindAsync(Id);
           if (entity != null)
              {
                var myTask = Task.Run( () => _context.Loggers.Remove(entity));
                await myTask;
                _context.SaveChanges();
                innerLogger = entity;
              }
            else
            {
                 innerLogger = new Logger();
            }  

           return innerLogger;
        }

        public async Task<Logger> FinalizeLogger(int Id, DateTime endTime)
        {
           Logger innerLogger; 
           var entity = await _context.Loggers.FindAsync(Id);
           if (entity != null)
              {
                entity.Fim = endTime;
                var myTask = Task.Run( () =>  _context.Entry(entity).CurrentValues.SetValues(entity));
                await myTask;
                myTask = Task.Run( () =>  _context.SaveChanges());
                await myTask;                
                innerLogger = entity;
              }
            else
            {
                 innerLogger = new Logger();
            }  

           return innerLogger;
        }

        public async Task<Logger> FindById(int Id)
        {
            var myTask = Task.Run( () => _context.Loggers.FirstOrDefault(x => x.loggerID == Id));
            var entity = await myTask;
            if (entity == null)
            {
                entity = new Logger();
            }
            return entity;
        }

        public async Task<Logger> FindByActivity(string activity)
        {
           var myTask = Task.Run( () => _context.Loggers.FirstOrDefault(x => x.Atividade == activity));
            var entity = await myTask;
            if (entity == null)
            {
                entity = new Logger();
            }
            return entity;
        }

        public async Task<IEnumerable<Logger>> GetAll(int pagenumber, int quantity)
        {
              var myTask = Task.Run(() => _context.Loggers
                                          .Skip(pagenumber * quantity)
                                          .ToList()
                                          .Take(quantity)
                                          .OrderBy(x => x.loggerID));
              //var myTask = Task.Run(() => _context.Loggers.ToList());
              IEnumerable<Logger> entities = await myTask;
              return  entities;
        }

        public async Task<Logger> InitLogger(Logger newLog)
        {
           
           var result  = await _context.Loggers.AddAsync(newLog);

           await _context.SaveChangesAsync();
           return newLog;

        }
    }
}