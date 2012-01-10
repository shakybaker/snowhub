<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<html>
<head>
    <style type="text/css" media="all">
        @import "/Static/Styles/Reset.css";
        @import "/Static/Styles/960.css";
        @import "/Static/Styles/Sporthub.css";
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function() {
//            sporthub.notLoggedIn.onLoad(); 
        });
        
    </script>
</head>
<body>
<table style="margin: -5px -5px 0 -5px; width: 210px;">
<tr>
<td colspan="2"></td>
<td><img class="close" src="/static/images/close.png" alt="Close" id="header_1_close_popup"/></td>
</tr>
<tr>
<td colspan="3">&nbsp;</td>
</tr>
<tr>
<td colspan="3">You need to be logged-in to do that.</td>
</tr>
<tr>
<td><a class="button" href="/account/login"><span>Login</span></a></td>
<td><a class="button" href="/account/create"><span>Register</span></a></td>
<td></td>
</tr>
</table>

</body>
</html>