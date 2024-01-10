This is ASP.NET MVC application that allows end users to select their desired origin & destination & departure date and lists available journeys for the specified query.

A session is created and maintained for each different end user who visits the application using the GetSession method of the Obilet.com business API. Each user uses their own login information in subsequent API requests made by the application on that user's behalf.

All available bus locations are retrieved from obilet.com business API GetBusLocations.
It will be listed as starting and destination points.

Departure, arrival and departure dates provided by the user are passed to the obilet.com business API GetJourneys method. Then, the trips returned by the API response are sorted according to their departure times and displayed to the user.

The user's on-screen selections are stored in local storage.
The user's session information is also stored in the cookie.

The screens are designed based on the designs conveyed in the document.
