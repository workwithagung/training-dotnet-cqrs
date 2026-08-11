using System.Data;
using FluentValidation;
using UserManagement.Application.Commands;

namespace UserManagement.Application.Validators;

public class CreatePegawaiCommandValidator: AbstractValidator<CreatePegawaiCommand>
{
    public CreatePegawaiCommandValidator()
    {
        RuleFor(x => x.Nama)
            .NotEmpty().WithMessage("Nama tidak boleh kosong.");
        
        RuleFor(x => x.JabatanId)
            .NotEmpty().WithMessage("JabatanId tidak boleh kosong.");

        RuleFor(x => x.Nip)
            .NotEmpty().WithMessage("NIP tidak boleh kosong.")
            .Length(18).WithMessage("NIP harus 18 digit.")
            .Matches(@"^\d+$").WithMessage("NIP hanya boleh terdiri dari angka.");

        RuleFor(x => x.Tunjangan)
            .NotEmpty().WithMessage("Tunjangan tidak boleh kosong.")
            .GreaterThan(1_000_000_000).WithMessage("Tunjangan harus lebih dari 1 milyar.");
    }
}