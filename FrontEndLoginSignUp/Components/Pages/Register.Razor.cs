using ClassLibraryModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.JSInterop;

namespace FrontEndLoginSignUp.Components.Pages
{
    public partial class Register
    {
        [Inject]
        private IJSRuntime JSRun { get; set; }
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
                    Navigation.NavigateTo("/login");
                }
                else
                {
                    errorMessage = $"Admin registration failed: {await adminResponse.Content.ReadAsStringAsync()}";
                    await JSRun.InvokeVoidAsync("alert", errorMessage);
                }
            }
            else
            {
                errorMessage = $"Admin creation failed: {await registerResponse.Content.ReadAsStringAsync()}";
                await JSRun.InvokeVoidAsync("alert", errorMessage);
            }
        }
    }
    
}