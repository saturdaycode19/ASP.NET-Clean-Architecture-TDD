using System;
using FluentValidation;

namespace Identity.Core.Domain.Params
{
	public class ParamDeleteUser
	{
		public Guid UserId { get; set; }
	}

	public class ParamDeleteUserValidation : AbstractValidator<ParamDeleteUser>
	{
		public ParamDeleteUserValidation()
		{
			RuleFor(x => x.UserId).NotEqual(Guid.Empty).WithMessage("User ID harus diisi");
		}
    }
}

