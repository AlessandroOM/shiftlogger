using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace shiftlogger.Interface
{
    public interface IBaseRepository
    {
        Task<List<Logger>> GetAll();

        Task<Logger> InitLogger(Logger newLog);

        Task<Logger> FindById(int Id);

        Task<Logger> Delete(int Id);

        Task<Logger> FinalizeLogger(int Id, DateTime endTime);

    }
}