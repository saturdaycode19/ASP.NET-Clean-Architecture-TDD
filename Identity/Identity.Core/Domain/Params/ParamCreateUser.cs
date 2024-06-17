using System;
using FluentValidation;

namespace Identity.Core.Domain.Params
{
	public class ParamCreateUser
	{
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}

    public class ParamCreateUserValidator : AbstractValidator<ParamCreateUser>
    {
        public ParamCreateUserValidator()
        {
            RuleFor(x => x.FullName).NotNull().NotEmpty().WithMessage("Nama lengkap tidak boleh kosong");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email tidak boleh kosong");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password tidak boleh kosong");
        }
    }
}

