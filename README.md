# DeltaXMovieApplication
This ASP.NET core Web API was built using .NET 6 for DeltaX Assessment. 
I used EF core Data base first approach  for Object-Relation mapping.I uploaded Database script file "MovieApplicationDB.sql" to repository.
The Database consists of 4 tables(Movie,Actor,Producer,MovieActotRelationship).
In project I created a MovieDetails class with all required properties for API.
MockMovieData class contains all the methods that provide data to controller.
The Endpoints used to call API are

1.AddNewMovie: This endpoint takes moviedetails as input and if the movie already exists it will return Badrequest else it will add new moviedetails to database.
    HttpMethod: POST
    Endpoint:<Host>/api/Movie/AddMovieDetails
              https://localhost:7143/api/Movie/AddMovieDetails
    
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
        Response Body:  {
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
    
    Sample2: As we already added Ironman to Database in above sample. Another movie with same name cannot be inserted.
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

2.UpdateMovieDetails: This endpoint takes movieName, moviedetails as input and if the movie name exists in Database we can edit it's details. If movie does not exist in Database we can't edit movie.
    HttpMethod: PUT
    Endpoint:<Host>/api/Movie/EditMovieDetails/{movieName}
             https://localhost:7143/api/Movie/EditMovieDetails/Iron%20Man
    Sample1: Iron Man movie exists in Database.Updated movie description and removed one actor. 
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
    
    Sample2: Passing steelman as movieName in URI. As steel man name doesnot exist in database it will give badrequest. 
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

3.FetchMovieDetails: This endpoint returns all movie details from Database as a List.
    HttpMethod: GET
    Endpoint:<Host>/api/Movie/GetMovies
             https://localhost:7143/api/Movie/GetMovies
    Sample1: 
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
    Sample2: I added few more movies into database 
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
Along with these 3 endpoints mentioned in Assessment I also created another 3 endpoints.

4. GetActorNames: Gets all actor names from Database as List. Can be used in add/edit movie to choose actors from existing list.
    HttpMethod: GET
    Endpoint: <Host>/api/Movie/GetActors
                https://localhost:7143/api/Movie/GetActors
    Sample:
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

5.GetProducerNames: Gets all producer names from Database as List. Can be used in add/edit movie to choose producers from existing list.
    HttpMethod: GET
    Endpoint: <Host>/api/Movie/GetProducers
               https://localhost:7143/api/Movie/GetProducers
        Sample:
        Response code: 200
        Response Body:
                    [
                        "Marvel Studios",
                        "Emma Thomas"
                    ]

6.DeleteMovie: Used to delete movie based on movie name from database.
    HttpMethod: DELETE
    Endpoint: <Host>/api/Movie/DeleteMovie/{movieName}
               https://localhost:7143/api/Movie/DeleteMovie/The%20Dark%20Knight
        

        Sample1:
            Input: The Dark Knight
            Response code: 200
            Response Body: Movie Deleted
        Sample2:
            Input: The Incredible Hulk
            Response code: 400
            Response Body: Moviename does not exist