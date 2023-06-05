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
        public List<Domain.Model.Notification> GetAll();


        public Domain.Model.Notification FindById(int id);


        public void Save(Domain.Model.Notification notification);

        public List<Domain.Model.Notification> GetAllById(int id);
        public int NextId();

        public void Delete(Domain.Model.Notification notification);

        public void SaveAll(List<Domain.Model.Notification> notifications);

        public void Update(Domain.Model.Notification notification);

        public void DeleteAll();
       
    }
}
