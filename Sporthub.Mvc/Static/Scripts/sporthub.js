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
            background: '#3197FD',
            color: '#111',
            border: {
                width: 1,
                radius: 6,
                color: '#1E7BD8'
            },
            tip: 'bottomMiddle'
        }
        $.fn.qtip.styles.buttonTip = {
            background: '#000',
            color: '#eee',
            fontSize: '11px',
            padding: '2px 6px 2px 6px',
            border: {
                width: 1,
                radius: 3,
                color: '#000'
            },
            tip: { 
                corner: 'bottomLeft', 
                color: '#000000',
                size: {
                    x: 6, 
                    y: 6 
                }
            } 
        }
        $('a.actbutt[title]').qtip({
            style: {
                name: 'buttonTip'
            },
            position: {
                corner: {
                    target: 'topMiddle',
                    tooltip: 'bottomLeft'
                }
            }
        });

        $('.qt').qtip({
            style: {
                name: 'buttonTip'
            },
            position: {
                corner: {
                    target: 'topMiddle',
                    tooltip: 'bottomLeft'
                }
            }
        });
        $('.qtu').each(function() {
            var pop = $(this).find('.profileSummary').html();
            $(this).qtip({
                style: {
                    name: 'buttonTip'
                },
                content: {
                    text: pop
                },
                position: {
                    corner: {
                        target: 'topMiddle',
                        tooltip: 'bottomLeft'
                    }
                }
            });
        });

        $(".closeFeedback").click(function() {
            $("div.feedback").fadeOut("slow");
        });
        if (sporthub.pageHasNavigation) {
            $("input#search").focus();
            sporthub.utility.qTipInit('');
            $('a[rel="external"]').click(sporthub.utility.relExternal);
            $("ul.list1 > li:last-child").addClass("last");
            $("input#search").keyup(function() {
                var offset = $(this).offset();
                if ($(this).val() == '') {
                    $("#searchResults").hide();
                    $("#searchResultsInner").html('');
                } else {
                    $.getJSON("/Ajax/Search?", { text: $(this).val() }, function(o) {
                        $("#searchResults").css("top", ((offset.top) + 30) + "px");
                        $("#searchResults").css("left", ((offset.left) + 2) + "px");
                        //$("#searchResults").css("width", "124px");

                        $("#searchResults").fadeIn();
                        var content = "<ul class='list1'>";
                        if (o.results.length == 0)
                            content += "No Results Found";
                        $.each(o.results, function(i, result) {
                            content += '<li><a href="/resorts/' + result.id + '">' + result.info + result.value + '</a>';
                        });
                        content += "</ul>";
                        $("#searchResultsInner").html(content);
                    });
                }
            });
            $(".now-viewing a span").click(
                function() {
                    var offset = $(this).offset();
                    $("#jump-to").css("top", ((offset.top) + 15) + "px");
                    $("#jump-to").css("left", ((offset.left) - 190) + "px");

                    $("#jump-to").toggle();
                }
            );
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
                }
            );
        $("table.profile tr").hover(
                function() {
                    $(this).addClass("ovr");
                },
                function() {
                    $(this).removeClass("ovr");
                }
            );
        $("#closeSearch").click(function() {
            $("#searchResults").fadeOut();
            $("#searchResultsInner").html('');
            $("input#search").val('');
        });
        $(".logout").click(function() {
            FB.logout(function(response) {
              // user is now logged out
            });
            return true;
        });


    } //onLoad
};

