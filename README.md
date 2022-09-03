# DeltaXMovieApplication
This ASP.NET core Web API was built using .NET 6 for DeltaX Assessment. 
I used EF core Data base first approach  for Object-Relation mapping.I uploaded Database script file "MovieApplicationDB.sql" to repository.
The Database consists of 4 tables(Movie,Actor,Producer,MovieActotRelationship).
In project I created a MovieDetails class with all required properties for API.
MockMovieData class contains all the methods that provide data to controller.
The Endpoints used to call API are

1.AddNewMovie: This endpoints takes moviedetails as input and if the movie already exists it will return Badrequest else it will add new moviedetails to database.
    Endpoint: https://localhost:7143/api/Movie/AddMovieDetails
    The endpoint takes moviedetails as input.If movie already exists returns BadRequest("Movie with this name already exists").Else it will add data to database and return added movie details.
    Sample1: Added a Movie Iron Man to dataBase. Given all the required details as input.
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

2.UpdateMovieDetails: This endpoint takes moviedetails as input and if the movie name exists in Database we can edit it's details. If movie does not exist in Database we can't edit movie that doesnot exist.
    Endpoint: https://localhost:7143/api/Movie/EditMovieDetails
    Sample1: Iron Man movie exists in Database.Updated movie description and removed one actor. 
        Input: 
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
    
    Sample2: Updated movie name to Batman, but Batman name does not exist in Database.So we can't edit movie that doesnot exist.
        Input : 
             {
                "movieName": "Batman",
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

3.GetMovieDetails: This endpoint returns all movie details from Database as a List.
    Endpoint: https://localhost:7143/api/Movie/GetMovies
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
