namespace DTOS
{
    public class LoggerDto 
    {
         public int loggerID { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime? Fim { get; set; }
        
        public string? Atividade { get; set; }
    }
}