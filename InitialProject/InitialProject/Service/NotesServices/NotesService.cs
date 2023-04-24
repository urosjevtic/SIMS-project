using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Service.NotesServices
{
    public class NotesService
    {
        private readonly INotesRepository _notesRepository;

        public NotesService()
        {
            _notesRepository = Injector.Injector.CreateInstance<INotesRepository>();
        }


        public List<string> GetNotesByOwnersId(int ownersId)
        {
            return _notesRepository.GetByOwnersId(ownersId);
        }

        public void AddNewNote(int ownerId, string newNote)
        {
            _notesRepository.AddNewNote(ownerId, newNote);
        }

        public void RemoveNote(int ownerId, string note)
        {
            _notesRepository.DeleteNote(ownerId, note);
        }
    }
}
