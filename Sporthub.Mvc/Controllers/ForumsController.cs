using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sporthub.Mvc.ViewData;
using Sporthub.Model;
using Sporthub.Repository;
using Sporthub.Services;
using Sporthub.Utils;
using Sporthub.Model.Enumerators;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Mvc.Controllers
{
    [HandleError]
    public class ForumsController : SporthubController
    {
        private ForumRepository forumRepository = new ForumRepository();
        private ForumService forumService;
        private ThreadRepository threadRepository = new ThreadRepository();
        private ThreadService threadService;
        private PostRepository postRepository = new PostRepository();
        private PostService postService;

        public ActionResult Threads(string forumid)
        {
            forumService = new ForumService(forumRepository);
            threadService = new ThreadService(threadRepository);

            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();

            breadcrumbs.Add(NewBreadcrumb("Home", "/"));
            breadcrumbs.Add(NewBreadcrumb("Forums", "/forums"));

            if (string.IsNullOrEmpty(forumid))
            {
                var viewData = new ForumsListViewData();

                viewData.Forums = new List<Sporthub.Model.Forum>();
                forumService = new ForumService(forumRepository);
                var forums = forumService.GetAll();
                foreach (var forum in forums)
                {
                    var latestPosts = new List<Sporthub.Model.Post>();
                    foreach (var thread in forum.Threads)
                    {
                        if (thread.Posts.Count > 0)
                        {
                            var post = thread.Posts.OrderByDescending(x => x.CreatedDate).Take(1).ToArray();
                            latestPosts.Add(post[0]);
                        }
                    }
                    if (latestPosts.Count > 0)
                    {
                        var latest = latestPosts.OrderByDescending(x => x.CreatedDate).Take(1).ToArray();
                        forum.LastPost = latest[0];
                    }
                    viewData.Forums.Add(forum);
                }

                viewData.Breadcrumbs = breadcrumbs;

                return View("List", viewData);
            }
            else
            {
                var viewData = new ThreadsListViewData();

                viewData.Forum = forumService.Get(int.Parse(forumid));
                breadcrumbs.Add(NewBreadcrumb(viewData.Forum.ForumName, string.Format("/forums/{0}", viewData.Forum.ID)));
                viewData.Breadcrumbs = breadcrumbs;
                //viewData.Threads = threadService.GetAll(int.Parse(forumid));

                return View("Threads", viewData);
            }
        }

        public ActionResult Posts(string forumid, string threadid)
        {
            if (string.IsNullOrEmpty(forumid) || string.IsNullOrEmpty(threadid))
                return RedirectToAction("List", "Forums");

            var viewData = GetPostsViewData(int.Parse(forumid), int.Parse(threadid));

            return View("Posts", viewData);
        }

        public ActionResult NewThread(string forumid)
        {
            int id;

            if (!UserContext.UserIsLoggedIn())
                return RedirectToAction("Index", "Home");

            if (string.IsNullOrEmpty(forumid))
                return RedirectToAction("List", "Forums");

            try {
                id = int.Parse(forumid);
            }
            catch (Exception ex) {
                return RedirectToAction("List", "Forums");
            }

            var viewData = GetForumViewData(id);
            viewData.Breadcrumbs.Add(NewBreadcrumb("New Topic", string.Format("/newthread/{0}", viewData.Forum.ID)));

            return View("NewThread", viewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NewThread(FormCollection form)
        {
            threadService = new ThreadService(threadRepository);
            postService = new PostService(postRepository);

            var viewData = GetForumViewData(int.Parse(form["hidForumId"]));
            viewData.ThreadTitle = form["ThreadTitle"];
            viewData.PostText = form["PostText"];
            int postId = 0;

            try
            {

                var thread = new Sporthub.Model.Thread
                {
                    ForumID = viewData.Forum.ID,
                    Title = viewData.ThreadTitle,
                    CreatedUserID = UserContext.CurrentUser.ID,
                    UpdatedUserID = UserContext.CurrentUser.ID,
                    ResortID = 0,
                    ThreadStatusID = 1,
                    IsAdmin = false,
                    IsSticky = false,
                    IsVisible = true
                };
                int threadId = threadService.Add(thread);

                var post = new Sporthub.Model.Post
                {
                    ThreadID = threadId,
                    PostText = Server.HtmlEncode(viewData.PostText),
                    IsAdmin = false,
                    IsSticky = false,
                    IsVisible = true,
                    PostStatusID = 1,
                    CreatedUserID = UserContext.CurrentUser.ID,
                    UpdatedUserID = UserContext.CurrentUser.ID,
                    ThreadTitle = thread.Title
                };
                postId = postRepository.Add(post);
                Utilities.FeedbackManager.AddFeedback(FeedbackType.Thanks, "<strong>Thanks!</strong> Your topic has been saved");
            }
            catch (Exception ex)
            {
                Utilities.FeedbackManager.AddFeedback(FeedbackType.Error, "Sorry but there was an Error saving your topic");
            }

            if (postId > 0)
                return Redirect(string.Format("/forums/{0}", viewData.Forum.ID));

            return View("NewThread", viewData);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Posts(FormCollection form)
        {
            if (!UserContext.UserIsLoggedIn())
                return RedirectToAction("Index", "Home");

            var viewData = GetPostsViewData(int.Parse(form["hidForumId"]), int.Parse(form["hidThreadId"]));
            postService = new PostService(postRepository);
            int id = 0;

            var post = new Sporthub.Model.Post
            {
                PostText = Server.HtmlEncode(form["post"]),
                ThreadID = viewData.Thread.ID,
                ThreadTitle = viewData.Thread.Title,
                IsAdmin = false,
                IsSticky = false,
                IsVisible = false,
                PostStatusID = 1,
                CreatedUserID = UserContext.CurrentUser.ID,
                UpdatedUserID = UserContext.CurrentUser.ID
            };

            try
            {
                id = postService.Add(post);
            }
            catch (Exception ex)
            {
                Utilities.FeedbackManager.AddFeedback(FeedbackType.Error, "Sorry but there was an Error saving your post");
                viewData.PostText = form["post"];
                return View("Posts", viewData);
            }
            Utilities.FeedbackManager.AddFeedback(FeedbackType.Thanks, "<strong>Thanks!</strong> Your post has been saved");

            string forumid = "";
            string threadid = "";

            return RedirectToAction("Posts", "Forums", new { forumid = viewData.Forum.ID.ToString(), threadid = viewData.Thread.ID.ToString() });
        }

        private ForumViewData GetForumViewData(int forumID)
        {
            forumService = new ForumService(forumRepository);

            var viewData = new ForumViewData();
            viewData.Forum = forumService.Get(forumID);

            var breadcrumbs = new List<Breadcrumb>();
            breadcrumbs.Add(NewBreadcrumb("Home", "/"));
            breadcrumbs.Add(NewBreadcrumb("Forums", "/forums"));
            breadcrumbs.Add(NewBreadcrumb(viewData.Forum.ForumName, string.Format("/forums/{0}", viewData.Forum.ID)));
            viewData.Breadcrumbs = breadcrumbs;

            return viewData;
        }

        private PostsViewData GetPostsViewData(int forumID, int threadID)
        {
            var viewData = new PostsViewData();

            forumService = new ForumService(forumRepository);
            threadService = new ThreadService(threadRepository);
            postService = new PostService(postRepository);

            viewData.Thread = threadService.Get(threadID);
            viewData.Forum = forumService.Get(forumID);
            viewData.Posts = postService.GetAllOldestFirst(threadID);

            var breadcrumbs = new List<Breadcrumb>();
            breadcrumbs.Add(NewBreadcrumb("Home", "/"));
            breadcrumbs.Add(NewBreadcrumb("Forums", "/forums"));
            breadcrumbs.Add(NewBreadcrumb(viewData.Forum.ForumName, string.Format("/forums/{0}", viewData.Forum.ID)));
            breadcrumbs.Add(NewBreadcrumb(viewData.Thread.Title, string.Format("/forums/{0}/thread", viewData.Thread.ID)));
            viewData.Breadcrumbs = breadcrumbs;

            viewData.StartPostNum = 1;
            int cnt = viewData.Posts.Count / 10;
            cnt += ((viewData.Posts.Count % 10) > 0) ? 1 : 0;
            viewData.TotalPageCount = cnt;

            return viewData;
        }

        public ActionResult About()
        {
            return View();
        }

        public Breadcrumb NewBreadcrumb(string name, string url)
        {
            Breadcrumb bc = new Breadcrumb();
            bc.Name = name;
            bc.Url = url;

            return bc;
        }

    }
}
