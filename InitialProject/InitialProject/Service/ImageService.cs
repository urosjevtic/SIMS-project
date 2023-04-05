using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class ImageService
    {
        private readonly ImageRepository _imageRepository;

        public ImageService()
        {
            _imageRepository = new ImageRepository();
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
