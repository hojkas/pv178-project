import React, { useState } from "react";
import { API_BASE_URL } from "./utilities/constants";

export default function App() {
  const [tournaments, SetTournaments] = useState([]);
  const [loaded_tournament, SetTournament] = useState([]);

  function getTournaments() {
    const url = API_BASE_URL + "Tournaments";
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

      {tournaments.length == 0 && renderLoadTournamentsButton()}
      {tournaments.length > 0 && renderTournamentsManipulationButton()}
      {tournaments.length > 0 && renderTournamentsTable()}

    </div>
  );

  function renderLoadTournamentsButton() {
    return (
      <div className="mt-5">
        <button onClick={getTournaments} className="btn btn-blue">Load Tournaments</button>
      </div>
    );
  }

  function renderTournamentsManipulationButton() {
    return (
      <div className="mt-5">
        <button onClick={getTournaments} className="btn btn-blue">Reload Tournaments</button>
      </div>
    );
  }

  function renderTournamentsTable() {
    return(
      <div className="mt-5">
        <table className="tournament-table simple-table">
          <thead>
            <tr>
              <th scope="col">Id</th>
              <th scope="col">Name</th>
              <th scope="col">Description</th>
              <th scope="col"></th>
            </tr>
          </thead>
          <tbody>
            {tournaments.map((tournament) => (
              <tr key={tournament.id}>
                <td scope="row">{tournament.id}</td>
                <td>{tournament.name}</td>
                <td>{tournament.description}</td>
                <td>
                  <button className="btn-small btn-blue">Load this tournament</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    )
  }
}
