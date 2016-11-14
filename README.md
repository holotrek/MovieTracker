MovieTracker
============

Requirements
----

 - [Visual Studio](http://www.microsoft.com/visualstudio/en-us/try)
 - [SQL Express](http://www.microsoft.com/en-us/sqlserver/editions/2012-editions/express.aspx)

_[Note: movie tracker usage nuget package restore](http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages)_


**Steps to get it running**

 - Unzip the archive
 - Open MovieTracker.sln
 - Attach the MovieTracker.mdf to your instance of sql express.
 - Run the application


Default User with movies setup
---
 - User name: *members1st*
 - Password: *tester*


Planned Bugs
--
 - Able to edit, delete, and view movies that are not tied to your account.
    - Steps to reproduce
     - Go to either /Movie/Edit/5000, /Movie/Detail/5000, or /Movie/Delete/5000
        - It appears you can also adjust a rating on a movie you do not own as well by adjusting the request parameters via firebug.
     - You are able to view, edit, delete that movie that is not on your account
  - Expected behavior
     - You should be directed to an error page or some other page stating that movie is not part of your inventory
 - My Movies page seems snappier than the other pages
   - Steps to reproduce
     - Open up My Movies which comes up almost instantly
     - Click on details, edit, or delete
     - Page lags a bit *(would increase as total movie inventory increases)*
   - Expected behavior
     - Each request should share the same performance
 - Rating on my movies page only works once per title
    - Steps to reproduce
     - Open up my movies
      - Either click a star or click clear rating on a movie
     - After the rating is updated you cannot click on that rating again
     - If you leave the page and come back the rating link will work again
   - Expected behavior
     - You should be able to rate a movie as many times as you want during the page load.

Features Requested
--
 - As a user when there are numerous items on the page I would like a way to filter the movie list
    - Create a free form text box that filters out the movie list as you type.  The filter should be based off of movie name.
   - This text box should be placed above the movie list.
 - As a user I would like to mark a movie as lent to a friend with date and click to mark it as returned.
   - Movie Details
     - Under movie detail add a section for a free form text box to record a friends name and also date for when the movie was lent to your friend
     - There is no need for an audit log of actions
     - Clicking the lent to a friend button updates the movie as lent to a friend
     - The friend name and date field should contain validation to make sure the information is valid before submitting.
     - Clicking the movie returned button will clear out the fields and also mark the movie as returned for the movie record.
    - My Movies Page
     - Add the friends name and date it was lent out under the movie title.  This row should also be marked in a fashion to see that the movie is lent to your friend.
     - Add a small action link next to the friends name to mark the movie as returned, this should clear the marked name and date from the movie and update the record accordingly
 - As a new user I no longer want redirected to the index page but rather to add a new movie page.
 - As a new user I would like restrictions for a more complex password
    - Passwords should have a min length of 10
   - Passwords should contain a minimum of 3 non alpha numeric characters.

