using System;
using System.Threading.Tasks;
using Kinvey;

namespace XamarinSampleForms.Services
{
    public class KinveyAuthService: IAuthService
    {
        private Client kinveyClient;

        public KinveyAuthService()
        {
            kinveyClient = KinveyService.Instance().kinveyClient;
        }
        
        public async Task<string> LoginUser(string username, string password)
        {
            if (kinveyClient.IsUserLoggedIn())
                return kinveyClient.ActiveUser.UserName;

            try
            {
                var user = await Kinvey.User.LoginAsync(username, password);
                return user.UserName;
            }
            catch (Exception e)
            {
                throw new System.Exception(e.Message);
            }
        }

        public async Task<string> RegisterUser(string username, string password)
        {
            if (kinveyClient.IsUserLoggedIn())
                this.LogoutUser();

            try
            {
                var user = await Kinvey.User.SignupAsync(username, password);
                return user.UserName;
            }
            catch (Exception e)
            {
                throw new System.Exception(e.Message);
            }
        }

        public void LogoutUser()
        {
            if (kinveyClient.IsUserLoggedIn() != true)
                return;

            try
            {
                kinveyClient.ActiveUser.Logout();
            }
            catch (Exception e)
            {
                throw new System.Exception(e.Message);
            }
        }

        public bool ActiveUserExists()
        {
            return kinveyClient.IsUserLoggedIn();
        }
    }
}
