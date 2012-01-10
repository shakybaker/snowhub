/* last update - 25.11.2009 15:38 */
$(document).ready(function() {
    sporthub.onLoad();
});

var sporthub = {
    mouseX: 0,
    mouseY: 0,
    isIE: false,
    pageHasNavigation: true,
    layerContent: '#layerContent',
    urlLoadingS: '/Static/Images/Anim/load-s.gif',
    urlLoadingM: '/Static/Images/Anim/loading-m.gif',
    urlLoadingL: '/Static/Images/Anim/loading-l.gif',
    loadingMedium: function() {
        var img = document.createElement('img');
        img.setAttribute('alt', "...");
        img.setAttribute('class', 'loadingImg');
        img.setAttribute('src', sporthub.urlLoadingM);
        return img;
    }, //loadingMedium
    loadingSmall: function() {
        var img = document.createElement('img');
        img.setAttribute('alt', "...");
        img.setAttribute('class', 'loadingImg');
        img.setAttribute('src', sporthub.urlLoadingS);
        return img;
    }, //loadingMedium
    onLoad: function() {

        $.fn.qtip.styles.profileTip = {
            width: 240,
            background: '#7bbbfc',
            color: '#111',
            border: {
                width: 1,
                radius: 6,
                color: '#094989'
            },
            tip: 'bottomMiddle'
        }
        $(".closeFeedback").click(function() {
            $("div.feedback").fadeOut("slow");
        });
        $(".fbLogout").click(function() {
            $.getJSON("/Account/Logout", {});
            FB.Connect.logout(function() {
                $.getJSON("/Account/Logout", {}, function(data) {
                    window.location = "/Home/Index";
                    //window.location.reload();
                });
            });
        });
        if (sporthub.pageHasNavigation) {
            $("input#search").focus();
            sporthub.utility.qTipInit('');
            $('a[rel="external"]').click(sporthub.utility.relExternal);
            $("ul.list1 > li:last-child").addClass("last");
            var options = {
                script: "/Ajax/Search?",
                varname: "text",
                json: true,
                maxresults: 50,
                timeout: 5000,
                callback: sporthub.utility.redirectSearch
            };
            var as = new bsn.AutoSuggest('search', options);
        }
        $().mousemove(function(e) {
            sporthub.mouseX = e.pageX;
            sporthub.mouseY = e.pageY;
        });
        $("ul.list3 li").hover(
            function() {
                $(this).addClass("ovr");
            },
            function() {
                $(this).removeClass("ovr");
            });
    } //onLoad
};

sporthub.resortPages = {
    onLoad: function() {
        $("#markAsVisited").click(function() {
            $(this).addClass("on");

            var offset = $(this).offset();
            $("#visitEntryPopup").css("top", ((offset.top) + 18) + "px");
            $("#visitEntryPopup").css("left", offset.left);
            $("#visitEntryPopup").css("width", "123px");

            $("#visitEntryPopup").show();
        });
        $("#cnclButt").click(function() {
            $("#markAsVisited").removeClass("on");
            $("#visitEntryPopup").hide();
        });
        //        $("a#checkInHere").fancybox({
        //            'hideOnContentClick': false
        //        });
        $("#svButt").click(function() {
            var dt = $("#dob_y").val() + "-" + $("#dob_m").val();
            $("#markAsVisited").removeClass("on");
            $("#visitEntryPopup").hide();
            $("#markAsVisited").html("<img alt='...' src='/Static/Images/Anim/load-s.gif' /> Saving ...");
            $("#markAsVisited").addClass("loading");
            $.getJSON("/Account/AddDateVisited", { resortID: document.getElementById("hidResortID").value, lastVisitDate: dt }, function(data) {
                if (data.IsAuthenticated == true) {
                    $("#markAsVisited").removeClass("loading");
                    if (data.Result == true) {
                        if (data.IsRemove == true) {
                            $("#markAsVisited").removeClass("isVisited");
                            $("#markAsVisited").html("Mark As Visited");
                        }
                        else {
                            $("#markAsVisited").addClass("isVisited");
                            $("#markAsVisited").html("Visited");
                        }
                        window.location.reload();
                    }
                    else {
                        alert(data.ErrorMessage);
                    }
                }
                else {
                    $("#markAsVisited").removeClass("loading");
                    $("#markAsVisited").html("Mark As Visited");
                    alert(data.ErrorMessage);
                }
            });
        });
        $("#addAsFave").click(function() {
            $("#addAsFave").html("<img alt='...' src='/Static/Images/Anim/load-s.gif' /> Saving ...");
            $("#addAsFave").addClass("loading");
            $.getJSON("/Account/UpdateFaveResort", { resortID: document.getElementById("hidResortID").value }, function(data) {
                if (data.IsAuthenticated == true) {
                    $("#addAsFave").removeClass("loading");
                    if (data.Result == true) {
                        if (data.IsRemove == true) {
                            $("#addAsFave").removeClass("isFave");
                            $("#addAsFave").html("<span></span>Add as Favourite");
                        }
                        else {
                            $("#addAsFave").addClass("isFave");
                            $("#addAsFave").html("<span></span>A Favourite");
                        }
                        window.location.reload();
                    }
                    else {
                        alert(data.ErrorMessage);
                    }
                }
                else {
                    $("#addAsFave").removeClass("loading");
                    $("#addAsFave").html("<span></span>Add as Favourite");
                    alert(data.ErrorMessage);
                }
            });
        });
    }
};

