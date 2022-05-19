import React, { useState } from "react";

export default function App() {
  const [tournaments, SetTournaments] = useState([]);

  function getTournaments() {
    const url = "https://localhost:7191/api/Tournaments";
    fetch(url, {
      method: 'GET'
    })
    .then(response => response.json())
    .then(tournamentsFromServer => {
      console.log(tournaments);
      SetTournaments(tournamentsFromServer);
    })
    .catch((error) => {
      console.log(error);
      alert(error);
    });
  }

  return (
    <div className="App md:container md:mx-auto mt-5">
      <h1 className="text-3xl font-bold underline">
        Hello world!
      </h1>

      <div className="mt-5">
        <button onClick={getTournaments} className="btn btn-blue">Load Tournaments</button>
      </div>

      <div className="mt-5">
        {renderTournamentsTable()}
      </div>
    </div>
  );


  function renderTournamentsTable() {
    return(
      <div className="tournament-table">
        <table className="table-auto border-collapse border border-slate-600">
          <thead>
            <tr>
              <th className="border border-slate-600" scope="col">Id</th>
              <th className="border border-slate-600" scope="col">Name</th>
              <th className="border border-slate-600" scope="col">Description</th>
            </tr>
          </thead>
        </table>
      </div>
    )
  }
}
