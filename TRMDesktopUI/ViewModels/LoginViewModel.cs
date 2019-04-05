using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using TRMDesktopUI.Helpers;

namespace TRMDesktopUI.ViewModels
{
	public class LoginViewModel : Screen
	{
		private string _userName;
		private string _password;
		private string _errorMessage;
		private IApiHelper _apiHelper;

		public LoginViewModel(IApiHelper apiHelper)
		{
			_apiHelper = apiHelper;
		}

		public string UserName
		{
			get { return _userName; }
			set
			{
				_userName = value;
				NotifyOfPropertyChange(() => UserName);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}

		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				NotifyOfPropertyChange(() => Password);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}

		public bool IsErrorVisible
		{
			get
			{
				bool output = false;
				if (ErrorMessage?.Length > 0)
				{
					output = true;
				}
				return output;
			}
		}

		public string ErrorMessage
		{
			get { return _errorMessage; }
			set
			{
				_errorMessage = value;
				NotifyOfPropertyChange(() => ErrorMessage);
				NotifyOfPropertyChange(() => IsErrorVisible);
			}
		}



		public bool CanLogIn
		{
			get
			{
				bool output = false;
				if (UserName?.Length > 0 && !string.IsNullOrEmpty(Password))
				{
					output = true;
				}

				return output;
			}
		}

		public async Task LogIn()
		{
			try
			{
				ErrorMessage = string.Empty;
				var result = await _apiHelper.Authenticate(UserName, Password);
			}
			catch (Exception ex)
			{
				ErrorMessage = ex.Message;
			}
		}
	}
}
