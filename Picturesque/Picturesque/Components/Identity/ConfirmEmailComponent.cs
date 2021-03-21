using Microsoft.AspNetCore.Components;

namespace Picturesque.Components
{
    public class ConfirmEmailComponent : ComponentBase
    {
        [Parameter]
        public string Email { get; set; }

        [Parameter]
        public string ConfirmationCode { get; set; }
    }
}
