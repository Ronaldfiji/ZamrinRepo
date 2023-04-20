using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
namespace CoffeeNext.Helper
{
    public class UIMessage
    {
        public static ToastOptions DisplayTimeOutMessage() {

            var Message = new ToastOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.Red,
                    Message = "Session Timeout. Redirecting to app home!",
                    Font = Font.SystemFontOfSize(14).WithSize(20),
                    Padding = new Thickness(0, 0, 10, 100),


                },
                BackgroundColor = Color.FromHex("#03fcc6"),
                Duration = TimeSpan.FromSeconds(1),
                CornerRadius = 1,
                IsRtl = false,

            };
            return Message;

        }
        
    }
}
