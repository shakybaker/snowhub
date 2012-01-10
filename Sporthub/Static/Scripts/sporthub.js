$(document).ready(function() {
    preparePage();
});


var myimages = new Array()
function preloadimages() {
    for (i = 0; i < preloadimages.arguments.length; i++) {
        myimages[i] = new Image()
        myimages[i].src = preloadimages.arguments[i]
    }
}

preloadimages(
    "/assets/images/snowhub_ovr.png",
    "/assets/images/loadingAnimation.gif"
);

$(window).keydown(function(event) {
    var keyCode = event.keyCode || window.event.keyCode;
    if (keyCode == 27) {//ESC
        $("#results").slideUp("slow");
    }
});

//        $(document).ready(function() {
//            if (keyCode == 27) {
//                $("#results").hide("slow");
//            }
//        });

function preparePage() {

    // START breadcrumb navigation
    var id;
    var on = false;
    $('.bcMenuitem').hover(
          function() {
              $('.bcNavdd').hide();
              var idArr = ($(this).attr('id')).split('_');
              id = idArr[1];
              var offset = $(this).offset();
              $(this).css("background-color", "#3197fe");
              $('#dropdown_' + id).css("top", (offset.top += $(this).height()) + "px");
              $('#dropdown_' + id).css("left", offset.left + "px");
              $('#dropdown_' + id).fadeIn("fast");
          },
          function() {
              if (on) {
                  $('#dropdown_' + id).show();
              }
              $(this).css("background-color", "transparent");
          }
      );

    $('.bcNavdd').hover(
          function() {
              on = true;
          },
          function() {
              on = false;
              $('.bcNavdd').hide();
          }
      );

    $('#bcNav').hover(
          function() {
          },
          function() {
              if (!on) {
                  $(".bcNavdd").hide();
              }
          }
      );
          // END breadcrumb navigation

    $('#head').hover(
        function() {
            $('#head').addClass('h1Ovr');
        },
        function() {
            $('#head').removeClass('h1Ovr');
        }
    );

    $('h1').click(function() {
        window.location = "/Default.aspx";
    });

    $('#PageHeading').click(function() {
        //TODO: if IE then dont animate
        $('#ResortInfo').animate({ marginTop: "0" }, 750);
        $('#ResortInfo').animate({ marginTop: "-10px" }, 150);
        $('#ResortInfo').animate({ marginTop: "0" }, 100);
        $('#ResortInfo').animate({ marginTop: "-5px" }, 50);
        $('#ResortInfo').animate({ marginTop: "0" }, 25);
        $('#ResortButtons').animate({ marginTop: "0", marginBottom: "10px" }, 750);
        $('#ResortButtons').animate({ marginTop: "-10px" }, 150);
        $('#ResortButtons').animate({ marginTop: "0" }, 100);
        $('#ResortButtons').animate({ marginTop: "-5px" }, 50);
        $('#ResortButtons').animate({ marginTop: "0" }, 25);
    });

//    $('table.table1 tbody tr:last').css('border-bottom-width', '0');
   // $('table.table1 tbody tr:even').addClass('even');
//    $('ul.list1 li:last').css('border-bottom-width', '0');
//    $('div.pod h4:first').css('border-top-width', '0');
   // $('ul.list1 li:even').addClass('even');
} //preparePage()
