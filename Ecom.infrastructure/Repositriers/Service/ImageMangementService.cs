using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositriers.Service;

public class ImageMangementService : IImageMangementService
{
    private readonly IFileProvider fileProvider;
    public ImageMangementService(IFileProvider fileProvider)
    {
     this.fileProvider = fileProvider;
    }
    public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
    {
        List<string> SaveImageSrc = new List<string>();

        var ImadeDirectory = Path.Combine("wwwroot","Images",src);
        if (!Directory.Exists(ImadeDirectory))
        {
            Directory.CreateDirectory(ImadeDirectory);
        }

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var imageName = file.FileName;
                var imageSrc = ($"/Images/{src}/{imageName}");
                var fileRoot = Path.Combine(ImadeDirectory, file.FileName);

                using (FileStream stream = new FileStream(fileRoot, FileMode.Create))
                {
                  await file.CopyToAsync(stream);
                }
                SaveImageSrc.Add(imageSrc);
            }
        }
        return SaveImageSrc;

    }

    public void DeleteImageAsync(string src)
    {
        var info = fileProvider.GetFileInfo(src);

        var root = info.PhysicalPath;
        File.Delete(root);
    }
}
