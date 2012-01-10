


var TimeToFade = 300.0;

//initiates fade-out of any element
function fade(eid) {
    var element = document.getElementById(eid);
    if (element == null)
        return;

    if (element.FadeState == null) {
        if (element.style.opacity == null
            || element.style.opacity == ''
            || element.style.opacity == '1') {
            element.FadeState = 2;
        }
        else {
            element.FadeState = -2;
        }
    }

    if (element.FadeState == 1 || element.FadeState == -1) {
        element.FadeState = element.FadeState == 1 ? -1 : 1;
        element.FadeTimeLeft = TimeToFade - element.FadeTimeLeft;
    }
    else {
        element.FadeState = element.FadeState == 2 ? -1 : 1;
        element.FadeTimeLeft = TimeToFade;
        setTimeout("animateFade(" + new Date().getTime() + ",'" + eid + "')", 33);
    }
}

//animates fade-out
function animateFade(lastTick, eid) {
    var curTick = new Date().getTime();
    var elapsedTicks = curTick - lastTick;

    var element = document.getElementById(eid);

    if (element.FadeTimeLeft <= elapsedTicks) {
        element.style.opacity = element.FadeState == 1 ? '1' : '0';
        element.style.filter = 'alpha(opacity = ' + (element.FadeState == 1 ? '100' : '0') + ')';
        element.FadeState = element.FadeState == 1 ? 2 : -2;
        return;
    }

    element.FadeTimeLeft -= elapsedTicks;
    var newOpVal = element.FadeTimeLeft / TimeToFade;
    if (element.FadeState == 1)
        newOpVal = 1 - newOpVal;

    element.style.opacity = newOpVal;
    element.style.filter = 'alpha(opacity = ' + (newOpVal * 100) + ')';

    setTimeout("animateFade(" + curTick + ",'" + eid + "')", 33);
}









/*
* --------------------------------------------------------------------
* jQuery-Plugin "preloadCssImages"
* by Scott Jehl, scott@filamentgroup.com
* http://www.filamentgroup.com
* reference article: http://www.filamentgroup.com/lab/automated_image_preloading/
* demo page: http://www.filamentgroup.com/examples/preloadImages/
* 
* Copyright (c) 2008 Filament Group, Inc
* Licensed under GPL (http://www.opensource.org/licenses/gpl-license.php)
*
* Version: 1.0, 31.05.2007
* Changelog:
* 	02.20.2008 initial Version 1.0
* --------------------------------------------------------------------
*/
$.preloadCssImages = function(settings) {
//overrideable defaults
    settings = jQuery.extend({
        imgDir: 'images'
    }, settings);

    //dump all the css rules into one string
    var sheets = document.styleSheets;
    var cssPile = '';
    for (var i = 0; i < sheets.length; i++) {
        var thisSheetRules = document.styleSheets[i].cssRules;
        for (var j = 0; j < thisSheetRules.length; j++) {
            cssPile += thisSheetRules[j].cssText;
        }
    }
    //parse string for image urls and load them into the DOM
    var allImgs = []; //new array for all the image urls  
    var imgUrls = cssPile.match(/[^\/]+\.(gif|jpg|jpeg|png)/g); //reg ex to get a string of between a "/" and a ".filename"
    if (imgUrls != null && imgUrls.length > 0 && imgUrls != '') {//loop array
        var arr = jQuery.makeArray(imgUrls); //create array from regex obj	 
        $(arr).each(function(k) {
            allImgs[k] = new Image(); //new img obj
            allImgs[k].src = settings.imgDir + '/' + this;
        });
    }
    return allImgs;
}
