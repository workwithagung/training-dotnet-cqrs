using FluentValidation;
using UserManagement.Application.Commands;

namespace UserManagement.Application.Validators;

public class CreateJabatanValidator: AbstractValidator<CreateJabatanCommand>
{
    public CreateJabatanValidator()
    {
        RuleFor(x => x.Nama)
            .NotEmpty().WithMessage("Nama tidak boleh kosong.");

        RuleFor(x => x.Level)
            .NotEmpty().WithMessage("Level tidak boleh kosong.")
            .InclusiveBetween(0, 5).WithMessage("Level harus berada di rentang 0 s.d. 5");
    }
}