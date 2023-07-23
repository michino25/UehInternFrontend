using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http;

namespace UehInternFrontend
{
    public class Service
    {
        public static async Task<User?> GetUser(IJSRuntime js, NavigationManager navigationManager, bool debug = false, bool strict = true)
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
            else if (strict)
            {
                // No login cookie
                navigationManager.NavigateTo("/loginsessionexpired", true);
                return null;
            }
            return null;
        }

        public static async Task<bool> LoginRequest(IJSRuntime js, NavigationManager navigationManager, string api, UserLogin user, string RedirectTo = null)
        {
            if (user != null)
            {
                string token = System.Text.Json.JsonSerializer.Serialize(user);
                token = AES.EncryptData(token);
                // Debug get token
                // await js.InvokeVoidAsync("console.log", token);

                token = Uri.EscapeDataString(token);

                var url = api + $"User/LoginUser?encryptedJson={token}";

                using var client = new HttpClient();
                var response = await client.PostAsync(url, new StringContent(""));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    User? getUser = JsonConvert.DeserializeObject<User>(content);
                    getUser.RoleRestore = getUser.Role == "admin" ? "admin" : "";

                    string tokenUser = System.Text.Json.JsonSerializer.Serialize(getUser);
                    tokenUser = AES.EncryptData(tokenUser);
                    await js.InvokeVoidAsync("userLogin", tokenUser, 30);

                    if (RedirectTo != null)
                    {
                        navigationManager.NavigateTo(RedirectTo, forceLoad: true);
                    }
                    return true;
                }
            }
            await js.InvokeVoidAsync("alert", "Đã xảy ra lỗi khi gửi yêu cầu");
            return false;
        }

        public static async Task AdminChangeMode(IJSRuntime js, NavigationManager navigationManager, string RedirectTo)
        {
            string token = await js.InvokeAsync<string>("userInfo");
            string json = AES.DecryptData(token);

            if (!string.IsNullOrEmpty(json))
            {
                User? user = JsonConvert.DeserializeObject<User>(json);
                if (user.RoleRestore == "admin")
                {
                    user.Role = user.Role == "admin" ? "teacher" : "admin";

                    string tokenUser = System.Text.Json.JsonSerializer.Serialize(user);
                    tokenUser = AES.EncryptData(tokenUser);
                    await js.InvokeVoidAsync("userLogin", tokenUser, 30);

                    navigationManager.NavigateTo(RedirectTo, forceLoad: true);
                }
                else
                {
                    await js.InvokeVoidAsync("alert", "Đã xảy ra lỗi khi gửi yêu cầu");
                }
            }
            else
            {
                // No login cookie
                navigationManager.NavigateTo("/loginsessionexpired", true);
            }

        }
    }
}
