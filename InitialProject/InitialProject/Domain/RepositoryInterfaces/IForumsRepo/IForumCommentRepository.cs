using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Forums;

namespace InitialProject.Domain.RepositoryInterfaces.IForumsRepo
{
    public interface IForumCommentRepository
    {
        List<ForumComment> GetAll();
        ForumComment GetById(int id);
        void Update(ForumComment forumComment);
        void Delete(ForumComment forumComment);

        ForumComment Save(ForumComment forumComment);
    }
}
