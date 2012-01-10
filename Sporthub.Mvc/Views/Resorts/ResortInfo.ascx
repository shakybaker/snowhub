<%@ Control Language="C#" Inherits="ViewUserControl<Sporthub.Mvc.ViewData.ResortViewData>" %>
        <div class="pod" style="margin-top: 20px">
            <div class="scoreWrap">
                <%= Html.Score(Model.Resort.Score.ToString(), Sporthub.Model.Enumerators.ScoreSize.Large, Model.Resort.ScoreCount, Model.ReviewCount, Model.FavouriteCount, Model.VisitedCount )%>
                <div class="cb"></div>
<%--                <div class="other-rating first"><span class="visited"></span><span><%=Model.VisitedCount %></span></div>
                <div class="other-rating"><span class="favourited"></span><span><%=Model.FavouriteCount %></span></div>
                <div class="other-rating"><span class="reviewed"></span><span><%=Model.ReviewCount %></span></div>--%>
            </div>
        </div>
        <%if (Model.Resort.ResortStats != null) {%>
        <%if (Model.Resort.ResortStats.HasRunsInfo()) {%>
        <div class="pod">
            <div class="headwrap">
                <h3>Ski Runs/Pistes</h3>
            </div>
            <div class="podIn">
            <%= Html.ResortRuns(Model.Resort)%>
                
                <div class="cb"></div>
            </div>
        </div>
        <%} %>
        <%} %>
        <%if (Model.Resort.ResortStats != null) {%>
        <% if (Model.Resort.ResortStats.HasLiftInfo()) { %>
        <div class="pod">
            <div class="headwrap">
                <h3>Lift System</h3>
            </div>
            <div class="podIn">
                <% var i = 0; %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.LiftTotal))
                   { %>
                <% i++; %>
                <div class="qt lift" title="Total Lifts" style="margin: 4px <%= (i%4==0) ? "0" : "11px" %> 8px 0; "><img src="/Static/Images/lift-chair.png" alt="chair2" /><br /><span class="counter"><%= Model.Resort.ResortStats.LiftTotal%></span></div>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.DoubleCount))
                   { %>
                <% i++; %>
                <div class="qt lift" title="Double Chairs" style="margin: 4px <%= (i%4==0) ? "0" : "11px" %> 8px 0; "><img src="/Static/Images/lift-chair-2.png" alt="chair2" /><br /><span class="counter"><%= Model.Resort.ResortStats.DoubleCount%></span></div>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.TripleCount))
                   { %>
                <% i++; %>
                <div class="qt lift" title="Triple Chairs" style="margin: 4px <%= (i%4==0) ? "0" : "11px" %> 8px 0; "><img src="/Static/Images/lift-chair-3.png" alt="chair3" /><br /><span class="counter"><%= Model.Resort.ResortStats.TripleCount%></span></div>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.QuadCount))
                   { %>
                <% i++; %>
                <div class="qt lift" title="Quad Chairs" style="margin: 4px <%= (i%4==0) ? "0" : "11px" %> 8px 0; "><img src="/Static/Images/lift-chair-4.png" alt="chair4" /><br /><span class="counter"><%= Model.Resort.ResortStats.QuadCount%></span></div>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.QuadPlusCount))
                   { %>
                <% i++; %>
                <div class="qt lift" title="Quad+ Chairs" style="margin: 4px <%= (i%4==0) ? "0" : "11px" %> 8px 0; "><img src="/Static/Images/lift-chair-4-plus.png" alt="chair4+" /><br /><span class="counter"><%= Model.Resort.ResortStats.QuadPlusCount%></span></div>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.SurfaceCount))
                   { %>
                <% i++; %>
                <div class="qt lift" title="Surface/Drag/Poma" style="margin: 4px <%= (i%4==0) ? "0" : "11px" %> 8px 0; "><img src="/Static/Images/lift-surface.png" alt="surface" /><br /><span class="counter"><%= Model.Resort.ResortStats.SurfaceCount%></span></div>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.GondolaCount))
                   { %>
                <% i++; %>
                <div class="qt lift" title="Gondola/Cable Car" style="margin: 4px <%= (i%4==0) ? "0" : "11px" %> 8px 0; "><img src="/Static/Images/lift-gondola.png" alt="gondola" /><br /><span class="counter"><%= Model.Resort.ResortStats.GondolaCount%></span></div>
                <% } %>
                <div class="cb"></div>
            </div>
        </div>
            <%} %>
            <%} %>
            
        <%= Html.AverageSnowfall(Model.Resort) %>
      
        <%if (Model.Resort.ResortStats != null) {%>
        <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.BaseLevel) ||
               !string.IsNullOrEmpty(Model.Resort.ResortStats.TopLevel) ||
               !string.IsNullOrEmpty(Model.Resort.ResortStats.VerticalDrop) ||
               !string.IsNullOrEmpty(Model.Resort.ResortStats.LongestRunDistance) ||
               !string.IsNullOrEmpty(Model.Resort.ResortStats.SkiableTerrianSize) ||
               !string.IsNullOrEmpty(Model.Resort.ResortStats.SnowmakingCoverage)) { %>
        <div class="pod">
            <div class="headwrap">
                <h3>Quick Facts</h3>
            </div>
            <div class="podIn">
                <table class="table1 nobord">
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.BaseLevel)) { %>
                <tr><th style="width: 80%">Base</th><td><%= string.Concat(Model.Resort.ResortStats.BaseLevel, "m") %></td></tr>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.TopLevel)) { %>
                <tr><th>Top</th><td><%= string.Concat(Model.Resort.ResortStats.TopLevel, "m") %></td></tr>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.VerticalDrop)) { %>
                <tr><th>Vertical Drop</th><td><%= string.Concat(Model.Resort.ResortStats.VerticalDrop, "m") %></td></tr>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.LongestRunDistance)) { %>
                <tr><th>Longest Run</th><td><%= string.Concat(Model.Resort.ResortStats.LongestRunDistance, "km")%></td></tr>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.SkiableTerrianSize)) { %>
                <tr><th>Skiable Terrian</th><td><%= string.Concat(Model.Resort.ResortStats.SkiableTerrianSize, "hectares")%></td></tr>
                <% } %>
                <% if (!string.IsNullOrEmpty(Model.Resort.ResortStats.SnowmakingCoverage)) { %>
                <tr><th>Snowmaking</th><td><%= string.Concat(Model.Resort.ResortStats.SnowmakingCoverage, "hectares")%></td></tr>
                <% } %>
                </table>
                <div class="cb"></div>
            </div>
        </div>
        <% } %>
        <% } %>
