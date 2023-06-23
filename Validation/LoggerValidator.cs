using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Models;

namespace shiftlogger.Validation
{
    public class LoggerValidator : AbstractValidator<Logger>
    {
        public LoggerValidator()
        {
            {
                RuleFor(p => p.Atividade)
                       .NotEmpty()
                       .WithMessage("A atividade é obrigatória.");
                RuleFor(p => p.Inicio)
                        .NotNull()
                        .WithMessage("É necessário informar o tempo inicial")
                        .When(p => p.loggerID == 0);
                RuleFor(p => p.Fim)
                       .NotNull()
                       .WithMessage("É necessário informar o tempo final")
                       .When(p => p.loggerID >0);
                RuleFor(p => p.Fim)
                       .GreaterThanOrEqualTo(p => p.Inicio)
                       .WithMessage("O tempo final deve ser maior que tempo inicial")
                       .When(p => p.loggerID >0);
            }
        }
    }
}