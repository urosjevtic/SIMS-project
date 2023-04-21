using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface INotesRepository
    {
        List<String> GetByOwnersId(int ownersId);
        Notes Save(Notes notes);

        void AddNewNote(int ownersId, string newNote);
        void Delete(Notes notes);

        void DeleteNote(int ownersId, string note);
    }
}
