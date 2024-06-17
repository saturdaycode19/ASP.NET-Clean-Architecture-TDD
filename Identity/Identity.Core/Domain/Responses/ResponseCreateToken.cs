using System;
namespace Identity.Core.Domain.Responses
{
	public class ResponseCreateToken
	{
		public string Message { get; set; }
		public string AccessToken { get; set; }
	}
}

