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

        public async Task<List<Logger>> GetAll()
        {
              var myTask = Task.Run( () => _context.Loggers.ToList());
              List<Logger> entities = await myTask;
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