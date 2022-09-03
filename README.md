# DeltaXMovieApplication

This ASP.NET core Web API was built using .NET 6 for DeltaX Assessment. 
EF core Database-First approach was used for Object-Relation mapping. Database script file "MovieApplicationDB.sql" was uploaded to the repository.
The database consists of 4 tables(Movie,Actor,Producer,MovieActotRelationship).The **MovieDetails** class in the project contains all the required properties for the API. **MockMovieData** class contains all the methods that provide data to controller.
The application has following APIs:

### 1. AddNewMovie: 
This endpoint takes movie details as input and if the movie already exists it will return Bad Request else it will add new movie details to the database.
- ***HttpMethod:*** POST
- ***Endpoint:*** <Host>/api/Movie/AddMovieDetails(Eg: https://localhost:7143/api/Movie/AddMovieDetails)
              
Sample1:
```
Input: 
{
	"movieName": "Iron Man",
	"movieDescription": "After being held captive in an Afghan cave, billionaire engineer Tony Stark creates a unique weaponized suit of armor to fight evil.",
	"releasedDate": "2008-05-02T00:00:00",
	"producerName": "Marvel Studios",
	"actors": [
	  "Robert Downey Jr",
	  "Gwyneth Paltrow",
	  "Terrence Howard",
	  "Jeff Bridges"
	 ]
}

Respons Code: 200  
Response Body:  
{
	"movieName": "Iron Man",
	"movieDescription": "After being held captive in an Afghan cave, billionaire engineer Tony Stark creates a unique weaponized suit of armor to fight evil.",
	"releasedDate": "2008-05-02T00:00:00",
	"producerName": "Marvel Studios",
	"actors": [
	  "Robert Downey Jr",
	  "Gwyneth Paltrow",
	  "Terrence Howard",
	  "Jeff Bridges"
	 ]
}
```
Sample2:   
As we already added Ironman movie to the database as shown in Sample1, this request will return 400 status code. Another movie with the same name cannot be inserted.
```
Input:  
{
	"movieName": "Iron Man",
	"movieDescription": "After being held captive in an Afghan cave",
	"releasedDate": "2008-05-02T00:00:00",
	"producerName": "Marvel Studios",
	"actors": [
	  "Robert Downey Jr",
	  "Gwyneth Paltrow",
	  "Terrence Howard"    
	 ]
 }
Response Code: 400
Response Body: Movie with this name already exists
```

### 2. UpdateMovieDetails: 
This endpoint takes movie name and movie details as input and if the movie name exists in the database we can edit it's details. If the movie does not exist in the database, we can't edit movie.
- ***HttpMethod:*** PUT
- ***Endpoint:*** <Host>/api/Movie/EditMovieDetails/{movieName} (Eg: https://localhost:7143/api/Movie/EditMovieDetails/Iron%20Man)

Sample1: 
Iron Man movie exists in the database. In this request, we updated the movie description and removed one actor.
```
Input: https://localhost:7143/api/Movie/EditMovieDetails/Iron%20Man
{
	"movieName": "Iron Man",
	"movieDescription": "Update test",
	"releasedDate": "2008-05-02T00:00:00",
	"producerName": "Marvel Studios",
	"actors": [
		"Robert Downey Jr",
		"Gwyneth Paltrow",
		"Terrence Howard"                            
	]
}

Respons Code: 200  
Response Body:  
{
	"movieName": "Iron Man",
	"movieDescription": "Update test",
	"releasedDate": "2008-05-02T00:00:00",
	"producerName": "Marvel Studios",
	"actors": [
		"Robert Downey Jr",
		"Gwyneth Paltrow",
		"Terrence Howard"                            
	]
}
```
Sample2: 
Passing steelman as movieName in URI. As steel man name does not exist in the database it will give Bad Request.
```
Input : https://localhost:7143/api/Movie/EditMovieDetails/steel%20man
{
	"movieName": "Steel Man",
	"movieDescription": "Update test",
	"releasedDate": "2008-05-02T00:00:00",
	"producerName": "Marvel Studios",
	"actors": [
		"Robert Downey Jr",
		"Gwyneth Paltrow",
		"Terrence Howard"                            
	]
}
Response Code: 400
Response Body: Movie with this name does not exist
```

### 3. FetchMovieDetails: 
This endpoint returns all movie details from the database as a list.
- ***HttpMethod:*** GET
- ***Endpoint:*** <Host>/api/Movie/GetMovies(Eg: https://localhost:7143/api/Movie/GetMovies)

Sample1:
```
Response code: 200
Response Body: 
[
	{
	  "movieName": "Iron Man",
	  "movieDescription": "Update test",
	  "releasedDate": "2008-05-02T00:00:00",
	  "producerName": "Marvel Studios",
	  "actors": [
			"Robert Downey Jr",
			"Gwyneth Paltrow",
			"Terrence Howard"
		  ]
	 }
]
```
Sample2: 
I added few more movies to the database
```
Response code: 200
Response Body:
[
	{
	  "movieName": "Iron Man",
	  "movieDescription": "After being held captive in an Afghan cave, billionaire engineer Tony Stark creates a unique weaponized suit of armor to fight evil.",
	  "releasedDate": "2008-05-02T00:00:00",
	  "producerName": "Marvel Studios",
	  "actors": [
			"Robert Downey Jr",
			"Gwyneth Paltrow",
			"Terrence Howard",
			"Jeff Bridges"
		  ]
	},
	{
	  "movieName": "Spider-Man: Homecoming",
	  "movieDescription": "Peter Parker balances his life as an ordinary high school student in Queens with his superhero alter-ego Spider-Man, and finds himself on the trail of a new menace prowling the skies of New York City.",
	  "releasedDate": "2017-07-07T00:00:00",
	  "producerName": "Marvel Studios",
	  "actors": [
			"Robert Downey Jr",
			"Tom Holland",
			"Zendaya",
			"Gwyneth Paltrow",
			"Michael Keaton"
		  ]
	 },
	{
	  "movieName": "The Dark Knight",
	  "movieDescription": "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
	  "releasedDate": "2017-07-18T00:00:00",
	  "producerName": "Emma Thomas",
	  "actors": [
			"Christian Bale",
			"Heath Ledger"
		  ]
	 }
]
```
**_NOTE:_** Apart from the 3 APIs mentioned in the Assessment I also created 3 other APIs that can be used in the web API.

### 4. GetActorNames: 
Gets all the actor names from the database as a list. Can be used in add/edit movie to choose actors from dropdown list.
- ***HttpMethod:*** GET
- ***Endpoint:*** <Host>/api/Movie/GetActors(Eg: https://localhost:7143/api/Movie/GetActors)

Sample:
```
Response code: 200
Response Body : 
[
   "Robert Downey Jr",
   "Gwyneth Paltrow",
   "Terrence Howard",
   "Jeff Bridges",
   "Tom Holland",
   "Zendaya",
   "Michael Keaton",
   "Christian Bale",
   "Heath Ledger"
]
 ```
 
### 5. GetProducerNames:
Gets all the producer names from the database as a list. Can be used in add/edit movie to choose producers from dropdown list.
- ***HttpMethod:*** GET
- ***Endpoint:*** <Host>/api/Movie/GetProducers(Eg: https://localhost:7143/api/Movie/GetProducers)

Sample:
```
Response code: 200
Response Body:
[
	"Marvel Studios",
	"Emma Thomas"
]
```

### 6. DeleteMovie:
Used to delete movie based on movie name from database.
- ***HttpMethod:*** DELETE
- ***Endpoint:*** <Host>/api/Movie/DeleteMovie/{movieName}(Eg: https://localhost:7143/api/Movie/DeleteMovie/The%20Dark%20Knight)

Sample1:
```
Input: The Dark Knight
Response code: 200
Response Body: Movie Deleted
```
Sample2:
```
Input: The Incredible Hulk
Response code: 400
Response Body: Moviename does not exist
```
