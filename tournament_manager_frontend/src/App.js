import React, { useState } from "react";
import { API_BASE_URL } from "./utilities/constants";
import { format } from "date-fns";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faAngleDown, faAngleUp } from '@fortawesome/free-solid-svg-icons'

export default function App() {
  const [tournaments, SetTournaments] = useState([]);
  const [selectedTournament, SetTournament] = useState();
  const [showTournamentInfo, SetShowTournamentInfo] = useState();
  const [tournamentPlayers, SetTournamentPlayers] = useState([]);
  const [tournamentMatches, SetTournamentMatches] = useState([]);


  function getTournaments() {
    const url = API_BASE_URL + "Tournaments";
    fetch(url, {
      method: 'GET'
    })
    .then(response => response.json())
    .then(tournamentsFromServer => {
      console.log(tournamentsFromServer);
      SetTournaments(tournamentsFromServer);
    })
    .catch((error) => {
      console.log(error);
      alert(error);
    });
  }

  function getTournamentPlayers() {
    const url = API_BASE_URL + "Tournaments/" + selectedTournament.id + '/Players';
    fetch(url, {
      method: 'GET'
    })
    .then(response => response.json())
    .then(playersFromServer => {
      console.log(playersFromServer);
      SetTournamentPlayers(playersFromServer);
    })
    .catch((error) => {
      console.log(error);
      alert(error);
    });
  }

  function getTournamentMatches() {
    const url = API_BASE_URL + "Tournaments/" + selectedTournament.id + '/Matches';
    fetch(url, {
      method: 'GET'
    })
    .then(response => response.json())
    .then(matchesFromServer => {
      console.log(matchesFromServer);
      SetTournamentMatches(matchesFromServer);
    })
    .catch((error) => {
      console.log(error);
      alert(error);
    });
  }

  function setNewTournament(tournament) {
    SetTournament(tournament);
    SetTournamentPlayers([]);
  }

  return (
    <div className="App md:container md:mx-auto mt-5 mb-5 px-5">
      <h1 className="text-3xl font-bold text-blue-800">
        My very not scuffed Tournament Manager
      </h1>

      {tournaments.length === 0 && renderLoadTournamentsButton()}
      {tournaments.length > 0 && renderTournamentsManipulationButton()}
      {tournaments.length > 0 && renderTournamentsTable()}

      {selectedTournament != null && renderTounament()}
      {selectedTournament != null && renderTournamentPlayersDiv()}
      {selectedTournament != null && renderPlainMatchesDiv()}

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
                  <button className="btn-small btn-blue" onClick={() => setNewTournament(tournament)}>Select this tournament</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    )
  }

  function renderTounament() {
    if (showTournamentInfo) return (
      <div className="mt-5">
        <div className="header2 clickable" onClick={() => SetShowTournamentInfo(false)}>
          Tournament info <FontAwesomeIcon icon={faAngleUp}/>
        </div>
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
    )
    else return (
      <div className="mt-5">
        <div className="header2 clickable" onClick={() => SetShowTournamentInfo(true)}>
          Tournament info <FontAwesomeIcon icon={faAngleDown}/>
        </div>
      </div>
    )
  }

  function renderTournamentPlayersDiv() {
    if(tournamentPlayers.length == 0) return (
      <div className="mt-5">
        <div className="header2 clickable" onClick={getTournamentPlayers}>
          Players <FontAwesomeIcon icon={faAngleDown}/>
        </div>
      </div>
    )
    else return (
      <div className="mt-5">
        <div className="header2 clickable" onClick={() => SetTournamentPlayers([])}>
          Players <FontAwesomeIcon icon={faAngleUp}/>
        </div>
        <div>
          <table className="tournament-table simple-table">
            <thead>
              <tr>
                <th scope="col">Id</th>
                <th scope="col">Name</th>
                <th scope="col">Note</th>
              </tr>
            </thead>
            <tbody>
              {tournamentPlayers.map((player) => (
                <tr key={player.id}>
                  <th scope="row">{player.id}</th>
                  <td>{player.name}</td>
                  <td>{player.note}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    )
  }

  function renderPlainMatchesDiv() {
    if(tournamentMatches.length == 0) return (
      <div className="mt-5">
        <div className="header2 clickable" onClick={getTournamentMatches}>
          Matches list <FontAwesomeIcon icon={faAngleDown}/>
        </div>
      </div>
    )
    else return (
      <div className="mt-5">
        <div className="header2 clickable" onClick={() => SetTournamentMatches([])}>
          Matches list <FontAwesomeIcon icon={faAngleUp}/>
        </div>
        <div>
          <table className="tournament-table simple-table">
            <thead>
              <tr>
                <th scope="col">Id</th>
                <th scope="col">Name</th>
                <th scope="col">Note</th>
              </tr>
            </thead>
            <tbody>
              {tournamentMatches.map((match) => (
                <tr key={match.id}>
                  <th scope="row">{match.id}</th>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    )
  }
  
}
