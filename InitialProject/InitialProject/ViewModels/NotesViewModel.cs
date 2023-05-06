using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Service.NotesServices;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Ratings;
using RelayCommand = GalaSoft.MvvmLight.Command.RelayCommand;

namespace InitialProject.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        private readonly NotesService _notesService;

        private readonly User _logedInUser;
        public ObservableCollection<string> Notes { get; private set; }

        public NotesViewModel(User logedInUser)
        {
            _notesService = new NotesService();
            _logedInUser = logedInUser;

            Notes = new ObservableCollection<string>(_notesService.GetNotesByOwnersId(logedInUser.Id));
        }


        private string _newNote;

        public string NewNote
        {
            get { return _newNote; }
            set
            {
                _newNote = value;
                OnPropertyChanged(nameof(NewNote));
            }
        }

        public ICommand AddNewNoteCommand => new RelayCommand(AddNewNote);

        private void AddNewNote()
        {
            Notes.Add(_newNote);
            _notesService.AddNewNote(_logedInUser.Id, _newNote);
            NewNote = "";
        }

        public ICommand CloseNotesCommand => new RelayCommand(CloseNotes);

        private void CloseNotes()
        {
            CloseCurrentWindow();
        }

        public ICommand RemoveNoteCommand => new RelayCommandWithParams(RemoveNote);

        private void RemoveNote(object parameter)
        {
            if (parameter is string selectedNote)
            {
                Notes.Remove(selectedNote);
                _notesService.RemoveNote(_logedInUser.Id, selectedNote);
            }
        }
    }
}
