using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;


namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IImageRepository
    {
        List<Image> GetAll();
        Image ReturnSaved(Image image);
        int NextId();
        Image GetById(int id);
        void Delete(Image image);
        void Save(Image image);
        void SaveAll(List<Image> entities);
        void Update(Image entity);
        void Update(Tour tour, ObservableCollection<string> urls);
        List<string> GetAllImagesById(int id);
    }
}