using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Sporthub.Data;
using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Services;
using Sporthub.Repository;
using Sporthub.Mvc.ViewData;
using System.Configuration;

namespace Sporthub.Mvc.Controllers
{
    public class ReviewController : SporthubController
    {
        private readonly ResortRepository _resortRepository = new ResortRepository();
        private ResortService _resortService;
        private bool _increaseReviewedCount = false;

        public Breadcrumb NewBreadcrumb(string name, string url)
        {
            return new Breadcrumb { Name = name, Url = url };
        }

        public ActionResult Edit(string id, string ReturnUrl)
        {
            if (!UserContext.UserIsLoggedIn())
            {
                if (SessionContext.CurrentSession == null)
                {
                    SessionContext.CurrentSession = new Session();
                }
                SessionContext.CurrentSession.IsRedirectedToLogin = true;
                SessionContext.CurrentSession.ReturnUrl = "";//TODO:
                Response.Redirect("/account/login");
            }

            var viewData = GetViewData(id, ReturnUrl);

            return View("Edit", viewData);
        }

        public ResortRatingsViewData GetViewData(string id, string ReturnUrl)
        {
            var db = new Sporthub.Repository.DataAccess.SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            var viewData = new ResortRatingsViewData();
            _resortService = new ResortService(_resortRepository);
            viewData.Resort = _resortService.Get(id);
            var country = LocationDataManager.GetCountryByID(viewData.Resort.CountryID.ToString());
            var continent = country.Continent;

            viewData.ReturnUrl = ReturnUrl;

            var userId = (UserContext.UserIsLoggedIn()) ? UserContext.CurrentUser.ID : 0;

            //viewData.CurrentUserRatings.LiftRating = (viewData.CurrentUserRatings.LiftRating == null) ? 0 : viewData.CurrentUserRatings.LiftRating;
            //viewData.CurrentUserRatings.SnowRating = (viewData.CurrentUserRatings.SnowRating == null) ? 0 : viewData.CurrentUserRatings.SnowRating;
            //viewData.CurrentUserRatings.QueueRating = (viewData.CurrentUserRatings.QueueRating == null) ? 0 : viewData.CurrentUserRatings.QueueRating;
            //viewData.CurrentUserRatings.SceneryRating = (viewData.CurrentUserRatings.SceneryRating == null) ? 0 : viewData.CurrentUserRatings.SceneryRating;
            //viewData.CurrentUserRatings.ConvenienceRating = (viewData.CurrentUserRatings.ConvenienceRating == null) ? 0 : viewData.CurrentUserRatings.ConvenienceRating;
            //viewData.CurrentUserRatings.AccomodationRating = (viewData.CurrentUserRatings.AccomodationRating == null) ? 0 : viewData.CurrentUserRatings.AccomodationRating;
            //viewData.CurrentUserRatings.FoodRating = (viewData.CurrentUserRatings.FoodRating == null) ? 0 : viewData.CurrentUserRatings.FoodRating;
            //viewData.CurrentUserRatings.FacilitiesRating = (viewData.CurrentUserRatings.FacilitiesRating == null) ? 0 : viewData.CurrentUserRatings.FacilitiesRating;
            //viewData.CurrentUserRatings.NightlifeRating = (viewData.CurrentUserRatings.NightlifeRating == null) ? 0 : viewData.CurrentUserRatings.NightlifeRating;

            viewData.IsUpdate = true;
            viewData.CurrentUserRatings = GetUserResortRatings(userId, viewData.Resort.ID, db);
            if (viewData.CurrentUserRatings == null)
            {
                viewData.IsUpdate = false;
                viewData.CurrentUserRatings = new LinkResortUser();
            }

            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb(string.Format("{0} Review", ((viewData.IsUpdate) ? "Edit" : "Create")),
                             string.Format("/reviews/{0}", viewData.Resort.PrettyUrl))
            };
            viewData.Breadcrumbs = breadcrumbs;

