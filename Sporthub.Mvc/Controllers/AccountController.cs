using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using Newtonsoft.Json.Linq;
using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Mvc.Code;
using Sporthub.Services;
using Sporthub.Repository;
using Sporthub.Mvc.ViewData;
using System.Configuration;
using Sporthub.Utilities;
using Sporthub.Repository.DataAccess;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Sporthub.Mvc.Controllers
{
    public class AccountController : SporthubController
    {
        private readonly SporthubDataContext _db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
        private readonly ConfigSportTypeRepository _configSportTypeRepository = new ConfigSportTypeRepository();
        private ConfigSportTypeService _configSportTypeService;
        private readonly UserRepository _userRepository = new UserRepository();
        private UserService _userService;
        private readonly LinkUserSportTypeRepository _linkUserSportTypeRepository = new LinkUserSportTypeRepository();
        private LinkUserSportTypeService _linkUserSportTypeService;
        private readonly ResortRepository _resortRepository = new ResortRepository();
        private ResortService _resortService;
        private readonly ActivityRepository _activityRepository = new ActivityRepository();
        private ActivityService _activityService;

        private readonly Session _session = new Session();

        public ActionResult Profile(string id)
        {
            _userService = new UserService(_userRepository);
            _activityService = new ActivityService(_activityRepository);
            var viewData = new UserViewData
           {
               Session = SessionContext.CurrentSession ?? new Session()
           };

            //if id same as logged-in user then show their profile
            var myId = string.Empty;
            if (UserContext.UserIsLoggedIn()) 
                myId = UserContext.CurrentUser.UserName;

            //if no param and logged-in then show user's profile
            if (string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(myId)) 
                id = myId;

            //if id is still null then kick them out
            if (string.IsNullOrEmpty(id))
                return Redirect("/");

            viewData.User = _userService.Get(id);
            if (id == myId)
            {
                //viewData.User = UserContext.CurrentUser;
                viewData.User.IsAuthUserProfile = true;
            }
            else
            {
                //viewData.User = userService.Get(id);

                if ((string.IsNullOrEmpty(id)) && (viewData.User == null))
                    return RedirectToAction("Index", "Home");
                if (viewData.User == null)
                    return RedirectToAction("Error404", "Site");

                viewData.User.IsAuthUserProfile = false;
            }

            if (viewData.User.LinkResortUsers == null) viewData.User.LinkResortUsers = new List<Sporthub.Model.LinkResortUser>();

            if (viewData.User.IsAuthUserProfile)
            {
                //viewData.Friends = GetFriends();
            }
            //viewData = GetProfilePhoto(viewData);

            //viewData.RecentActivity = activityService.GetAllByUserID(viewData.User.ID, 10);
            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Users", "/user/"),
                NewBreadcrumb(viewData.User.RealName, "/user/" + viewData.User.UserName)
            };
            viewData.Breadcrumbs = breadcrumbs;

            return View("Profile", viewData);
        }

        //private UserViewData GetProfilePhoto(UserViewData viewData)
        //{
        //    if (System.IO.File.Exists(string.Format(Server.MapPath("/Content/Users/{0}.png"), viewData.User.UserName)))
        //    {
        //        viewData.ProfileImageUrl = string.Format("/content/users/{0}.png", viewData.User.UserName);
        //        viewData.HasProfileImage = true;
        //    }
        //    else
        //    {
        //        viewData.ProfileImageUrl = "/static/images/placeholder.png";
        //        viewData.HasProfileImage = false;
        //    }

        //    return viewData;
        //}

        public ActionResult Login()
        {
            return View(new LoginViewData { Email = string.Empty });
        }

        public ActionResult Edit()
        {
            if (!UserContext.UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            var viewData = GetUserViewData(UserContext.CurrentUser);

            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Users", "/users"),
                NewBreadcrumb(viewData.User.RealName, "/users/" + viewData.User.ID),
                NewBreadcrumb("Edit Details", "/user/edit")
            };
            viewData.Breadcrumbs = breadcrumbs;

            return View("EditProfile", viewData);
        }

        public ActionResult AddPicture()
        {
            if (!UserContext.UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            var viewData = GetUserViewData(UserContext.CurrentUser);

            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Users", "/users"),
                NewBreadcrumb(viewData.User.RealName, "/users/" + viewData.User.ID),
                NewBreadcrumb("Edit Details", "/user/edit"),
                NewBreadcrumb("Add Profile Picture", "/user/addpicture")
            };
            viewData.Breadcrumbs = breadcrumbs;

            return View("AddPicture", viewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form)
        {
            if (!UserContext.UserIsLoggedIn())
                return RedirectToAction("Index", "Home");

            var user = UserContext.CurrentUser;
            _userService = new UserService(_userRepository);
            _configSportTypeService = new ConfigSportTypeService(_configSportTypeRepository);
            _linkUserSportTypeService = new LinkUserSportTypeService(_linkUserSportTypeRepository);
            _linkUserSportTypeService.DeleteList(user.LinkUserSportTypes);
            user.LinkUserSportTypes = null;
            user.LinkUserSportTypes = new List<Sporthub.Model.LinkUserSportType>();

            if (form["cb-3"] == "on")//neither
            {
                var sport = new Sporthub.Model.LinkUserSportType
                {
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedUserID = user.ID,
                    Level = 0,
                    Seasons = 0,
                    SportTypeID = 3,
                    UpdatedUserID = user.ID,
                    UserID = user.ID,
                    ConfigSportType = GetConfigSportType(3)
                };
                //linkUserSportTypeService.Add(sport);
                user.LinkUserSportTypes.Add(sport);
            }
            else
            {
                if (form["cb-1"] == "on")//snowboard
                {
                    var sport = new Sporthub.Model.LinkUserSportType
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedUserID = user.ID,
                        Level = int.Parse(form["experience-1"]),
                        //Seasons = int.Parse(form["seasons-1"]),
                        Seasons = 0,
                        SportTypeID = 1,
                        UpdatedUserID = user.ID,
                        UserID = user.ID,
                        ConfigSportType = GetConfigSportType(1)
                    };
                    //linkUserSportTypeService.Add(sport);
                    user.LinkUserSportTypes.Add(sport);
                }
                if (form["cb-2"] == "on")//ski
                {
                    var sport = new Sporthub.Model.LinkUserSportType
                    {
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedUserID = user.ID,
                        Level = int.Parse(form["experience-2"]),
                        //Seasons = int.Parse(form["seasons-2"]),
                        Seasons = 0,
                        SportTypeID = 2,
                        UpdatedUserID = user.ID,
                        UserID = user.ID,
                        ConfigSportType = GetConfigSportType(2)
                    };
                    //linkUserSportTypeService.Add(sport);
                    user.LinkUserSportTypes.Add(sport);
                }
            }

            foreach (Sporthub.Model.LinkUserSportType sport in user.LinkUserSportTypes)
            {
                _linkUserSportTypeService.Add(sport);
            }

            user.RealName = form["RealName"].ToString();
            user.Sex = char.Parse(form["Gender"].ToString());
            user.UsualCity = form["UsualCity"].ToString();
            user.DobDay = form["dob_d"].ToString();
            user.DobMonth = form["dob_m"].ToString();
            user.DobYear = form["dob_y"].ToString();
            user.UsualCountryID = int.Parse(form["Country"]);

            _userService.Update(user);

            UserContext.CurrentUser = _userService.Get(user.ID);

            Utilities.FeedbackManager.AddFeedback(FeedbackType.Success, "Profile updated successfully");

            return RedirectToAction("Profile", "Account");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPic()
        {
            var path = string.Empty;
            var fileName = string.Empty;

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf != null)
                    if (hpf.ContentLength > 0)
                    {
                        var destinationFolder = Server.MapPath("/Content/Users/Orig");
                        fileName = Path.GetFileName(hpf.FileName);
                        path = Path.Combine(destinationFolder, fileName);
                        hpf.SaveAs(path);
                    }
            }

            var resp = new BasicJsonModel
            {
                Flag = true,
                Message = "Profile picture uploaded",
                Value = fileName
            };

            return this.Json(resp);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPicture(FormCollection form)
        {
            var message = string.Empty;
            _userService = new UserService(_userRepository);
            var user = UserContext.CurrentUser;

            try
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength > 0)
                    {
                        if (hpf.ContentLength < 5242880) //5Mb
                        {
                            var destinationFolder = Server.MapPath("/Content/Users/Orig");
                            string fileName = Path.GetFileName(hpf.FileName);
                            string path = Path.Combine(destinationFolder, fileName);
                            hpf.SaveAs(path);

                            var origImg = System.Drawing.Image.FromFile(path);
                            var unit = GraphicsUnit.Pixel;
                            RectangleF bounds = origImg.GetBounds(ref unit);
                            int origWidth = (int)bounds.Width;
                            int origHeight = (int)bounds.Height;
                            int newHeight;
                            int newWidth;
                            int tnHeight = 50;
                            int tnWidth = 50;
                            double divisor;

                            //if (origWidth == origHeight)
                            //{
                            //    newWidth = 200;
                            //    newHeight = 200;
                            //}
                            //else if (origWidth > origHeight)
                            //{
                                newWidth = 200;
                                divisor = Convert.ToDouble(origWidth) / 200;
                                newHeight = Convert.ToInt32(origHeight / divisor);
                            //}
                            //else
                            //{
                            //    newHeight = 200;
                            //    divisor = Convert.ToDouble(origHeight) / 200;
                            //    newWidth = Convert.ToInt32(origWidth / divisor);
                            //}

                            if (newHeight < 401)
                            {
                                var resizedDestinationFolder = Server.MapPath("/Content/Users");

                                System.Drawing.Image.GetThumbnailImageAbort dummyCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                                System.Drawing.Image saveImg = origImg.GetThumbnailImage(newWidth, newHeight, dummyCallBack, IntPtr.Zero);
                                path = Path.Combine(resizedDestinationFolder, string.Format("{0}_lge.png", UserContext.CurrentUser.UserName));
                                saveImg.Save(path, ImageFormat.Png);

                                System.Drawing.Image.GetThumbnailImageAbort dummyCallBack2 = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                                System.Drawing.Image saveImg2 = origImg.GetThumbnailImage(tnWidth, tnHeight, dummyCallBack, IntPtr.Zero);
                                path = Path.Combine(resizedDestinationFolder, string.Format("{0}_sml.png", UserContext.CurrentUser.UserName));
                                saveImg2.Save(path, ImageFormat.Png);
                            }
                            else
                            {
                                message = "Image height is greater than 400 pixels";
                            }
                        }
                        else
                        {
                            message = "File greater than 5Mb";
                        }
                    }
                }
                user.HasProfilePicture = true;
                UserContext.CurrentUser = user;
                _userService.Update(user);
            }
            catch (Exception ex)
            {
                message = "Fatal Error: " + ex.Message;
            }

            if (!string.IsNullOrEmpty(message))
            {
                var viewData = GetUserViewData(UserContext.CurrentUser);

                var breadcrumbs = new List<Breadcrumb>
                {
                    NewBreadcrumb("Home", "/"),
                    NewBreadcrumb("Users", "/users"),
                    NewBreadcrumb(viewData.User.RealName, "/users/" + viewData.User.ID),
                    NewBreadcrumb("Edit Details", "/user/edit"),
                    NewBreadcrumb("Add Profile Picture", "/user/addpicture")
                };
                viewData.Breadcrumbs = breadcrumbs;

                Utilities.FeedbackManager.AddFeedback(FeedbackType.Error, message);
                return View("AddPicture", viewData);
            }

            Utilities.FeedbackManager.AddFeedback(FeedbackType.Success, "Profile Picture updated successfully");
            return Redirect("/user");
            //var resp = new BasicJsonModel
            //{
            //    Flag = true,
            //    Message = "Profile picture uploaded",
            //    Value = fileName
            //};

            //return this.Json(resp);
        }

        //this function is reqd for thumbnail creation
        public bool ThumbnailCallback()
        {
            return false;
        }

        public Model.ConfigSportType GetConfigSportType(int id)
        {
            var st = (from r in _db.ConfigSportTypes
                      where r.ID == id
                     select new Model.ConfigSportType
                     {
                         ID = r.ID,
                         Name = r.Name,
                         Alias = r.Alias,
                         Collective = r.Collective,
                         Description = r.Description,
                         IsOther = r.IsOther,
                         IsSki = r.IsSki,
                         IsSnowboard = r.IsSnowboard,
                         ParentID = r.ParentID,
                         Sequence = r.Sequence,
                         Verb = r.Verb
                     }).SingleOrDefault();

            return st;
        }

        public ActionResult Friends()
        {
            if (!UserContext.UserIsLoggedIn())
                return RedirectToAction("Index", "Home");

            var viewData = GetUserViewData(UserContext.CurrentUser);
            //viewData.Friends = GetFriends();


            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();
            breadcrumbs.Add(NewBreadcrumb("Home", "/"));
            breadcrumbs.Add(NewBreadcrumb("Users", "/users"));
            breadcrumbs.Add(NewBreadcrumb(viewData.User.RealName, "/users/" + viewData.User.ID));
            breadcrumbs.Add(NewBreadcrumb("Friends", "/user/friends"));
            viewData.Breadcrumbs = breadcrumbs;

            return View("Friends", viewData);
        }

        //private List<Sporthub.Model.User> GetFriends()
        //{
        //    var friends = new List<Sporthub.Model.User>();
        //    var api = new API()
        //    {
        //        ApplicationKey = FacebookConnectAuthentication.ApiKey,
        //        Secret = FacebookConnectAuthentication.SecretKey,
        //        SessionKey = FacebookConnectAuthentication.SessionKey,
        //        uid = long.Parse(UserContext.CurrentUser.FacebookUid)
        //    };

        //    var db = new FacebookDataContext(api);

        //    var friendIDs = from friend in db.friend_info
        //                    where friend.uid1 == api.uid
        //                    select friend.uid2;
        //    var friendDetails = (from user in db.user
        //                        where friendIDs.Contains(user.uid)
        //                        select user).ToList();

        //    //var userList = new List<user>();
        //    //userList.Add(new user { uid = 1361562085 });
        //    //userList.Add(new user { uid = 100000468646138 });
        //    //userList.Add(new user { uid = 533493250 });

        //    //var uidList = from u in userList select u.uid;

        //    //var facebookUsers = (from user in db.user where uidList.Contains(user.uid) select user).ToList();
        //    //foreach (user usr in facebookUsers)
        //    foreach (user usr in friendDetails)
        //    {
        //        if (usr.is_app_user ?? false)
        //        {
        //            var friend = new Sporthub.Model.User
        //            {
        //                RealName = string.Format("{0} {1}", usr.first_name, usr.last_name),
        //                FacebookUid = usr.uid.ToString()
        //            };
        //            friends.Add(friend);
        //        }
        //    }

        //    return friends;
        //}

        public ActionResult EditResorts()
        {
            return RedirectToAction("Index", "Home");
            //if (!UserContext.UserIsLoggedIn())
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //var viewData = GetUserViewData(UserContext.CurrentUser);

            //List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();
            //breadcrumbs.Add(NewBreadcrumb("Home", "/"));
            //breadcrumbs.Add(NewBreadcrumb("Users", "/users"));
            //breadcrumbs.Add(NewBreadcrumb(viewData.User.RealName, "/users/" + viewData.User.ID));
            //breadcrumbs.Add(NewBreadcrumb("Edit", "/user/edit"));
            //breadcrumbs.Add(NewBreadcrumb("Manage Resorts", "/user/resorts"));
            //viewData.Breadcrumbs = breadcrumbs;

            //return View("EditUserResorts", viewData);
        }

        public ActionResult InviteFriends()
        {
            if (!UserContext.UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            var viewData = GetUserViewData(UserContext.CurrentUser);
            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Users", "/users"),
                NewBreadcrumb(viewData.User.RealName, "/users/" + viewData.User.ID),
                NewBreadcrumb("Invite Friends", "/user/invite")
            };
            viewData.Breadcrumbs = breadcrumbs;

            return View("InviteFriends", viewData);
        }

        private UserViewData GetUserViewData(Sporthub.Model.User user)
        {
            var viewData = new UserViewData
            {
                Session = SessionContext.CurrentSession ?? new Session()
            };

            viewData.User = user;
            //viewData = GetProfilePhoto(viewData);
            Sporthub.Repository.DataAccess.SporthubDataContext db = new Sporthub.Repository.DataAccess.SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

            viewData.Countries = new List<Sporthub.Model.Country>();
            var countries = (from c in db.Countries
                             orderby c.CountryName
                             select new { c.ID, c.CountryName }).ToList();

            foreach (var c in countries)
            {
                var country = new Sporthub.Model.Country {ID = c.ID, CountryName = c.CountryName};
                viewData.Countries.Add(country);
            }

            viewData.Sports = new List<Sporthub.Model.ConfigSportType>();
            var sports = (from c in db.ConfigSportTypes
                             orderby c.Sequence
                             select c).ToList();

            foreach (var s in sports)
            {
                var sport = new Sporthub.Model.ConfigSportType
                {
                    ID = s.ID,
                    Alias = s.Alias,
                    Collective = s.Collective,
                    Description = s.Description,
                    IsOther = s.IsOther,
                    IsSki = s.IsSki,
                    IsSnowboard = s.IsSnowboard,
                    Name = s.Name,
                    ParentID = s.ParentID,
                    Verb = s.Verb,
                    Sequence = s.Sequence
                    
                };

                viewData.Sports.Add(sport);
            }

            return viewData;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(FormCollection form)
        {
            _userService = new UserService(_userRepository);

            var email = Request.Form["Email"].ToString();
            var password = Request.Form["Password"].ToString();
            var persist = Request.Form["Persist"].ToString().Contains("true");
            var returnUrl = (Request.QueryString["ReturnUrl"] != null) 
                ? Request.QueryString["ReturnUrl"].ToString() 
                : string.Empty;
            var user = _userService.GetByEmail(email);


            if (user != null && user.Password == password)
            {
                if (user.SignupStageReachedID == 1)
                {
                    var vd = new UserViewData
                    {
                        ProfileImageUrl = "",
                        User = user,
                        Message = "",
                        Breadcrumbs = null
                    };
                    return View("NotActivated", vd);
                }
                //FormsAuthentication.SetAuthCookie(userName, Convert.ToBoolean(Request.Form["Password"].ToString()));
                //                UserContext.SetUser(user);
                FormsAuthentication.SetAuthCookie(user.UserName, persist);
                SetUserCookie(user.UserName);

                user.LastVisitDate = DateTime.Now;
                _userService.Update(user);

                UserContext.CurrentUser = user;

                Response.Redirect(!string.IsNullOrEmpty(returnUrl) ? returnUrl : FormsAuthentication.DefaultUrl);
            }

            var viewData = new LoginViewData
            {
                Email = email
            };

            return View(viewData);
        }

        public void SetUserCookie(string name)
        {
            Response.Cookies.Add(new HttpCookie("sh-user") {Value = name});
        }

        //public ActionResult NotActivated()
        //{
        //    var viewData = new UserViewData
        //    {
        //        ProfileImageUrl = "",
        //        User = null,
        //        Message = "",
        //        Breadcrumbs = null
        //    };

        //    return View("notactivated", viewData);
        //}

        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();

        //    return RedirectToAction("Index", "Home");
        //}

        public ActionResult Create()
        {
            var viewData = new UserViewData
            {
                User = new Sporthub.Model.User()
            };

            return View("Create", viewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            _userService = new UserService(_userRepository);

            var user = new Sporthub.Model.User
            {
                Email = form["Email"].ToString(),
                UserName = form["UserName"].ToString(),
                Password = form["Password"].ToString(),
                SignupStageReachedID = 1,
                UserRoleID = 2
            };

            var id = _userService.Add(user);

            var emailService = new EmailService();
            var message = string.Empty;
            try
            {
                bool useSmtpPickup = HttpContext.Request.Url.Host.ToLower().Contains("localhost");
                emailService.Activation(user, id, Utilities.Encryption.EncryptEmail(user.Email), useSmtpPickup);
                message = "Email Sent OK";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            var viewData = new UserViewData
            {
                ProfileImageUrl = "",
                User = user,
                Message = "",
                Breadcrumbs = null
            };
            Session["UserEmail"] = user.Email;

            return Redirect("/registersuccess");
        }

        public ActionResult RegisterSuccess()
        {
            var viewData = new RegisterSucessViewData
            {
                EmailAddress = Session["UserEmail"] as string,
            };

            return View("RegisterSuccess", viewData);
        }


        public ActionResult Activate()
        {
            string[] qs = Request.QueryString[0].Split('/');
            bool isOK = true;
            int id = 0;
            string activationCode = string.Empty;

            //validate querystring
            try { id = int.Parse(qs[0]); }
            catch { isOK = false; }
            try { activationCode = qs[1]; }
            catch { isOK = false; }
            if (string.IsNullOrEmpty(activationCode)) { isOK = false; }

            if (isOK)
            {
                _userService = new UserService(_userRepository);
                var user = _userService.Get(id);
                var activationCodeCompare = Utilities.Encryption.EncryptEmail(user.Email);

                if (activationCode == activationCodeCompare)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);

                    user.SignupStageReachedID = 2;//activated
                    user.LastVisitDate = DateTime.Now;
                    _userService.Update(user);

                    UserContext.CurrentUser = user;
                    _session.IsRedirectedsFromActivation = true;
                    SessionContext.CurrentSession = _session;

                    var emailService = new EmailService();
                    var message = string.Empty;
                    try
                    {
                        bool useSmtpPickup = HttpContext.Request.Url.Host.ToLower().Contains("localhost");
                        emailService.Welcome(user, useSmtpPickup);
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                    }


                    return RedirectToAction("profile");
                }
            }

            return RedirectToAction("invalidcode");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult InvalidCode()
        {
            var viewData = new UserViewData
            {
                ProfileImageUrl = "",
                User = null,
                Message = "",
                Breadcrumbs = null
            };

            return View("invalidcode", viewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InvalidCode(FormCollection form)
        {
            _userService = new UserService(_userRepository);
            var user = _userService.GetByEmail(form["Email"]);

            var emailService = new EmailService();
            var message = string.Empty;
            try
            {
                bool useSmtpPickup = HttpContext.Request.Url.Host.ToLower().Contains("localhost");
                emailService.Activation(user, user.ID, Utilities.Encryption.EncryptEmail(user.Email), useSmtpPickup);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            var viewData = new UserViewData
            {
                ProfileImageUrl = "",
                User = user,
                Message = "",
                Breadcrumbs = null
            };

            return View("resentactivation", viewData);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ForgotPassword()
        {
            var viewData = new UserViewData
            {
                ProfileImageUrl = "",
                User = null,
                Message = "",
                Breadcrumbs = null
            };

            return View("forgotpassword", viewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ForgotPassword(FormCollection form)
        {
            _userService = new UserService(_userRepository);
            var user = _userService.GetByEmail(form["Email"]);

            var emailService = new EmailService();
            var message = string.Empty;
            try
            {
                bool useSmtpPickup = HttpContext.Request.Url.Host.ToLower().Contains("localhost");
                emailService.Welcome(user, useSmtpPickup);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            var viewData = new LoginViewData
            {
                Email = user.Email,
                Message = string.Format("We have sent your password to <strong>{0}</strong>. If there is a delay please check your spam folder.", user.Email),
            };

            return View("login", viewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResendEmail(FormCollection form)
        {
            _userService = new UserService(_userRepository);
            var user = _userService.GetByEmail(form["Email"]);

            var emailService = new EmailService();
            var message = string.Empty;
            try
            {
                bool useSmtpPickup = HttpContext.Request.Url.Host.ToLower().Contains("localhost");
                emailService.Activation(user, user.ID, Utilities.Encryption.EncryptEmail(user.Email), useSmtpPickup);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            var viewData = new UserViewData
            {
                ProfileImageUrl = "",
                User = user,
                Message = "",
                Breadcrumbs = null
            };

            return View("resentactivation", viewData);
        }

        public JsonResult UpdateFaveResort(int resortID)
        {
            var db = new Sporthub.Repository.DataAccess.SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

            var isSuccess = false;
            var isAuthenticated = false;
            var isRemove = false;
            var errorMessage = string.Empty;

            if (UserContext.UserIsLoggedIn())
            {
                isAuthenticated = true;

                _resortService = new ResortService(_resortRepository);

                var linkResortUserToUpdate = (from lru in db.LinkResortUsers
                                              where lru.ResortID == resortID && lru.UserID == UserContext.CurrentUser.ID
                                              select lru).SingleOrDefault();

                if (linkResortUserToUpdate == null)
                {
                    var insertLinkResortUser = new Sporthub.Repository.DataAccess.LinkResortUser
                    {
                        UserID = UserContext.CurrentUser.ID,
                        ResortID = resortID,
                        Score = 0,
                        IsFavourite = true,
                        CreatedDate = DateTime.Now,
                        CreatedUserID = UserContext.CurrentUser.ID,
                        UpdatedDate = DateTime.Now,
                        UpdatedUserID = UserContext.CurrentUser.ID
                    };

                    db.LinkResortUsers.InsertOnSubmit(insertLinkResortUser);

                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    if (linkResortUserToUpdate.IsFavourite)
                    {
                        isRemove = true;
                        linkResortUserToUpdate.IsFavourite = false;
                    }
                    else
                    {
                        linkResortUserToUpdate.IsFavourite = true;
                    }
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        //logger.Error(ex);
                        throw ex;
                    }
                }

                //update resort
                var resort = _resortService.Get(resortID);
                resort.FavedCount = resort.LinkResortUsers.Where(x => x.IsFavourite).Count();
                _resortService.Update(resort);


                isSuccess = true;
            }
            else
            {
                errorMessage = "You must be logged-in to do that";
            }

            return this.Json(new
            {
                Result = isSuccess,
                IsAuthenticated = isAuthenticated,
                ErrorMessage = errorMessage,
                IsRemove = isRemove
            },
            JsonRequestBehavior.AllowGet);
        }

        //public JsonResult Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    SessionContext.CurrentSession = null;
        //    UserContext.CurrentUser = null;

        //    return this.Json(new
        //    {
        //        Result = true,
        //        IsAuthenticated = true,
        //        ErrorMessage = "",
        //        IsRemove = false
        //    });
        //}

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            UserContext.CurrentUser = null;
            Response.Cookies.Remove("sh-user");
//            System.Threading.Thread.Sleep(2000);//to cater for removing fb cookie
            SessionContext.CurrentSession = new Session() {IsLogout = true};
            return RedirectToAction("index", "home");
        }

        public JsonResult AddDateVisited(int resortID, string lastVisitDate)
        {
            var db = new Sporthub.Repository.DataAccess.SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

            var isSuccess = false;
            var isAuthenticated = false;
            var isRemove = false;
            var errorMessage = string.Empty;

            if (UserContext.UserIsLoggedIn())
            {
                isAuthenticated = true;

                _resortService = new ResortService(_resortRepository);

                var linkResortUserToUpdate = (from lru in db.LinkResortUsers
                                              where lru.ResortID == resortID && lru.UserID == UserContext.CurrentUser.ID
                                              select lru).SingleOrDefault();

                if (linkResortUserToUpdate == null)
                {
                    var insertLinkResortUser = new Sporthub.Repository.DataAccess.LinkResortUser();
                    insertLinkResortUser.UserID = UserContext.CurrentUser.ID;
                    insertLinkResortUser.ResortID = resortID;
                    insertLinkResortUser.IsFavourite = false;
                    insertLinkResortUser.HasVisited = true;
                    insertLinkResortUser.Score = 0;
                    insertLinkResortUser.LastVisitDate = lastVisitDate;
                    insertLinkResortUser.CreatedDate = DateTime.Now;
                    insertLinkResortUser.CreatedUserID = UserContext.CurrentUser.ID;
                    insertLinkResortUser.UpdatedDate = DateTime.Now;
                    insertLinkResortUser.UpdatedUserID = UserContext.CurrentUser.ID;

                    db.LinkResortUsers.InsertOnSubmit(insertLinkResortUser);

                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    if (linkResortUserToUpdate.HasVisited)
                    {
                        isRemove = true;
                        linkResortUserToUpdate.HasVisited = false;
                    }
                    else
                    {
                        linkResortUserToUpdate.HasVisited = true;
                    }
                    linkResortUserToUpdate.LastVisitDate = lastVisitDate;
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        //logger.Error(ex);
                        throw ex;
                    }
                }

                //update resort
                var resort = _resortService.Get(resortID);
                resort.VisitedCount = resort.LinkResortUsers.Where(x => x.HasVisited).Count();
                _resortService.Update(resort);


                isSuccess = true;
            }
            else
            {
                errorMessage = "You must be logged-in to do that";
            }

            return this.Json(new
            {
                Result = isSuccess,
                IsAuthenticated = isAuthenticated,
                ErrorMessage = errorMessage,
                IsRemove = isRemove
            },
            JsonRequestBehavior.AllowGet);
        }


        //public JsonResult ConnectExistingToFacebook(string facebookUid, string accessToken)
        //{
        //    var isSuccess = false;
        //    var message = string.Empty;
        //    _userService = new UserService(_userRepository);

        //    var checkUser = _userService.GetByFacebookId(facebookUid);
        //    if (checkUser != null)
        //    {
        //        checkUser.FacebookUid = string.Empty;
        //        //TODO: this is probably wrong!!
        //        _userService.Update(checkUser);
        //    }
        //    var facebook = new FacebookGraphAPI(accessToken);
        //    var facebookUser = facebook.GetObject("me", null);
        //    UserContext.CurrentUser.RealName = GetRealName(facebookUser);

        //    UserContext.CurrentUser.FacebookUid = facebookUid;
        //    _userService.Update(UserContext.CurrentUser);
        //    SessionContext.CurrentSession.FacebookAccessToken = accessToken;
        //    isSuccess = true;

        //    return Json(new { Success = isSuccess, Message = message }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult ShowCheckins()
        {
            //var facebook = new FacebookGraphAPI(SessionContext.CurrentSession.FacebookAccessToken);
            //var tmp = facebook.GetObject(UserContext.CurrentUser.FacebookUid+ "/checkins", null);

            TempData["checkins"] = "PARP!!!";
            return View("Checkins", null);
        }

        //private string GetRealName(JObject facebookUser)
        //{
        //    var first_name = facebookUser["first_name"].ToString().Replace("\\", "");
        //    first_name = first_name.Replace("\"", "");
        //    var last_name = facebookUser["last_name"].ToString().Replace("\\", "");
        //    last_name = last_name.Replace("\"", "");
        //    return string.Format("{0} {1}", first_name.Trim(), last_name.Trim());
        //}

        //public JsonResult ConnectNewToFacebook(string facebookUid, string accessToken)
        //{
        //    _userService = new UserService(_userRepository);
        //    var isSuccess = false;
        //    var message = string.Empty;
        //    ////TODO: check they arent already connected to a different account
        //    SessionContext.CurrentSession.FacebookAccessToken = accessToken;
        //    isSuccess = true;

        //    var checkUser = _userService.GetByFacebookId(facebookUid);
        //    if (checkUser != null)
        //    {
        //        UserContext.CurrentUser = checkUser;
        //        FormsAuthentication.SetAuthCookie(checkUser.UserName, true);
        //        SetUserCookie(checkUser.UserName);
        //    }
        //    else
        //    {
        //        var facebook = new FacebookGraphAPI(accessToken);
        //        var facebookUser = facebook.GetObject("me", null);
        //        var email = facebookUser["email"].ToString().Replace("\\", "");
        //        email = email.Replace("\"", "");
        //        var username = GetRandomString();
        //        var sporthubUser = new Sporthub.Model.User
        //        {
        //            Email = email,
        //            FacebookUid = facebookUid,
        //            UserName = username,
        //            RealName = GetRealName(facebookUser),
        //            Password = GetRandomString(),
        //            SignupStageReachedID = 2,
        //            UserRoleID = 2
        //        };

        //        var id = _userService.Add(sporthubUser);
        //        UserContext.CurrentUser = _userService.Get(id);
        //    }
        //    //FormsAuthentication.SetAuthCookie(username, false);)


        //    return Json(new { Success = isSuccess, Message = message }, JsonRequestBehavior.AllowGet);
        //}

        public static string GetRandomString()
        {
            var path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }

        //public ActionResult Activate()
        //{
        //    string[] qs = Request.QueryString[0].Split('/');
        //    bool isOK = true;
        //    int id = 0;
        //    string activationCode = string.Empty;

        //    //validate querystring
        //    try { id = int.Parse(qs[0]); }
        //    catch { isOK = false; }
        //    try { activationCode = qs[1]; }
        //    catch { isOK = false; }
        //    if (string.IsNullOrEmpty(activationCode)) { isOK = false; }

        //    if (isOK)
        //    {
        //        var pressAgent = new PressAgent();
        //        pressAgent = pressAgentService.GetPressAgent(id);

        //        //hash email address
        //        var activationCodeCompare = Utilities.Encryption.EncryptPassword(pressAgent.Email);

        //        //check hashed email addresses match
        //        if (activationCode == activationCodeCompare)
        //        {
        //            //clear old email address & make active
        //            pressAgent.EmailOld = string.Empty;
        //            pressAgent.IsActive = true;
        //            pressAgentService.UpdatePressAgent(pressAgent);

        //            //put press agent in context
        //            PressAgentContext.SetPressAgent(pressAgent);

        //            return RedirectToAction("ActivateSuccess");
        //        }
        //        else
        //        {
        //            return RedirectToAction("InvalidCode");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("InvalidCode");
        //    }
        //}




        /* -----------------------GENERATED BELOW HERE---------------------------------*/
/*
        public ActionResult Register()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string userName, string email, string password, string confirmPassword)
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (ValidateRegistration(userName, email, password, confirmPassword))
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(userName, password, email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuth.SignIn(userName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions result in password not being changed.")]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (!ValidateChangePassword(currentPassword, newPassword, confirmPassword))
            {
                return View();
            }

            try
            {
                if (MembershipService.ChangePassword(User.Identity.Name, currentPassword, newPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                    return View();
                }
            }
            catch
            {
                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                return View();
            }
        }

        public ActionResult ChangePasswordSuccess()
        {

            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        #region Validation Methods

        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }
            if (newPassword == null || newPassword.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            if (!MembershipService.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }
            if (password == null || password.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }
            return ModelState.IsValid;
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }

    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            MembershipUser currentUser = _provider.GetUser(userName, true);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }
    */
    }
}