sporthub.reviews = {
    onLoad: function() {
        $(".review").hover(
            function() {
                $(this).addClass("ovr");
            },
            function() {
                $(this).removeClass("ovr");
            });
    }
};

sporthub.users = {
    popTip: function(id) {
        var a = id.split('_');
        var html = "<div class=\"profileSummaryIn\"><span class=\"fb-n533493250\" id=\"l1-1-n_533493250\">Mark Baker</span><br /><em>Snowboarder, Skier</em><br/><a href=\"/user/" + a + "\" class=\"smlbutt\">Snowhub Profile</a> <a target=\"_blank\" href=\"http://www.facebook.com/profile.php?id=" + a + "\" class=\"smlbutt\">Facebook Profile</a></div>";
        return html;
    },
    loadUsers: function() {
        var id;
        var on = false;

        $(".fbUsersList").each(function() {
            var id = $(this).attr("id");
            //            $("#" + id + " .loading").html(sporthub.loadingSmall());
            var api = FB.Facebook.apiClient;
            api.fql_query("SELECT uid, first_name, last_name, pic_square FROM user WHERE  uid IN (" + $("#" + id + "-idList").val() + ") order by first_name", function(result, ex) {
                if (result) {
                    for (i = 0; i < result.length; i++) {
                        if (result[i]) {
                            if (result[i].first_name == null && result[i].last_name == null) {
                                result[i].first_name = "Facebook";
                                result[i].last_name = "User";
                                result[i].pic_square = "";
                            }
                            var imgPath = sh_facebook.returnProfileThumb(result[i].pic_square);
                            var fullName = result[i].first_name + " " + result[i].last_name;
                            //$("#" + id + "-fb_" + result[i].uid).attr("rel", fullName);
                            //$("#" + id + "-fb_" + result[i].uid + " img").attr("src", imgPath);
                            //$("#" + id + "-n_" + result[i].uid).html(fullName);
                            $("#" + id + " .fb" + result[i].uid + " img").attr("src", imgPath);
                            $("#" + id + " .fb-n" + result[i].uid).html(fullName);
                        }
                    }
                }
            });
            $("#" + id + " .loading").hide();
            $("#" + id + " .inner").show();
        });
        $(".profile").each(function() {
            $(this).qtip({
                content: sporthub.users.popTip($(this).attr("id")),
                position: {
                    corner: { target: 'topMiddle', tooltip: 'bottomMiddle' }
                },
                hide: {
                    fixed: true
                },
                style: { name: 'profileTip' }
            })
        });

        ////        $(".fbUsersList li").hover(function() { }, function() {
        ////            $(".profileSummary").hide();
        ////        });
        ////        $(".fbUsersList").hover(function() { }, function() {
        ////            $(".profileSummary").hide();
        ////        });

        ////        $('.profile').hover(
        ////            function() {
        ////                $(".profileSummary").hide();
        ////                var groupId = sporthub.utility.getPrefix($(this).attr('id'));
        ////                var uniqueId = sporthub.utility.getUniqueId($(this).attr('id'));
        ////                id = sporthub.utility.getId($(this).attr('id'));
        ////                var offset = $(this).offset();
        ////                var pHeight = $("#" + groupId + "-" + uniqueId + "-p_" + id).height();
        ////                $("#" + groupId + "-" + uniqueId + "-p_" + id).css("top", (offset.top -= (pHeight - 10)) + "px");
        ////                $("#" + groupId + "-" + uniqueId + "-p_" + id).css("left", (offset.left -= 10) + "px");
        ////                $("#" + groupId + "-" + uniqueId + "-p_" + id).fadeIn("fast");
        ////                $(this).css("background-color", "#217CD7");
        ////            },
        ////            function() {
        ////                $(this).css("background-color", "#333");
        ////                var offset = $(this).offset();
        ////                var bottom = (offset.top) + 55;
        ////                var right = (offset.left) + 55;
        ////            }
        ////        );

        ////        $('.profileSummary').hover(
        ////            function() {
        ////                var id = sporthub.utility.getId($(this).attr('id'));
        ////                var uniqueId = sporthub.utility.getUniqueId(id);
        ////                $("#" + sporthub.utility.getPrefix(id) + "-" + uniqueId + "-fb_" + id).css("background-color", "#217CD7");
        ////            },
        ////            function() {
        ////                var id = sporthub.utility.getId($(this).attr('id'));
        ////                var uniqueId = sporthub.utility.getUniqueId(id);
        ////                $("#" + sporthub.utility.getPrefix(id) + "-" + uniqueId + "-fb_" + id).css("background-color", "#333");
        ////                $('.profileSummary').hide();
        ////            }
        ////        );
    }
};