            //check FB permissions
            //facebook.API api = new facebook.API()
            //{
            //    ApplicationKey = FacebookConnectAuthentication.ApiKey,
            //    Secret = FacebookConnectAuthentication.SecretKey,
            //    SessionKey = FacebookConnectAuthentication.SessionKey
            //};
            //viewData.HasFacebookPublishPermission = api.users.hasAppPermission(facebook.Types.Enums.Extended_Permissions.publish_stream);
            
            return viewData;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection form)
        {
            //facebook.API api = new facebook.API()
            //{
            //    ApplicationKey = FacebookConnectAuthentication.ApiKey,
            //    Secret = FacebookConnectAuthentication.SecretKey,
            //    SessionKey = FacebookConnectAuthentication.SessionKey
            //};
            //bool hasFacebookPublishPermission = api.users.hasAppPermission(facebook.Types.Enums.Extended_Permissions.publish_stream);
            var hasFacebookPublishPermission = false;

            var reviewId = 0;
            var overallRating = 0;
            var reviewTitle = string.Empty;
            var db = new Sporthub.Repository.DataAccess.SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            _resortService = new ResortService(_resortRepository);
            var resortID = ParseInt(form["hidResortID"]);
            var returnUrl = form["hidReturnUrl"];
            var prettyUrl = form["hidPrettyUrl"];
            var originalLinkResortUser = new LinkResortUser();
            var newLinkResortUser = new Sporthub.Repository.DataAccess.LinkResortUser();
            var increaseFaveCount = 0;
            var increaseVisitCount = 0;

            var viewData = GetViewData(prettyUrl, returnUrl);
            if (viewData.CurrentUserRatings != null)
                originalLinkResortUser = viewData.CurrentUserRatings;


            var userID = (UserContext.UserIsLoggedIn()) ? UserContext.CurrentUser.ID : 0;

            viewData.ErrorList = ValidateForm(form);

            if (!viewData.IsUpdate)
            {
                var insertLinkResortUser = new Sporthub.Repository.DataAccess.LinkResortUser
                {
                    UserID = userID,
                    ResortID = resortID,
                    Score = ParseInt(form["hid_0"]),
                    IsFavourite = (form["fave"] == "1") ? true : false,
                    HasVisited = true,
                    LiftRating = ParseInt(form["hid_1"]),
                    QueueRating = ParseInt(form["hid_2"]),
                    ConvenienceRating = ParseInt(form["hid_3"]),
                    AccomodationRating = ParseInt(form["hid_4"]),
                    FoodRating = ParseInt(form["hid_5"]),
                    SnowRating = ParseInt(form["hid_6"]),
                    SceneryRating = ParseInt(form["hid_7"]),
                    FacilitiesRating = ParseInt(form["hid_8"]),
                    NightlifeRating = ParseInt(form["hid_9"]),
                    Title = Server.HtmlEncode(form["title"]),
                    ReviewText = Server.HtmlEncode(form["review"]),
                    LastVisitDate =
                       string.Format("{0}-{1}", form["dob_y"], form["dob_m"]),
                    ResortSuitsExpert =
                       ((ResortSuits) (ParseInt(form["suits_ability"])) ==
                        ResortSuits.Expert)
                           ? 1
                           : 0,
                    ResortSuitsAdvanced =
                       ((ResortSuits) (ParseInt(form["suits_ability"])) ==
                        ResortSuits.Advanced)
                           ? 1
                           : 0,
                    ResortSuitsIntermediate =
                       ((ResortSuits) (ParseInt(form["suits_ability"])) ==
                        ResortSuits.Intermediate)
                           ? 1
                           : 0,
                    ResortSuitsBeginner =
                       ((ResortSuits) (ParseInt(form["suits_ability"])) ==
                        ResortSuits.Beginner)
                           ? 1
                           : 0,
                    ResortSuitsLively =
                       ((ResortSuits) (ParseInt(form["suits_nightlife"])) ==
                        ResortSuits.Lively)
                           ? 1
                           : 0,
                    ResortSuitsAverage =
                       ((ResortSuits) (ParseInt(form["suits_nightlife"])) ==
                        ResortSuits.Average)
                           ? 1
                           : 0,
                    ResortSuitsQuiet =
                       ((ResortSuits) (ParseInt(form["suits_nightlife"])) ==
                        ResortSuits.Quiet)
                           ? 1
                           : 0,
                    ResortSuitsSkiers =
                       ((ResortSuits) (ParseInt(form["suits_terrian"])) ==
                        ResortSuits.Skiers)
                           ? 1
                           : 0,
                    ResortSuitsSnowboarders =
                       ((ResortSuits) (ParseInt(form["suits_terrian"])) ==
                        ResortSuits.Snowboarders)
                           ? 1
                           : 0,
                    ResortSuitsBoth =
                       ((ResortSuits) (ParseInt(form["suits_terrian"])) ==
                        ResortSuits.Both)
                           ? 1
                           : 0,
                    ResortSuitsAffordable =
                       ((ResortSuits) (ParseInt(form["suits_expense"])) ==
                        ResortSuits.Affordable)
                           ? 1
                           : 0,
                    ResortSuitsCheap =
                       ((ResortSuits) (ParseInt(form["suits_expense"])) ==
                        ResortSuits.Cheap)
                           ? 1
                           : 0,
                    ResortSuitsExpensive =
                       ((ResortSuits) (ParseInt(form["suits_expense"])) ==
                        ResortSuits.Expensive)
                           ? 1
                           : 0,
                    CreatedDate = DateTime.Now,
                    CreatedUserID = UserContext.CurrentUser.ID,
                    UpdatedDate = DateTime.Now,
                    UpdatedUserID = UserContext.CurrentUser.ID
                };

                overallRating = insertLinkResortUser.Score;
                reviewTitle = insertLinkResortUser.Title;

                if (viewData.ErrorList.IsError)
                {
                    var html = viewData.ErrorList.Errors.First<ErrorItem>().Message;
                    Utilities.FeedbackManager.AddFeedback(FeedbackType.Error, html);
                    viewData.CurrentUserRatings = ConvertToModel(insertLinkResortUser);
                    return View("Edit", viewData);
                }

                db.LinkResortUsers.InsertOnSubmit(insertLinkResortUser);

                try
                {
                    db.SubmitChanges();
                    reviewId = insertLinkResortUser.ID;
                    newLinkResortUser = insertLinkResortUser;
                    //TODO: this is DIRE the way all of this is hadled - sort it out!
                    if (insertLinkResortUser.IsFavourite)
                        increaseFaveCount = 1;
                    increaseVisitCount = 1;
                    originalLinkResortUser.Score = 0;
                    originalLinkResortUser.LiftRating = 0;
                    originalLinkResortUser.QueueRating = 0;
                    originalLinkResortUser.ConvenienceRating = 0;
                    originalLinkResortUser.AccomodationRating = 0;
                    originalLinkResortUser.FoodRating = 0;
                    originalLinkResortUser.SnowRating = 0;
                    originalLinkResortUser.SceneryRating = 0;
                    originalLinkResortUser.FacilitiesRating = 0;
                    originalLinkResortUser.NightlifeRating = 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var linkResortUserToUpdate = (from lru in db.LinkResortUsers
                                              where lru.ResortID == resortID && lru.UserID == userID
                                              select lru).SingleOrDefault();
                //originalLinkResortUser = linkResortUserToUpdate;

                //TODO: this needs to be handled better - this is to cater for a fave being added without a rating
                if (linkResortUserToUpdate.Score == 0)
                    _increaseReviewedCount = true;

                linkResortUserToUpdate.Score = ParseInt(form["hid_0"]);
                linkResortUserToUpdate.IsFavourite = ((form["fave"] == "1") ? true : false);
                linkResortUserToUpdate.HasVisited = true;

                linkResortUserToUpdate.LiftRating = ParseInt(form["hid_1"]);
                linkResortUserToUpdate.QueueRating = ParseInt(form["hid_2"]);
                linkResortUserToUpdate.ConvenienceRating = ParseInt(form["hid_3"]);
                linkResortUserToUpdate.AccomodationRating = ParseInt(form["hid_4"]);
                linkResortUserToUpdate.FoodRating = ParseInt(form["hid_5"]);
                linkResortUserToUpdate.SnowRating = ParseInt(form["hid_6"]);
                linkResortUserToUpdate.SceneryRating = ParseInt(form["hid_7"]);
                linkResortUserToUpdate.FacilitiesRating = ParseInt(form["hid_8"]);
                linkResortUserToUpdate.NightlifeRating = ParseInt(form["hid_9"]);

                linkResortUserToUpdate.Title = Server.HtmlEncode(form["title"]);
                linkResortUserToUpdate.ReviewText = Server.HtmlEncode(form["review"]);
                linkResortUserToUpdate.LastVisitDate = string.Format("{0}-{1}", form["dob_y"], form["dob_m"]);

                linkResortUserToUpdate.ResortSuitsExpert = ((ResortSuits)(ParseInt(form["suits_ability"])) == ResortSuits.Expert) ? 1 : 0;
                linkResortUserToUpdate.ResortSuitsAdvanced = ((ResortSuits)(ParseInt(form["suits_ability"])) == ResortSuits.Advanced) ? 1 : 0;
                linkResortUserToUpdate.ResortSuitsIntermediate = ((ResortSuits)(ParseInt(form["suits_ability"])) == ResortSuits.Intermediate) ? 1 : 0;
                linkResortUserToUpdate.ResortSuitsBeginner = ((ResortSuits)(ParseInt(form["suits_ability"])) == ResortSuits.Beginner) ? 1 : 0;

                linkResortUserToUpdate.ResortSuitsLively = ((ResortSuits)(ParseInt(form["suits_nightlife"])) == ResortSuits.Lively) ? 1 : 0;
                linkResortUserToUpdate.ResortSuitsAverage = ((ResortSuits)(ParseInt(form["suits_nightlife"])) == ResortSuits.Average) ? 1 : 0;
                linkResortUserToUpdate.ResortSuitsQuiet = ((ResortSuits)(ParseInt(form["suits_nightlife"])) == ResortSuits.Quiet) ? 1 : 0;

                linkResortUserToUpdate.ResortSuitsSkiers = ((ResortSuits)(ParseInt(form["suits_terrian"])) == ResortSuits.Skiers) ? 1 : 0;
                linkResortUserToUpdate.ResortSuitsSnowboarders = ((ResortSuits)(ParseInt(form["suits_terrian"])) == ResortSuits.Snowboarders) ? 1 : 0;
                linkResortUserToUpdate.ResortSuitsBoth = ((ResortSuits)(ParseInt(form["suits_terrian"])) == ResortSuits.Both) ? 1 : 0;

                linkResortUserToUpdate.ResortSuitsAffordable = ((ResortSuits)(ParseInt(form["suits_expense"])) == ResortSuits.Affordable) ? 1 : 0;
                linkResortUserToUpdate.ResortSuitsCheap = ((ResortSuits)(ParseInt(form["suits_expense"])) == ResortSuits.Cheap) ? 1 : 0;
                linkResortUserToUpdate.ResortSuitsExpensive = ((ResortSuits)(ParseInt(form["suits_expense"])) == ResortSuits.Expensive) ? 1 : 0;

                linkResortUserToUpdate.UpdatedDate = DateTime.Now;
                linkResortUserToUpdate.UpdatedUserID = UserContext.CurrentUser.ID;
                if (viewData.ErrorList.IsError)
                {
                    var html = viewData.ErrorList.Errors.First<ErrorItem>().Message;
                    Utilities.FeedbackManager.AddFeedback(FeedbackType.Error, html);
                    viewData.CurrentUserRatings = ConvertToModel(linkResortUserToUpdate);
                    return View("Edit", viewData);
                }
                if (originalLinkResortUser.IsFavourite && linkResortUserToUpdate.IsFavourite)
                {
                    increaseFaveCount = 0;
                }
                if (originalLinkResortUser.IsFavourite && !linkResortUserToUpdate.IsFavourite)
                {
                    increaseFaveCount = -1;
                }
                if (!originalLinkResortUser.IsFavourite && linkResortUserToUpdate.IsFavourite)
                {
                    increaseFaveCount = 1;
                }

                overallRating = linkResortUserToUpdate.Score;
                reviewTitle = linkResortUserToUpdate.Title;

                try
                {
                    db.SubmitChanges();
                    reviewId = linkResortUserToUpdate.ID;
                    newLinkResortUser = linkResortUserToUpdate;
                }
                catch (Exception ex)
                {
                    //logger.Error(ex);
                    throw ex;
                }
            }

            //update resort
            var resort = _resortService.Get(resortID);

            resort.ScoreTotal = resort.LinkResortUsers.Sum(x => x.Score);
            resort.ScoreCount = resort.LinkResortUsers.Count(x => x.Score > 0);
            resort.Score = (int)Math.Round(Convert.ToDecimal(resort.ScoreTotal) / Convert.ToDecimal(resort.ScoreCount), 0);

            resort.FavedCount = resort.LinkResortUsers.Where(x => x.IsFavourite).Count();
            resort.VisitedCount = resort.LinkResortUsers.Where(x => x.HasVisited).Count();

            resort.LiftTotal = resort.LinkResortUsers.Sum(x => x.LiftRating);
            resort.LiftCount = resort.LinkResortUsers.Where(x => x.LiftRating > 0).Count();
            resort.LiftRating = (int)Math.Round(Convert.ToDecimal(resort.LiftTotal) / Convert.ToDecimal(resort.LiftCount), 0);

            resort.QueueTotal = resort.LinkResortUsers.Sum(x => x.QueueRating);
            resort.QueueCount = resort.LinkResortUsers.Where(x => x.QueueRating > 0).Count();
            resort.QueueRating = (int)Math.Round(Convert.ToDecimal(resort.QueueTotal) / Convert.ToDecimal(resort.QueueCount), 0);

            resort.ConvenienceTotal = resort.LinkResortUsers.Sum(x => x.ConvenienceRating);
            resort.ConvenienceCount = resort.LinkResortUsers.Where(x => x.ConvenienceRating > 0).Count();
            resort.ConvenienceRating = (int)Math.Round(Convert.ToDecimal(resort.ConvenienceTotal) / Convert.ToDecimal(resort.ConvenienceCount), 0);

            resort.AccomodationTotal = resort.LinkResortUsers.Sum(x => x.AccomodationRating);
            resort.AccomodationCount = resort.LinkResortUsers.Where(x => x.AccomodationRating > 0).Count();
            resort.AccomodationRating = (int)Math.Round(Convert.ToDecimal(resort.AccomodationTotal) / Convert.ToDecimal(resort.AccomodationCount), 0);

            resort.FoodTotal = resort.LinkResortUsers.Sum(x => x.FoodRating);
            resort.FoodCount = resort.LinkResortUsers.Where(x => x.FoodRating > 0).Count();
            resort.FoodRating = (int)Math.Round(Convert.ToDecimal(resort.FoodTotal) / Convert.ToDecimal(resort.FoodCount), 0);

            resort.SnowTotal = resort.LinkResortUsers.Sum(x => x.SnowRating);
            resort.SnowCount = resort.LinkResortUsers.Where(x => x.SnowRating > 0).Count();
            resort.SnowRating = (int)Math.Round(Convert.ToDecimal(resort.SnowTotal) / Convert.ToDecimal(resort.SnowCount), 0);

            resort.SceneryTotal = resort.LinkResortUsers.Sum(x => x.SceneryRating);
            resort.SceneryCount = resort.LinkResortUsers.Where(x => x.SceneryRating > 0).Count();
            resort.SceneryRating = (int)Math.Round(Convert.ToDecimal(resort.SceneryTotal) / Convert.ToDecimal(resort.SceneryCount), 0);

            resort.FacilitiesTotal = resort.LinkResortUsers.Sum(x => x.FacilitiesRating);
            resort.FacilitiesCount = resort.LinkResortUsers.Where(x => x.FacilitiesRating > 0).Count();
            resort.FacilitiesRating = (int)Math.Round(Convert.ToDecimal(resort.FacilitiesTotal) / Convert.ToDecimal(resort.FacilitiesCount), 0);

            resort.ResortSuitsExpert += resort.LinkResortUsers.Where(x => x.ResortSuitsExpert > 0).Count();
            resort.ResortSuitsAdvanced += resort.LinkResortUsers.Where(x => x.ResortSuitsAdvanced > 0).Count();
            resort.ResortSuitsIntermediate += resort.LinkResortUsers.Where(x => x.ResortSuitsIntermediate > 0).Count();
            resort.ResortSuitsBeginner += resort.LinkResortUsers.Where(x => x.ResortSuitsBeginner > 0).Count();

            resort.ResortSuitsLively += resort.LinkResortUsers.Where(x => x.ResortSuitsLively > 0).Count();
            resort.ResortSuitsAverage += resort.LinkResortUsers.Where(x => x.ResortSuitsAverage > 0).Count();
            resort.ResortSuitsQuiet += resort.LinkResortUsers.Where(x => x.ResortSuitsQuiet > 0).Count();

            resort.ResortSuitsSkiers += resort.LinkResortUsers.Where(x => x.ResortSuitsSkiers > 0).Count();
            resort.ResortSuitsSnowboarders += resort.LinkResortUsers.Where(x => x.ResortSuitsSnowboarders > 0).Count();
            resort.ResortSuitsBoth += resort.LinkResortUsers.Where(x => x.ResortSuitsBoth > 0).Count();

            resort.ResortSuitsAffordable += resort.LinkResortUsers.Where(x => x.ResortSuitsAffordable > 0).Count();
            resort.ResortSuitsCheap += resort.LinkResortUsers.Where(x => x.ResortSuitsCheap > 0).Count();
            resort.ResortSuitsExpensive += resort.LinkResortUsers.Where(x => x.ResortSuitsExpensive > 0).Count();

            _resortService.Update(resort);

            viewData.Resort = resort;

            viewData.CurrentUserRatings = GetUserResortRatings(userID, resortID, db);
            viewData.IsUpdate = (viewData.CurrentUserRatings == null) ? false : true;
            viewData.Message = "";

            //publish to Facebook
            //bool isPublish = hasFacebookPublishPermission;
            //if (isPublish)
            //{
            //    isPublish = (form["publish"] == "1") ? true : false;
            //}

            //if (isPublish)
            //{

            //    string name = string.Empty;
            //    string url = string.Empty;
            //    string defaultMediaUrl = string.Empty;
            //    string attachCaption = string.Empty;
            //    string attachDesc = string.Empty;

            //    name = resort.Name;
            //    defaultMediaUrl = "http://www.thesnowhub.com/static/images/fb-snowhub.png";
            //    url = string.Format("http://www.thesnowhub.com/resorts/{0}/reviews/{1}", resort.PrettyUrl, reviewId);
            //    attachCaption = string.Format("{0}, {1}, {2}", resort.Name, resort.CountryName, resort.ContinentName);
            //    attachDesc = string.Format("Have you been to {0} too? Click 'Add a Review' to give your opinion!", resort.Name);

            //    if (!string.IsNullOrEmpty(name))
            //    {
            //        attachment attach = new attachment();
            //        attach.caption = attachCaption;
            //        attach.description = attachDesc;
            //        attach.href = url;
            //        attach.name = string.Format("\"{0}\"", reviewTitle);
            //        attachment_media attach_media = new attachment_media();
            //        attach_media.type = attachment_media_type.image;
            //        attachment_media_image image = new attachment_media_image();
            //        image.type = attachment_media_type.image;
            //        image.href = url;
            //        image.src = defaultMediaUrl;
            //        List<attachment_media> attach_media_list = new List<attachment_media>();
            //        attach_media_list.Add(image);
            //        attach.media = attach_media_list;
            //        attachment_property attach_prop = new attachment_property();
            //        attachment_category attach_cat = new attachment_category();
            //        attach_cat.text = "Snow Sports";
            //        attach_cat.href = "#";
            //        attach_prop.category = attach_cat;
            //        attach_prop.ratings = overallRating + "/100";
            //        attach.properties = attach_prop;
            //        /* action links */
            //        List<action_link> actionlink = new List<action_link>();
            //        action_link al1 = new action_link();
            //        al1.href = url;
            //        al1.text = "Add a Review";
            //        actionlink.Add(al1);

            //        api.stream.publish(string.Format("has reviewed {0} on the Snowhub.", name), attach, actionlink, api.uid.ToString(), 0);
            //    }
            //}


            var html2 = string.Format("<strong style='color: #000;'>Thanks!</strong>&nbsp;Your ratings for {0} have been saved.", viewData.Resort.Name);
            Utilities.FeedbackManager.AddFeedback(FeedbackType.Thanks, html2);

            if (!string.IsNullOrEmpty(viewData.ReturnUrl))
                return Redirect(viewData.ReturnUrl);

            return View("Edit", viewData);
        }

        private int ParseInt(string inStr)
        {
            return string.IsNullOrEmpty(inStr) ? 0 : int.Parse(inStr);
        }

        private static LinkResortUser ConvertToModel(Sporthub.Repository.DataAccess.LinkResortUser lruIn)
        {
            var lruOut = new LinkResortUser
            {
                UserID = lruIn.UserID,
                ResortID = lruIn.ResortID,
                Score = lruIn.Score,
                IsFavourite = lruIn.IsFavourite,
                HasVisited = lruIn.HasVisited,
                LastVisitDate = lruIn.LastVisitDate,
                LiftRating = lruIn.LiftRating,
                QueueRating = lruIn.QueueRating,
                ConvenienceRating = lruIn.ConvenienceRating,
                AccomodationRating = lruIn.AccomodationRating,
                FoodRating = lruIn.FoodRating,
                SnowRating = lruIn.SnowRating,
                SceneryRating = lruIn.SceneryRating,
                FacilitiesRating = lruIn.FacilitiesRating,
                NightlifeRating = lruIn.NightlifeRating,
                Title = lruIn.Title,
                ReviewText = lruIn.ReviewText,
                ResortSuitsExpert = lruIn.ResortSuitsExpert ?? 0,
                ResortSuitsAdvanced = lruIn.ResortSuitsAdvanced ?? 0,
                ResortSuitsIntermediate = lruIn.ResortSuitsIntermediate ?? 0,
                ResortSuitsBeginner = lruIn.ResortSuitsBeginner ?? 0,
                ResortSuitsLively = lruIn.ResortSuitsLively ?? 0,
                ResortSuitsAverage = lruIn.ResortSuitsAverage ?? 0,
                ResortSuitsQuiet = lruIn.ResortSuitsQuiet ?? 0,
                ResortSuitsSkiers = lruIn.ResortSuitsSkiers ?? 0,
                ResortSuitsSnowboarders = lruIn.ResortSuitsSnowboarders ?? 0,
                ResortSuitsBoth = lruIn.ResortSuitsBoth ?? 0,
                ResortSuitsAffordable = lruIn.ResortSuitsAffordable ?? 0,
                ResortSuitsCheap = lruIn.ResortSuitsCheap ?? 0,
                ResortSuitsExpensive = lruIn.ResortSuitsExpensive ?? 0
            };

            return lruOut;
        }

        private int[] UpdateRating(int RatingTotal, int RatingCount, int RatingOrig, int RatingNew, bool IsUpdate)
        {
            //arr = UpdateRating(resort.SnowTotal, resort.SnowCount, originalLinkResortUser.SnowRating, newLinkResortUser.SnowRating, viewData.IsUpdate);
            int[] ratingArray = new int[3];

            RatingTotal -= RatingOrig;
            RatingTotal += RatingNew;
            if ((!IsUpdate) || (_increaseReviewedCount) || RatingOrig == 0)
                RatingCount++;
            ratingArray[0] = RatingTotal;
            ratingArray[1] = RatingCount;
            ratingArray[2] = (int)(RatingTotal / RatingCount);
            
            return ratingArray;
        }

        public ErrorList ValidateForm(FormCollection form)
        {
            var errorList = new ErrorList();
            var isFirst = true;

            //overall score
            if (string.IsNullOrEmpty(form["hid_0"]))
            {
                if (isFirst)
                {
                    errorList = errorList.AddError(errorList, "Oops! There were some problems with your review. Please check the errors below and try again", string.Empty);
                    isFirst = false;
                }
                errorList = errorList.AddError(errorList, "You must add an Overall Score", "hid_0");
            }
            if (string.IsNullOrEmpty(form["title"]))
            {
                errorList = errorList.AddError(errorList, "You must enter a review title", "title");
            }
            //if (string.IsNullOrEmpty(form["review"]))
            //{
            //    errorList = errorList.AddError(errorList, "You must enter a review", "review");
            //}
            if (string.IsNullOrEmpty(form["dob_m"]) || string.IsNullOrEmpty(form["dob_y"]))
            {
                errorList = errorList.AddError(errorList, "You must the date of your last visit", "dob_y");
            }

            return errorList;
        }

        public LinkResortUser GetUserResortRatings(int userID, int resortID, Sporthub.Repository.DataAccess.SporthubDataContext db)
        {
            var userRating = new LinkResortUser();
            userRating = (from lru in db.LinkResortUsers
                          where lru.UserID == userID && lru.ResortID == resortID
                          select new Sporthub.Model.LinkResortUser
                            {
                                ID = lru.ID,
                                ResortID = lru.ResortID,
                                UserID = lru.UserID,
                                Score = lru.Score,
                                HasVisited = lru.HasVisited,
                                IsFavourite = lru.IsFavourite,
                                LastVisitDate = lru.LastVisitDate,
                                Title = lru.Title,
                                ReviewText = lru.ReviewText,
                                LiftRating = lru.LiftRating,
                                SnowRating = lru.SnowRating,
                                QueueRating = lru.QueueRating,
                                SceneryRating = lru.SceneryRating,
                                ConvenienceRating = lru.ConvenienceRating,
                                AccomodationRating = lru.AccomodationRating,
                                FoodRating = lru.FoodRating,
                                FacilitiesRating = lru.FacilitiesRating,
                                NightlifeRating = lru.NightlifeRating,
                                CreatedDate = lru.CreatedDate,
                                CreatedUserID = lru.CreatedUserID,
                                UpdatedDate = lru.UpdatedDate,
                                UpdatedUserID = lru.UpdatedUserID,
                                Resort = null,
                                User =
                                  (from u in db.Users
                                   where u.ID == lru.UserID
                                   select new Sporthub.Model.User
                                   {
                                       ID = u.ID,
                                       UserName = u.UserName
                                   }).SingleOrDefault()
                          }).SingleOrDefault();

            return userRating;
        }

    }
}
