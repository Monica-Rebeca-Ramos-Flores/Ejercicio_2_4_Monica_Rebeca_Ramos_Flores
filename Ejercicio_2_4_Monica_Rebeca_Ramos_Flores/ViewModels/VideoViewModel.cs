using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

using Ejercicio_2_4_Monica_Rebeca_Ramos_Flores.Services;
using Ejercicio_2_4_Monica_Rebeca_Ramos_Flores.ViewModels;

namespace Ejercicio_2_4_Monica_Rebeca_Ramos_Floresara.ViewModels
{
    public class VideoViewModel : BaseViewModel
    {
        CameraService cameraService;

        private string _videoURL;

        public string VideoURL
        {
            get { return _videoURL; }
            set { _videoURL = value; OnPropertyChanged(); }
        }

        public ICommand TakeVideoCommand { get; private set; }
        public ICommand ChooseVideoCommand { get; private set; }

        public VideoViewModel()
        {
            cameraService = new CameraService();
            Task.Run(async () => await cameraService.Init());

            TakeVideoCommand = new Command(async () => await TakeVideo());
            ChooseVideoCommand = new Command(async () => await ChooseVideo());
        }

        private async Task TakeVideo()
        {
            var file = await cameraService.TakeVideo();

            if (file != null)
                VideoURL = file.Path;
        }

        private async Task ChooseVideo()
        {
            var file = await cameraService.ChooseVideo();

            if (file != null)
                VideoURL = file.Path;
        }
    }
}