using System;
using FluentValidation;

namespace Identity.Core.Domain.Params
{
	public class ParamCreateToken
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}

	public class ParamCreateTokenValidation : AbstractValidator<ParamCreateToken>
	{
		public ParamCreateTokenValidation()
		{
			RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email tidak boleh kosong");
			RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password tidak boleh kosong");
		}
    }
}

