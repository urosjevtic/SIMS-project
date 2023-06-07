using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Service
{
    public class ImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService()
        {
            _imageRepository = Injector.Injector.CreateInstance<IImageRepository>();
        }


        public Image GetById(int id)
        {
            return _imageRepository.GetById(id);
        }

        public List<string> GetAllById(Tour t)
        {
            return _imageRepository.GetAllImagesById(t.CoverImageUrl.Id);
        }

        public void SaveImages(int entityId, string urls)
        {
            Image images = new Image();
            images.EntityLd = entityId;
            string[] imagesUrls = SplitUrlByComma(urls);
            foreach (string imageUrl in imagesUrls)
            {
                images.Url.Add(imageUrl);
            }

            _imageRepository.Save(images);
        }

        public void SaveImages(List<string> urls)
        {
            Image images = new Image();
            images.EntityLd = 0;
            foreach (string imageUrl in urls)
            {
                images.Url.Add(imageUrl);
            }

            _imageRepository.Save(images);
        }

        private string[] SplitUrlByComma(string input)
        {
            return input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public int GetImagesId(string urls)
        {
            List<Image> allImages = _imageRepository.GetAll();
            string[] imageUrls = SplitUrlByComma(urls);
            foreach (Image image in allImages)
            {
                if (image.Url.SequenceEqual(imageUrls))
                {
                    return image.Id;
                }
            }
            throw new Exception("Error has occured");
        }
    }
}
