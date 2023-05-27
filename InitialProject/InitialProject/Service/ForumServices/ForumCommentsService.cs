using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Forums;
using InitialProject.Domain.RepositoryInterfaces.IForumsRepo;

namespace InitialProject.Service.ForumServices
{
    public class ForumCommentsService
    {
        private readonly IForumCommentRepository _forumCommentRepository;

        public ForumCommentsService()
        {
            _forumCommentRepository = Injector.Injector.CreateInstance<IForumCommentRepository>();
        }


        public List<ForumComment> GetAll()
        {
            return _forumCommentRepository.GetAll();
        }

    }
}
