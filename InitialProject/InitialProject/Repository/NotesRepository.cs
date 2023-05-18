using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    class NotesRepository : INotesRepository
    {

        private const string FilePath = "../../../Resources/Data/ownerNotes.csv";

        private readonly Serializer<Notes> _serializer;

        private List<Notes> _notes;

        public NotesRepository()
        {
            _serializer = new Serializer<Notes>();
            _notes = _serializer.FromCSV(FilePath);
        }

        public Notes Save(Notes notes)
        {
            notes.Id = NextId();
            _notes = _serializer.FromCSV(FilePath);
            _notes.Add(notes);
            _serializer.ToCSV(FilePath, _notes);
            return notes;
        }


        private int NextId()
        {
            _notes = _serializer.FromCSV(FilePath);
            if (_notes.Count < 1)
            {
                return 1;
            }
            return _notes.Max(c => c.Id) + 1;
        }

        public void Delete(Notes notes)
        {
            _notes = _serializer.FromCSV(FilePath);
            Notes founded = _notes.Find(c => c.Id == notes.Id);
            _notes.Remove(founded);
            _serializer.ToCSV(FilePath, _notes);
        }

        public List<string> GetByOwnersId(int ownersId)
        {
            _notes = _serializer.FromCSV(FilePath);
            return _notes.FirstOrDefault(note => note.OwnerId == ownersId).OwnerNotes;
        }

        public void AddNewNote(int ownersId, string newNote)
        {
            _notes = _serializer.FromCSV(FilePath);
            Notes ownerNotes = _notes.FirstOrDefault(note => note.OwnerId == ownersId);
            ownerNotes.OwnerNotes.Add(newNote);
            _serializer.ToCSV(FilePath, _notes);
        }

        public void DeleteNote(int ownersId, string note)
        {
            _notes = _serializer.FromCSV(FilePath);
            Notes ownerNotes = _notes.FirstOrDefault(note => note.OwnerId == ownersId);
            ownerNotes.OwnerNotes.Remove(note);
            _serializer.ToCSV(FilePath, _notes);
        }
    }
}
