using Microsoft.AspNetCore.Http;

namespace FriendsApp.API.Helpers
{
    // the static keyword means that you don't need to create a new instance of Extensions
        //  when you wanna use one of its methods;
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}