sporthub.adminResortEdit = {
    targetId: '#ResortLinks',
    linksUrl: '/Ajax/GetResortLinksList?resortID=' + $("#ResortID").val(),
    linksAddUrl: '/Ajax/AddResortLink',
    onLoad: function() {

        $(sporthub.adminResortEdit.targetId).html(sporthub.loadingSmall()).load(sporthub.adminResortEdit.linksUrl);

        $("#updatePrettyUrl").click(function() {
            $.getJSON("/Ajax/GetPrettyUrl", { resortName: $("#ResortName").val() }, function(o) {
                $("#inlineFeedback").html(o.Message);
                $("#PrettyUrl").val(o.Value);
                if (o.Flag == true) {
                    $("#PrettyUrlCheck").val("true");
                }
                else {
                    $("#PrettyUrlCheck").val("false");
                }
            });
        });

        $("#AddResortLinkBtn").click(function() {
            $(this).attr("disabled", "true");
            sporthub.addResortLink.loadForm();
            tb_show("", "#TB_inline?height=300&width=400&inlineId=layerTarget&modal=true", false);

            //            $(sporthub.adminResortEdit.linksId).html(sporthub.loadingMedium()).load(sporthub.adminResortEdit.linksAddUrl);
        });

    } //onLoad
};

//TODO: remove this once resort review has been moved to its own page
sporthub.ratereview = {
    step: 1,
    resortID: 0,
    formSubmitted: false,
    success: false,
    loadForm: function() {
        sporthub.resortID = document.getElementById("hidResortID").value;
    },
    onLoad: function() {
        //        $("#stepInfo_1").show();
        //        //        $("#prev").hide();
        //        //        $('form.thisForm').submit(function() {
        //        //            sporthub.ratereview.submitForm($(this));
        //        //            return false;
        //        //        });
        //        $('#prev').click(function() {
        //            sporthub.ratereview.step--;
        //            $('#next').show();
        //            if (sporthub.ratereview.step == 1) {
        //                $('#prev').hide();
        //            }
        //            //            sporthub.utility.toggleStepNav(sporthub.ratereview.step);
        //            $(".stepInfoInIn").hide();
        //            $("#stepInfo_" + sporthub.ratereview.step).show();
        //            var m = $("#panes").css("margin-left").replace("px", "");
        //            m = parseInt(m) + 715;
        //            m += "px";
        //            $("#panes").css("margin-left", m);
        //            $("#stepNum").html(", Step " + sporthub.ratereview.step + " of 3");
        //        });
        //        $('#next').click(function() {
        //            sporthub.ratereview.step++;
        //            $('#prev').show();
        //            if (sporthub.ratereview.step == 3) {
        //                $('#next').hide();
        //            }
        //            //            sporthub.utility.toggleStepNav(sporthub.ratereview.step);
        //            $(".stepInfoInIn").hide();
        //            $("#stepInfo_" + sporthub.ratereview.step).show();
        //            var m = $("#panes").css("margin-left").replace("px", "");
        //            m -= 715;
        //            m += "px";
        //            $("#panes").css("margin-left", m);
        //            $("#stepNum").html(", Step " + sporthub.ratereview.step + " of 3");
        //            //            $("#panes").animate({
        //            //                marginLeft: m
        //            //            }, 1000);
        //        });
        $(".slider").each(function() {
            var a = $(this).attr("id").split('_');
            var id = "#val_" + a[1];
            var hidid = "#hid_" + a[1];
            var val = ($(hidid).val() == "") ? 0 : $(hidid).val();
            //            var val = $(hidid).val();
            $(id).html(val);
            sporthub.ratereview.setRatingClass(id, val);
            $(this).slider({
                value: val,
                min: 0,
                max: 100,
                step: 1,
                slide: function(event, ui) {
                    $(id).html(ui.value);
                    $(hidid).val(ui.value);
                    sporthub.ratereview.setRatingClass(id, ui.value);
                }
            });
        });
    },
    setRatingClass: function(id, val) {
        $(id).removeClass("unrated");
        $(id).removeClass("bottom");
        $(id).removeClass("middle");
        $(id).removeClass("top");
        if (val == 0) {
            $(id).html("?");
            $(id).addClass("unrated");
        } else if (val < 30) {
            $(id).addClass("bottom");
        } else if (val > 69) {
            $(id).addClass("top");
        } else {
            $(id).addClass("middle");
        }
    }
    //    submitForm: function(form) {
    //        sporthub.ratereview.formSubmitted = true;
    //        var action = form.attr('action');
    //        var serialized = form.serialize();
    //        $.post(action,
    //			serialized,
    //			sporthub.ratereview.response
    //		);
    //    },
    //    response: function(resp, status) {
    //        scroll(0, 0);
    //        //        if (resp.indexOf("LOGINERROR") < 0) {
    //        //            window.location.reload(); //stays on current page
    //        //        }
    //        //        else {
    ////        $(sporthub.layerContent).html(resp);
    //        $(sporthub.layerContent).hide();
    //        tb_remove();
    //        window.location.reload();
    //                //        }
    //    }
};

