using FinTasker.Application.Features.Auth.Commands.Register;
using FluentValidation;

namespace SiAman.Application.Features.Auth.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        // Format nomor telepon
        private const string PhoneRegex = @"^(\+62|62|0)8[1-9][0-9]{6,10}$";

        public RegisterValidator()
        {
            // ── Nama 
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nama lengkap wajib diisi.")
                .MinimumLength(3).WithMessage("Nama minimal 3 karakter.")
                .MaximumLength(100).WithMessage("Nama maksimal 100 karakter.");

            // Email 
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email wajib diisi.")
                .EmailAddress().WithMessage("Format email tidak valid.");

            // ── Password 
            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Password wajib diisi.")
                .MinimumLength(8).WithMessage("Password minimal 8 karakter.")
                .Matches(@"[A-Z]").WithMessage("Password harus mengandung minimal 1 huruf kapital.")
                .Matches(@"[0-9]").WithMessage("Password harus mengandung minimal 1 angka.");

            // ── Nomor Telepon 
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Nomor telepon wajib diisi.")
                .Matches(PhoneRegex).WithMessage("Format nomor telepon tidak valid. Contoh: 08123456789");

            // ── Role
            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Role tidak valid. Pilih antara Admin atau User.");
            
        }
    }
}