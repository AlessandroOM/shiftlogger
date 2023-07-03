using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace shiftlogger.Interface
{
    public interface IBaseRepository
    {
        Task<IEnumerable<Logger>> GetAll(int pagenumber, int quantity);

        Task<Logger> InitLogger(Logger newLog);

        Task<Logger> FindById(int Id);
        Task<Logger> FindByActivity(string activity);

        Task<Logger> Delete(int Id);

        Task<Logger> FinalizeLogger(int Id, DateTime endTime);

        Task<int> CountRecords();

        Task<int> DeleteAll();

    }
}