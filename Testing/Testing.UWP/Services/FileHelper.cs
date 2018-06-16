﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Services.Interfaces;
using Testing.UWP.Services;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Testing.UWP.Services
{
    class FileHelper : IFileHelper
    {
        public string GetFileLocalPath(string path)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, path);
        }
    }
}