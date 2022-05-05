using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace AspNet.Tutorial.WebApi.Extensions
{
    public static class StaticPathExtension
    {
        public static void UseExternalStaticFiles(this IApplicationBuilder app, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            app.UseStaticFiles(new StaticFileOptions {FileProvider = new PhysicalFileProvider(path), RequestPath = ""});
        }
    }
}
