using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Logger 
    {
        public Logger()
        {
            Inicio = DateTime.UtcNow;
            Atividade = "";
            Fim = null;
        }


        [Key]
        public int loggerID { get; set; }

        [Required]
        public DateTime Inicio { get; set; }

        public DateTime? Fim { get; set; }
        
         [Required]
         [MaxLength(50)]
        public string? Atividade { get; set; }
    }

}
