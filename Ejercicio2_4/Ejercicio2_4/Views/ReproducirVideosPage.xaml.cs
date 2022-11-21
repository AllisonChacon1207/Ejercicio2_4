using Ejercicio2_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xam.Forms.VideoPlayer;


namespace Ejercicio2_4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReproducirVideoPage : ContentPage
    {
        public ReproducirVideoPage(VideoModel video)
        {
            InitializeComponent();
            setearDatos(video);
        }
        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void setearDatos(VideoModel video)
        {
            lbldescripcion.Text = video.Descripcion;
            UriVideoSource uriVideoSurce = new UriVideoSource()
            {
                Uri = video.Uri
            };
            videoPlayer2.Source = uriVideoSurce;
        }
    }
}