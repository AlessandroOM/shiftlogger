using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shiftlogger.Model
{
    public class CustomResult
    {
        public CustomResult()
        {
            Messages = new List<string>();
        }

        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public object Data { get; set; }



    }
}