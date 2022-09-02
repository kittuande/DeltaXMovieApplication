# DeltaXMovieApplication
This Web API was built using .NET 6 for DeltaX Assessment. 
I used EF core to establish for Object-Relation mapping.
I created a MovieDetails class with all properties required for API.
MockMovieData contains all the methods that provide data to controller.
The Endpoints used to call API
1. https://localhost:7143/api/Movie/GetMovies - Used to fetch details(MovieName, Description, Releaseddate, ProducerName, ActorsList) of all movies from Database.
	Sample Response: code 200
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
    },
    {
        "movieName": "Dangal",
        "movieDescription": "Former wrestler Mahavir Singh Phogat and his two wrestler daughters struggle towards glory at the Commonwealth Games in the face of societal oppression.",
        "releasedDate": "2017-12-21T15:54:49.247",
        "producerName": "Aamir Khan",
        "actors": [
            "Aamir Khan",
            "Fatima Sana Shaikh",
            "Sanya Malhotra"
        ]
    },
    {
        "movieName": "Avengers: Infinity War",
        "movieDescription": "The Avengers and their allies must be willing to sacrifice all in an attempt to defeat the powerful Thanos before his blitz of devastation and ruin puts an end to the universe.",
        "releasedDate": "2018-04-27T00:00:00",
        "producerName": "Marvel Studios",
        "actors": [
            "Robert Downey Jr",
            "Elizabeth Olsen",
            "Tom Holland",
            "Chadwick Boseman"
        ]
    },
    {
        "movieName": "Interstellar",
        "movieDescription": "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
        "releasedDate": "2014-11-07T17:09:10.327",
        "producerName": "Christopher Nolan",
        "actors": [
            "Matthew McConaughey",
            "Anne Hathaway"
        ]
    }
   ]
2.Endpoint: https://localhost:7143/api/Movie/AddMovieDetails
    The endpoint takes moviedetails as input.If movie already exists returns BadRequest("Movie with this name already exists").Else it will add data to database and return added movie details.
    Sample1:
    Input: 
    {
        "movieName": "Interstellar",
        "movieDescription": "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
        "releasedDate": "2014-11-07T17:09:10.327",
        "producerName": "Christopher Nolan",
        "actors": [
            "Matthew McConaughey",
            "Anne Hathaway"
        ]
    }
    Response: Code 400 
    Movie with this name already exists
    Sample2:
    Input:
    