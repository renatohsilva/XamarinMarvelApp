using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using MarvelApp.iOS.Services;
using MarvelApp.Service;

[assembly: Dependency(typeof(OpenWebService))]
namespace MarvelApp.iOS.Services
{
    public class OpenWebService : IOpenWebService
    {
        public OpenWebService()
        {
        }

        #region IOpenWebService implementation

        public void OpenUrl(string url)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
        }

        #endregion
    }
}