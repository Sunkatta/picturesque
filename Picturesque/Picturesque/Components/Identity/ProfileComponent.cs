using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class ProfileComponent : ComponentBase
    {
        [Parameter]
        public int TabIndex { get; set; }

        protected int tabIndex = 0;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                tabIndex = TabIndex;
                StateHasChanged();
            }
        }
    }
}
