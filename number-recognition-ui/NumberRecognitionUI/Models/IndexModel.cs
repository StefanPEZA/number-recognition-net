using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NumberRecognitionUI.Models
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost(IFormFile imageLoader)
        {
            /*HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            byte[] data = new byte[imageLoader.Length];
            imageLoader.OpenReadStream().Read(data, 0, (int)imageLoader.Length);
            form.Add(new ByteArrayContent(data));

            HttpResponseMessage response = httpClient.PostAsync("https://localhost:5001/api/v1/image/predict", form).Result;

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;

            System.Console.WriteLine(sd);*/
        }

    }
}