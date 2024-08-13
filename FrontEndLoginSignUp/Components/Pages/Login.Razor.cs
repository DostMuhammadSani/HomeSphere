using ClassLibraryModel;
using System.Security.Claims;
using System.Text.Json;


namespace FrontEndLoginSignUp.Components.Pages
{
    public partial class Login
    {
        private LoginModel loginModel = new LoginModel();
        private string errorMessage = string.Empty;
        public string Token;
        private async Task HandleLogin()
        {
            var client = HttpClientFactory.CreateClient("AuthApi");
            var response = await client.PostAsJsonAsync("api/Auth/loginadmin", loginModel);

            if (response.IsSuccessStatusCode)
            {
                UserService.Username = loginModel.UserName;
                var jwtResponse = await response.Content.ReadFromJsonAsync<JwtResponse>();
                Token = jwtResponse.Token;
                await SessionStorage.SetItemAsync("authToken", Token);
                Navigation.NavigateTo("/residents");
            }
            else
            {
                // Retrieve the detailed error message from the response
                var errorDetails = await response.Content.ReadAsStringAsync();
                errorMessage = $"Login failed: {errorDetails}";

                Console.WriteLine($"Login Error: {errorMessage}");
            }
        }
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public class JwtResponse
        {
            public string Token { get; set; }
        }
    }
}
