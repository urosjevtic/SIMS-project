using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InitialProject.Domain.Model.Users;
using InitialProject.Domain.RepositoryInterfaces.IUsersRepo;
using InitialProject.Serializer;

namespace InitialProject.Repository.UserRepo
{
    public class OwnerSettingsRepository : IOwnerSettingsRepository
    {

        private const string FilePath = "../../../Resources/Data/ownerSettings.csv";

        private readonly Serializer<OwnerSettings> _serializer;

        private List<OwnerSettings> _ownerSettings;

        public OwnerSettingsRepository()
        {
            _serializer = new Serializer<OwnerSettings>();
            _ownerSettings = _serializer.FromCSV(FilePath);
        }


        public List<OwnerSettings> GetAll()
        {
           return _serializer.FromCSV(FilePath);
        }

        public OwnerSettings GetByOwnerId(int ownerId)
        {
            List<OwnerSettings> ownerSettings = _serializer.FromCSV(FilePath);
            foreach (var settings in ownerSettings)
            {
                if (settings.OwnerId == ownerId)
                {
                    return settings;
                }
            }

            return  null;
        }

        public void Update(OwnerSettings ownerSettings)
        {
            List<OwnerSettings> allSettings = _serializer.FromCSV(FilePath);
            foreach (var settings in allSettings)
            {
                if (ownerSettings.OwnerId == settings.OwnerId)
                {
                    allSettings.Remove(settings);
                    allSettings.Add(ownerSettings);
                    _serializer.ToCSV(FilePath, allSettings);
                    break;
                }
            }
        }
    }
}
