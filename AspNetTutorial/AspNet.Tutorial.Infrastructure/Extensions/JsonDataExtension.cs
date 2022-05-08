using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;

using AspNet.Tutorial.Core.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNet.Tutorial.Infrastructure.Extensions
{
    public static class JsonDataExtension
    {
        public static void HasJsonData<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            string pathData = GetSeedingPath<TEntity>();

            if (File.Exists(pathData) is false)
            {
                return;
            }

            string rawJson = File.ReadAllText(pathData);
            IEnumerable<TEntity> entities = JsonSerializer.Deserialize<IEnumerable<TEntity>>(rawJson);

            if (entities is null)
            {
                return;
            }

            foreach (TEntity entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedBy = null;
                entity.ModifiedBy = null;
                entity.IsDeleted = false;
                entity.CreatedAt = DateTime.UtcNow;
                entity.ModifiedAt = DateTime.UtcNow;
            }

            builder.HasData(entities);
        }

        public static void HasJsonData<TEntity>(this EntityTypeBuilder<TEntity> builder, string mediaPath) where TEntity : BaseEntity
        {
            string pathData = GetSeedingPath<TEntity>();

            if (File.Exists(pathData) is false)
            {
                return;
            }

            string rawJson = File.ReadAllText(pathData);
            IEnumerable<TEntity> entities = JsonSerializer.Deserialize<IEnumerable<TEntity>>(rawJson);

            if (entities is null)
            {
                return;
            }

            foreach (TEntity entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedBy = null;
                entity.ModifiedBy = null;
                entity.IsDeleted = false;
                entity.CreatedAt = DateTime.UtcNow;
                entity.ModifiedAt = DateTime.UtcNow;
            }

            if (!Directory.Exists(mediaPath))
            {
                Directory.CreateDirectory(mediaPath);
            }

            if (Directory.EnumerateFiles(mediaPath).Count() == 0 && entities is IEnumerable<Product> products)
            {
                WebClient webClient = new();
                foreach (Product product in products)
                {
                    try
                    {
                        Uri uri = new(product.Image);
                        string ext = Path.GetExtension(product.Image);
                        string fileName = $"{Guid.NewGuid().ToString()}{ext}";
                        string localFile = $"{mediaPath}{Path.DirectorySeparatorChar}{fileName}";

                        product.Image = fileName;
                        webClient.DownloadFile(uri, localFile);

                        Console.WriteLine($"Saved {localFile}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.InnerException?.Message);
                        return;
                    }
                }
            }

            builder.HasData(entities);
        }

        public static string GetSeedingPath<TEntity>()
        {
            string parentDir = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
            string projectDir = typeof(JsonDataExtension).Assembly.GetName().Name;
            string fileName = $"Seeds{Path.DirectorySeparatorChar}{typeof(TEntity).Name}.json";
            string[] dirs = {parentDir, projectDir, fileName};
            return Path.Combine(dirs.Where(d => d is not null).ToArray());
        }
    }
}
