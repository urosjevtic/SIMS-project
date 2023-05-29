using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Users;
using InitialProject.Domain.RepositoryInterfaces.IUsersRepo;

namespace InitialProject.Service.SettingsService
{
    public class OwnerSettingsService
    {

        private readonly IOwnerSettingsRepository _ownerSettingsRepository;

        public OwnerSettingsService()
        {
            _ownerSettingsRepository = Injector.Injector.CreateInstance<IOwnerSettingsRepository>();
        }



        public List<OwnerSettings> GetAll()
        {
            return _ownerSettingsRepository.GetAll();
        }

        public OwnerSettings GetByOwnerId(int ownerId)
        {
            return _ownerSettingsRepository.GetByOwnerId(ownerId);
        }

        public void Update(OwnerSettings ownerSettings)
        {
            _ownerSettingsRepository.Update(ownerSettings);
        }

        public void UpdateLanguage(int ownerId, string language)
        {
            OwnerSettings ownerSettings = _ownerSettingsRepository.GetByOwnerId(ownerId);
            ownerSettings.Language = language;
            _ownerSettingsRepository.Update(ownerSettings);
        }

        public void UpdateTheme(int ownerId, string theme)
        {
            OwnerSettings ownerSettings = _ownerSettingsRepository.GetByOwnerId(ownerId);
            ownerSettings.Theme = theme;
            _ownerSettingsRepository.Update(ownerSettings);
        }
    }
}
