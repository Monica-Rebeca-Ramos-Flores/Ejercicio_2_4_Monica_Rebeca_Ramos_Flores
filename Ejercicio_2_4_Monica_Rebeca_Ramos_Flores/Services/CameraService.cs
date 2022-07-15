using System;
using System.Threading.Tasks;

using Plugin.Media;
using Plugin.Media.Abstractions;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;

namespace Ejercicio_2_4_Monica_Rebeca_Ramos_Flores.Services
{
    public class CameraService
    {
        Plugin.Permissions.Abstractions.PermissionStatus cameraOK;
        Plugin.Permissions.Abstractions.PermissionStatus storageOK;

        public async Task Init()
        {
            await CrossMedia.Current.Initialize();

            cameraOK = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            storageOK = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraOK != Plugin.Permissions.Abstractions.PermissionStatus.Granted || storageOK != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                var status = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });

                cameraOK = status[Permission.Camera];
                storageOK = status[Permission.Storage];
            }
        }

        public async Task<MediaFile> TakePhoto()
        {
            if (cameraOK == Plugin.Permissions.Abstractions.PermissionStatus.Granted
                && storageOK == Plugin.Permissions.Abstractions.PermissionStatus.Granted
                && CrossMedia.Current.IsCameraAvailable
                && CrossMedia.Current.IsTakePhotoSupported)
            {
                var options = new StoreCameraMediaOptions
                {
                    DefaultCamera = CameraDevice.Front, // Doesn't always work on Android, depends on Device
                    AllowCropping = true, // UWP & iOS only,
                    PhotoSize = PhotoSize.Medium, // if Custom, you can set CustomPhotoSize = percentage_value 
                    CompressionQuality = 90,
                    Directory = "DemoCamara",
                    Name = $"{Guid.NewGuid()}.jpg",
                    SaveToAlbum = true
                };

                var file = await CrossMedia.Current.TakePhotoAsync(options);
                return file;
            }

            return null;
        }

        public async Task<MediaFile> ChoosePhoto()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var file = await CrossMedia.Current.PickPhotoAsync();
                return file;
            }

            return null;
        }

        public async Task<MediaFile> TakeVideo()
        {
            if (cameraOK == Plugin.Permissions.Abstractions.PermissionStatus.Granted
                && storageOK == Plugin.Permissions.Abstractions.PermissionStatus.Granted
                && CrossMedia.Current.IsTakeVideoSupported)
            {
                var options = new StoreVideoOptions { SaveToAlbum = true };
                var file = await CrossMedia.Current.TakeVideoAsync(options);
                return file;
            }

            return null;
        }

        public async Task<MediaFile> ChooseVideo()
        {
            if (CrossMedia.Current.IsPickVideoSupported)
            {
                var file = await CrossMedia.Current.PickVideoAsync();
                return file;
            }

            return null;
        }
    }
}
