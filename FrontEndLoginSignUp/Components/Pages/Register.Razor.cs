using ClassLibraryModel;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FrontEndLoginSignUp.Components.Pages
{
    public partial class Register
    {
        private AdminModel person = new AdminModel();
        private string errorMessage = string.Empty;

        private async Task HandleRegister()
        {
            Guid Id = Guid.NewGuid();
            person.A_id = Id.ToString();
            var client = HttpClientFactory.CreateClient("AuthApi");

            var registerResponse = await client.PostAsJsonAsync("api/Auth/registeradmin", person);
            if (registerResponse.IsSuccessStatusCode)
            {
                var adminResponse = await client.PostAsJsonAsync("api/Admin", person);
                if (adminResponse.IsSuccessStatusCode)
                {
                    Navigation.NavigateTo("/");
                }
                else
                {
                    errorMessage = $"Admin registration failed: {await adminResponse.Content.ReadAsStringAsync()}";
                    Console.WriteLine($"Admin Registration Error: {errorMessage}");
                }
            }
            else
            {
                errorMessage = $"Admin creation failed: {await registerResponse.Content.ReadAsStringAsync()}";
                Console.WriteLine($"Admin Creation Error: {errorMessage}");
            }
        }
    }

}