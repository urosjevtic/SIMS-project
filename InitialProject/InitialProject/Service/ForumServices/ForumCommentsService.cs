using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.Domain.RepositoryInterfaces.IForumsRepo;

namespace InitialProject.Service.ForumServices
{
    public class ForumCommentsService
    {
        private readonly IForumCommentRepository _forumCommentRepository;
        private readonly UserService _userService;

        public ForumCommentsService()
        {
            _forumCommentRepository = Injector.Injector.CreateInstance<IForumCommentRepository>();
            _userService = new UserService();
        }


        public List<ForumComment> GetAll()
        {
            List<ForumComment> comments = _forumCommentRepository.GetAll();
            BindUserToComment(comments);
            return comments;
        }

        private void BindUserToComment(List<ForumComment> forumComments)
        {
            List<User> users = _userService.GetAll();
            foreach (var comment in forumComments)
            {
                foreach (var user in users)
                {
                    if (user.Id == comment.Author.Id)
                    {
                        comment.Author = user;
                    }
                }
            }
        }

        public ForumComment Save(User user, string commentText)
        {
            ForumComment comment = new ForumComment();
            comment.Author = user;
            comment.Comment = commentText;
            comment.NumberOfReports = 0;
            return _forumCommentRepository.Save(comment);
        }

        public void Report(ForumComment comment, User reporter)
        {
            comment.NumberOfReports++;
            comment.ReportedBy.Add(reporter);
            _forumCommentRepository.Update(comment);
        }

        public void HasUserReported(User user, List<ForumComment> comments)
        {
            foreach (var comment in comments)
            {
                foreach (var reporter in comment.ReportedBy)
                {
                    if (reporter.Id == user.Id)
                    {
                        comment.HasUserReported = true;
                        break;
                    }
                }
            }
        }

    }
}
