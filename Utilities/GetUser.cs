using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace UehInternFrontend
{
    public class Service
    {

        public static async Task<User?> GetUser(IJSRuntime js, NavigationManager navigationManager, bool debug = false)
        {
            string token = await js.InvokeAsync<string>("userInfo");
            string json = AES.DecryptData(token);

            if (debug == true)
                await js.InvokeVoidAsync("console.log", json);

            if (!string.IsNullOrEmpty(json))
            {
                User? user = JsonConvert.DeserializeObject<User>(json);
                return user;
            }
            else
            {
                // No login cookie
                navigationManager.NavigateTo("/loginsessionexpired");
                return null;
            }
        }

    }
}