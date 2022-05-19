import React, { useState } from "react";
import { API_BASE_URL } from "./utilities/constants";
import { format } from "date-fns";

export default function App() {
  const [tournaments, SetTournaments] = useState([]);
  const [selectedTournament, SetTournament] = useState();
  

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
    <div className="App md:container md:mx-auto mt-5 px-5">
      <h1 className="text-3xl font-bold text-blue-800">
        My very not scuffed Tournament Manager
      </h1>

      {tournaments.length === 0 && renderLoadTournamentsButton()}
      {tournaments.length > 0 && renderTournamentsManipulationButton()}
      {tournaments.length > 0 && renderTournamentsTable()}

      {selectedTournament != null && renderTounament()}
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
    return (
      <div className="mt-5">
        <div className="header2">Tournaments</div>
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
                <th scope="row">{tournament.id}</th>
                <td>{tournament.name}</td>
                <td>{tournament.description}</td>
                <td>
                  <button className="btn-small btn-blue" onClick={() => SetTournament(tournament)}>Select this tournament</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    )
  }

  function renderTounament() {
    return (
      <div className="mt-5">
        <div className="header2">Tournament info</div>
        <div>Id: {selectedTournament.id}</div>
        <div>{selectedTournament.name}</div>
        <div>{selectedTournament.description}</div>
        
        <div>
          Start time:&nbsp;
            {selectedTournament.startDate ?
            format(new Date(selectedTournament.startDate), "dd.MM.yyyy H:mm") :
            "No date given"}
        </div>
        <div>
          End time:&nbsp;
            {selectedTournament.endDate ?
            format(new Date(selectedTournament.endDate), "dd.MM.yyyy H:mm") :
            "No date given"}
        </div>
      </div>
    );
  }
}