sporthub.news = {
    feedCount: '',
    targetId: '',
    feedUrl: '',
    onLoad: function() {
        for (var i = 0; i <= sporthub.news.feedCount; i++) {
            sporthub.news.getFeed(i);
        }
    },
    getFeed: function(i) {
        sporthub.news.targetId = "#TestFeed_" + i;
        sporthub.news.feedUrl = '/Ajax/GetFeed?feedUrl=' + $("#feedUrl_" + i).val();
        $(sporthub.news.targetId).html(sporthub.loadingSmall()).load(sporthub.news.feedUrl);
    }
};

sporthub.addResortLink = {
    targetId: '#AddResortLink',
    loadUrl: '/Ajax/AddResortLink?resortID=' + $("#ResortID").val(),
    loadForm: function() {
        $(sporthub.layerContent).show();
        $(sporthub.layerContent).html(sporthub.loadingMedium()).load(sporthub.addResortLink.loadUrl);
    },
    submitForm: function(form) {
        var action = form.attr('action');
        alert(action);
        var serialized = form.serialize();
        $.post(action,
			serialized,
			sporthub.addResortLink.response
		);
    },
    response: function(resp, status) {
        //        $(sporthub.addResortLink.targetId).html(resp);
        $(sporthub.layerContent).hide();
        tb_remove();
        //        window.location.reload();

        //TODO: check for success
        $(sporthub.adminResortEdit.targetId).html(sporthub.loadingMedium()).load(sporthub.adminResortEdit.linksUrl);

        //        setTimeout(function() {
        //            tb_remove();
        //        }, 2000);
    }
};

sporthub.notLoggedIn = {
    url: "/Ajax/NotLoggedIn/",
    success: false,
    loadForm: function() {
        $('.close').click(function() {
            $(sporthub.layerContent).hide();
            tb_remove();
        });
        $(sporthub.layerContent).show();
        $(sporthub.layerContent).html(sporthub.loadingSmall()).load(sporthub.notLoggedIn.url);
    }
};

