using System;
using Kinvey;

namespace XamarinSampleForms.Services
{
    public class KinveyService
    {
        private const string appKey = "kid_HJdVi4iZS";
        private const string appSecret = "856506e4b055433890775b8b0e7b9111";

        public Client kinveyClient;

        private static KinveyService _instance;

        protected KinveyService()
        {
            var builder = new Client.Builder(appKey, appSecret)
                        .setLogger(Console.WriteLine);

            kinveyClient = builder.Build();
        }

        public static KinveyService Instance()
        {
            if (_instance == null)
            {
                _instance = new KinveyService();
            }

            return _instance;
        }

    }
}
