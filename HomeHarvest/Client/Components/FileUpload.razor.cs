using HomeHarvest.Client.HttpRepository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Components
{
	public partial class FileUpload
    {
        [Parameter]
        public string ImgUrl { get; set; }

        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        [Inject]
        public ICropHttpRepository Repository { get; set; }

        private async Task HandleSelected(InputFileChangeEventArgs e)
        {
            var imageFiles = e.GetMultipleFiles();
            foreach (var imageFile in imageFiles)
            {
                if (imageFile != null)
                {
                    var resizedFile = await imageFile.RequestImageFileAsync("image/png", 300, 500);
                    using (var ms = resizedFile.OpenReadStream(resizedFile.Size))
                    {
                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), "image", imageFile.Name);
                        ImgUrl = await Repository.UploadPlotImage(content);
                        await OnChange.InvokeAsync(ImgUrl);
                    }
                }
            }
        }
    }
}