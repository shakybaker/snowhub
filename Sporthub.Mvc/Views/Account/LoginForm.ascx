<%@ Control Language="C#" Inherits="ViewUserControl<LoginViewData>" %>
                <label for="Email">Email</label>
                <%= Html.TextBox("Email", Model.Email, new { @class = "tb tb200" })%>
                <label for="Password">Password</label>
                <%= Html.Password("Password", Model.Email, new { @class = "tb tb200" })%><span style="float: left; display: block; margin: 7px 0 0 7px;">(<a href="#">Forgot your password?</a>)</span>
                <label for="Persist"><%= Html.CheckBox("Persist") %>  Remember me for 2 weeks</label>
                <span class="button"><button type="submit" name="login" value="Come In >>">Come In >></button></span> 