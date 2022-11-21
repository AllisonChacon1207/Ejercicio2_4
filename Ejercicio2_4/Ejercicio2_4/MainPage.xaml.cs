using Ejercicio2_4.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Forms.VideoPlayer;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Ejercicio2_4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public string PhotoPath;

        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {

            var status = await Permissions.RequestAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                return;
            }

            GrabarVideo();
        }
        //btnSalvar_Clicked
        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdescripcion.Text))
            {
                await DisplayAlert("Warning", "Llene todos los campos", "OK");
                txtdescripcion.Focus();
            }
            else
            {
                var videos = new Models.VideoModel
                {
                    Uri = PhotoPath,
                    Descripcion = txtdescripcion.Text
                };
                var resultado = await App.BaseDatosObject.SaveVideoRecord(videos);

                if (resultado == 1)
                {
                    await DisplayAlert("", "Video Guardado con exito", "OK");
                    txtdescripcion.Text = "";
                    videoPlayer.Source = null;
                }
                else
                {
                    await DisplayAlert("warning", "ERROR", "OK");
                }
            }
        }

        private async void btnlistarvideo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListarVideosPage());
        }




        async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            PhotoPath = newFile;
        }

        public async void GrabarVideo()
        {
            try
            {
                var photo = await MediaPicker.CaptureVideoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");

                UriVideoSource uriVideoSurce = new UriVideoSource()
                {
                    Uri = PhotoPath
                };

                videoPlayer.Source = uriVideoSurce;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }


    }
}