sporthub.resortPages = {
    onLoad: function() {
        //sporthub.users.loadUsers();
        $("a#markAsVisited").fancybox({
            'modal': true
        });
        $("a.not-logged-in").fancybox({
            'modal': true
        });
        //        $("#markAsVisited").click(function() {
        //            $(this).addClass("on");

        //            var offset = $(this).offset();
        //            $("#visitEntryPopup").css("top", ((offset.top) + 28) + "px");
        //            $("#visitEntryPopup").css("left", (offset.left) + "px");
        //            $("#visitEntryPopup").css("width", "124px");

        //            $("#visitEntryPopup").show();
        //        });
        $("#cnclButt").click(function() {
            $("#markAsVisited").removeClass("on");
            //            $("#visitEntryPopup").hide();
            parent.$.fancybox.close();
        });
        $("#svButt").click(function() {
            var dt = $("#dob_y").val() + "-" + $("#dob_m").val();
            $("#markAsVisited").removeClass("on");
            //$("#visitEntryPopup").hide();
            parent.$.fancybox.close();
            $("#markAsVisited").html("<span><img alt='...' src='/Static/Images/Anim/load-s.gif' /> Saving ...</span>");
            $("#markAsVisited").addClass("loading");
            $.getJSON("/Account/AddDateVisited", { resortID: document.getElementById("hidResortID").value, lastVisitDate: dt }, function(data) {
                if (data.IsAuthenticated == true) {
                    $("#markAsVisited").removeClass("loading");
                    if (data.Result == true) {
                        if (data.IsRemove == true) {
                            $("#markAsVisited").removeClass("isVisited");
                            $("#markAsVisited").html("<span>I've Been</span>");
                        }
                        else {
                            $("#markAsVisited").addClass("isVisited");
                            $("#markAsVisited").html("<span><img src=\"/static/images/tick.png\" alt=\"\" />You've Been</span>");
                        }
                        //window.location.reload();
                    }
                    else {
                        alert(data.ErrorMessage);
                    }
                }
                else {
                    $("#markAsVisited").removeClass("loading");
                    $("#markAsVisited").html("<span>I've Been</span>");
                    alert(data.ErrorMessage);
                }
            });
        });
        $("#addAsFave").click(function() {
            $("#addAsFave").html("<span><img alt='...' src='/Static/Images/Anim/load-s.gif' /> Saving ...</span>");
            $("#addAsFave").addClass("loading");
            $.getJSON("/Account/UpdateFaveResort", { resortID: document.getElementById("hidResortID").value }, function(data) {
                if (data.IsAuthenticated == true) {
                    $("#addAsFave").removeClass("loading");
                    if (data.Result == true) {
                        if (data.IsRemove == true) {
                            $("#addAsFave").removeClass("isFave");
                            $("#addAsFave").html("<span><img src=\"/static/images/fave_off.png\" alt=\"\" />Favourite</span>");
                        }
                        else {
                            $("#addAsFave").addClass("isFave");
                            $("#addAsFave").html("<span><img src=\"/static/images/fave_on.png\" alt=\"\" />Favourite</span>");
                        }
                        //window.location.reload();
                    }
                    else {
                        alert(data.ErrorMessage);
                    }
                }
                else {
                    $("#addAsFave").removeClass("loading");
                    $("#addAsFave").html("<span>Favourite</span>");
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

sporthub.user = {
    submitForm: function(form) {
        $.ajax({
                type: form.attr('method'), // get type of request from 'method'
                url: form.attr('action'), // get url of request from 'action'
                data: form.serialize(), // serialize the form's data
                success: sporthub.user.response
        });


//        var action = form.attr('action');
//        $.post(action,
//			sporthub.user.response
//		);
    },
    response: function(resp, status) {
        alert(resp.Message);
    }
};

sporthub.users = {
    popTip: function(id) {
        var a = id.split('_');
        var a2 = a[0].split('-');
        var id2 = '#' + a2[0] + '-' + a2[1] + '-p_' + a[1];
        var name = $(id2 + ' span').html();
        var sport = $(id2 + ' em').html();
        //        var html = "<div class=\"profileSummaryIn\"><span class=\"fb-n533493250\" id=\"l1-1-n_533493250\">Mark Baker</span><br /><em>Snowboarder, Skier</em><br/><a href=\"/user/" + a + "\" class=\"smlbutt\">Snowhub Profile</a> <a target=\"_blank\" href=\"http://www.facebook.com/profile.php?id=" + a + "\" class=\"smlbutt\">Facebook Profile</a></div>";
        var html = "<div class=\"profileSummaryIn\"><span>" + name + "</span><br /><em>" + sport + "</em><br/><a href=\"/user/" + a[1] + "\" class=\"smlbutt\">Snowhub Profile</a> <a target=\"_blank\" href=\"http://www.facebook.com/profile.php?id=" + a[1] + "\" class=\"smlbutt\">Facebook Profile</a></div>";
        return html;
    },
    loadUsers: function() {
        var id;
        var on = false;

        $(".fbUsersList").each(function() {
            var id = $(this).attr("id");
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
                            $("#" + id + " .fb" + result[i].uid + " img").attr("src", imgPath);
                            $("#" + id + " .fb-n" + result[i].uid).html(fullName);
                        }
                    }
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
                            });
//                            $(this).hover(
//                                function() {
//                                    $(this).parent("div").find("a.profile").css("opacity", "0.5");
//                                    $(this).css("opacity", "1");
//                                },
//                                function() {
//                                    $(this).parent("div").find("a.profile").css("opacity", "1");
//                                }
//                            );
                    });
                }
            });
            //            $("#" + id + " .loading").hide();
            //            $("#" + id + " .inner").show();
        });
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
        $(".slider").each(function() {
            var a = $(this).attr("id").split('_');
            var id = "#val_" + a[1];
            var hidid = "#hid_" + a[1];
            var val = ($(hidid).val() == "") ? 0 : $(hidid).val();
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
        $(sporthub.layerContent).hide();
        tb_remove();

        //TODO: check for success
        $(sporthub.adminResortEdit.targetId).html(sporthub.loadingMedium()).load(sporthub.adminResortEdit.linksUrl);
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
            count: 2,
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
            $("#target-1").html(sporthub.loadingSmall()).load(sporthub.loadBasicUserList.loadUrl + $(this).val());
        });

    }
};

function mapSetZoomLevel(markers) {
    var bounds = new GLatLngBounds;
    for (var i = 0; i < markers.length; i++) {
        bounds.extend(markers[i].getLatLng());
    }
    map.setZoom(map.getBoundsZoomLevel(bounds));
    map.panTo(bounds.getCenter());
}
