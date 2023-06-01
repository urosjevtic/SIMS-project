using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using Microsoft.VisualBasic.ApplicationServices;

namespace InitialProject.Repository
{
    public class ImageRepository : IImageRepository
    {
        private const string FilePath = "../../../Resources/Data/images.csv";

        private readonly Serializer<Image> _serializer;

        private List<Image> _images;

        public ImageRepository()
        {
            _serializer = new Serializer<Image>();
            _images = _serializer.FromCSV(FilePath);
        }

        public List<Image> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public List<string> GetAllImagesById(int id)
        {
            _images = _serializer.FromCSV(FilePath);
            foreach(Image image in _images)
            {
                if(image.Id == id)
                {
                    return image.Url;
                }
            }
            return null;
        }
        public Image ReturnSaved(Image image)
        {
            image.Id = NextId();
            _images = _serializer.FromCSV(FilePath);
            _images.Add(image);
            _serializer.ToCSV(FilePath, _images);
            return image;
        }

        public int NextId()
        {
            _images = _serializer.FromCSV(FilePath);
            if (_images.Count < 1)
            {
                return 1;
            }
            return _images.Max(c => c.Id) + 1;
        }

        public Image GetById(int id)
        {
            _images = _serializer.FromCSV(FilePath);
            return _images.FirstOrDefault(i => i.Id == id);
        }

        public void Delete(Image image)
        {
            _images = _serializer.FromCSV(FilePath);
            Image founded = _images.Find(c => c.Id == image.Id);
            _images.Remove(founded);
            _serializer.ToCSV(FilePath, _images);
        }

        public void Save(Image image)
        {
            image.Id = NextId();
            _images = _serializer.FromCSV(FilePath);
            _images.Add(image);
            _serializer.ToCSV(FilePath, _images);
        }

        public void SaveAll(List<Image> entities)
        {
            throw new NotImplementedException();
        }
        public void Update(Tour tour, ObservableCollection<string> urls)
        {
            _images = _serializer.FromCSV(FilePath);
            Image found = _images.Find(c => c.Id == tour.CoverImageUrl.Id);
            foreach(string s in urls)
            {
                found.Url.Add(s);
            }
            _serializer.ToCSV(FilePath, _images);
        }
        public void Update(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}
