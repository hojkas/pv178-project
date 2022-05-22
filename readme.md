# Project overview

The project implements a simple **Tournament Manager**.

It has two parts - API in asp.NET and frontend in React.

The API has endpoints for all the CRUD operations needed to manage the tournament - to view and modify tournament itself, its players, matches played and players participating in the matches.

* All the controllers in the API are **async**.
* The API uses **database** (SQLite) to store the data, the data are accessed using Entity Framework, also in **asynchronous** way.

The React frontend only displays the data but doesn't modify them (to keep the scope of the project realistic).

# Running the project from Commandline

## API

From inside of `./TournamentManagerAPI/TournamentManagerAPI/` folder:

```
dotnet restore
dotnet run
```

The API will run on `https://localhost:7191/`.

You can visit `https://localhost:7191/swagger` to see all the available endpoints and to try sending requests to them.

## Frontend

From inside of `./tournament-manager-frontend/` folder:

```
npm install
npm start
```

(The npm install will require downloading additional data.)

The application will run on `http://localhost:3000/`.