sporthub.utility = {
    ieCheck: function(str) {
        $.each($.browser, function(i) {
            if ($.browser.msie) {
                sporthub.isIE = true; //detect IE, because its a pile of fucking shit
            }
        });
    },
    toggleStepNav: function(step) {
        if (step == 1) {
            $("#prev").hide();
        } else {
            $("#prev").show();
        }
        if (step == 3) {
            $("#next").hide();
        } else {
            $("#next").show();
        }
        return false;
    },
    getId: function(str) {
        var arr = (str).split('_');
        return arr[1];
    },
    getPrefix: function(str) {
        var arr = (str).split('_');
        var arr2 = (arr[0]).split('-');
        return arr2[0];
    },
    getUniqueId: function(str) {
        var arr = (str).split('_');
        var arr2 = (arr[0]).split('-');
        return arr2[1];
    },
    relExternal: function() {
        window.open(this.href);
        return false;
    },
    twitter: function() {
        getTwitters('tweet', {
            id: 'snowhub',
            count: 1,
            clearContents: true,
            enableLinks: true,
            ignoreReplies: true,
            withFriends: false
        });
    },
    qTipProfile: function(id) {
        alert($(id).attr("rel"));
        $(id).qtip({
            content: $(id).attr("rel"),
            position: {
                corner: { target: 'rightMiddle', tooltip: 'leftTop' }
            },
            show: { effect: 'fade' },
            style: {
                name: 'cream',
                tip: true,
                border: {
                    width: 2,
                    radius: 5
                }
            }
        });
        return false;
    },
    qTipInit: function(el) {
        //        $(el + ' a[title]').qtip({
        //            position: {
        //                target: 'mouse',
        //                corner: { target: 'rightMiddle', tooltip: 'leftMiddle' },
        //                adjust: { mouse: true }
        //            },
        //            style: { name: 'cream', tip: true }
        //        });
        $('a.qtip[title]').qtip({
            position: {
                target: 'mouse',
                corner: { target: 'rightBottom', tooltip: 'leftTop' },
                adjust: { mouse: true }
            },
            show: { effect: 'fade' },
            style: {
                name: 'cream',
                tip: true,
                border: {
                    width: 3,
                    radius: 8
                }
            }
        });
        $('a.user[title]').qtip({
            position: {
                corner: { target: 'bottomMiddle', tooltip: 'topMiddle' }
            },
            show: { effect: 'fade' },
            style: {
                name: 'dark',
                tip: true,
                border: {
                    width: 3,
                    radius: 8
                }
            }
        });
        return false;
    },
    createCookie: function(name, value, days) {
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else var expires = "";
        document.cookie = name + "=" + value + expires + "; path=/";
    },
    readCookie: function(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    },
    eraseCookie: function(name) {
        createCookie(name, "", -1);
    },
    clearAlert: function(id) {
        document.getElementById(id).style.display = 'none';
    },
    //    initRowLinks: function() {
    //        $('a.row').hover(function() {
    //            $(this).parent().parent().css("background-color", "#34383a");
    //        },
    //        function() {
    //            $(this).parent().parent().css("background-color", "transparent");
    //        });
    //    },
    //    initListLinks: function() {
    //        $('ul.list1 li a').hover(function() {
    //            $(this).parent().css("background-color", "#34383a");
    //        },
    //        function() {
    //            $(this).parent().css("background-color", "transparent");
    //        });
    ////        $('ul.list1 li:last').css("border-bottom-width", "0");
    //    },
    getDotNetID: function(inID) {
        return document.getElementById("ctl00_ContentMain_" + inID);
    },
    redirectSearch: function(o) {
        window.location = "/resorts/" + o.id;
    },
    getQueryString: function(qs) {
        var r = '';
        var q = location.search;
        q = q.replace(/^\?/, ''); // remove the leading ?
        q = q.replace(/\&$/, ''); // remove the trailing &
        jQuery.each(q.split('&'), function() {
            var key = this.split('=')[0];
            if (key == qs) {
                r = this.split('=')[1];
                return;
            }
        });
        return r;
    }
};

sporthub.loadBasicUserList = {
    targetId: '#target-',
    loadUrl: '/Ajax/BasicUserList?ids=',
    getUsers: function() {

        $(".userIds").each(function() {
            var id = $(this).attr("id").split('-');
            //$(sporthub.loadBasicUserList.targetId + id[1]).html(sporthub.loadingSmall()).load(sporthub.loadBasicUserList.loadUrl + $(this).val());
            $("#target-1").html(sporthub.loadingSmall()).load(sporthub.loadBasicUserList.loadUrl + $(this).val());
        });

    }
};

//gets .net id

function mapSetZoomLevel(markers) {
    var bounds = new GLatLngBounds;
    for (var i = 0; i < markers.length; i++) {
        bounds.extend(markers[i].getLatLng());
    }
    map.setZoom(map.getBoundsZoomLevel(bounds));
    map.panTo(bounds.getCenter());
}
