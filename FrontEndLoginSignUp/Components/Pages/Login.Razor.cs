using ClassLibraryModel;


namespace FrontEndLoginSignUp.Components.Pages
{
    public partial class Login
    {
        private LoginModel loginModel = new LoginModel();
        private string errorMessage = string.Empty;

        private async Task HandleLogin()
        {
            var client = HttpClientFactory.CreateClient("AuthApi");
            var response = await client.PostAsJsonAsync("api/Auth/loginadmin", loginModel);

            if (response.IsSuccessStatusCode)
            {
                UserService.Username = loginModel.UserName;
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
    }
}
