using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
namespace AndroidApp
{
    [Activity(Label = "Xamarin Android App", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button BtnDownload;
        ImageView Image;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            BtnDownload = FindViewById<Button>(Resource.Id.btndownload);
            Image = FindViewById<ImageView>(Resource.Id.image);
            BtnDownload.Click += PutImage;
        }
        async void PutImage(object sender, EventArgs e)
        {
            var path = await DownloadImage();
            Android.Net.Uri ImagePath = Android.Net.Uri.Parse
                (path);
            Image.SetImageURI(ImagePath);
        }
        public async Task<string> DownloadImage()
        {
            var client = new WebClient();
            byte[] imageData = await client.DownloadDataTaskAsync
                ("https://pbs.twimg.com/media/DZt_rBAVAAAtNWe.jpg");
            var documentspath = System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal);
            var localfilename = "mydog.jpg";
            var localpath = Path.Combine(documentspath, localfilename);
            File.WriteAllBytes(localpath, imageData);
            return localpath;
        }
    }
}