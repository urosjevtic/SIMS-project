using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        public List<Notification> GetAll();


        public Notification FindById(int id);


        public void Save(Notification notification);

        public List<Notification> GetAllById(int id);
        public int NextId();

        public void Delete(Notification notification);

        public void SaveAll(List<Notification> notifications);

        public void Update(Notification notification);

        public void DeleteAll();
       
    }
}
