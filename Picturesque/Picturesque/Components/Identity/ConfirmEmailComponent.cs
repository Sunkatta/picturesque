using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
