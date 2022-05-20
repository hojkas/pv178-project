import React, { useState, useEffect } from "react";
import { API_BASE_URL } from "./utilities/constants";
import { format } from "date-fns";
import ReactTooltip from 'react-tooltip';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faAngleDown, faAngleUp, faArrowsRotate } from '@fortawesome/free-solid-svg-icons'

export default function App() {
  const [tournaments, SetTournaments] = useState([]);
  const [selectedTournament, SetTournament] = useState();
  const [showTournamentInfo, SetShowTournamentInfo] = useState();
  const [tournamentPlayers, SetTournamentPlayers] = useState([]);
  const [tournamentMatches, SetTournamentMatches] = useState([]);

  const [selectedMatch, SetSelectedMatch] = useState();
  const [matchRequiringSelectedMatch, SetMatchRequiringSelectedMatch] = useState();


  useEffect(() => {
    getTournaments();
  }, []);

  function getTournaments() {
    const url = API_BASE_URL + "Tournaments";
    SetTournament();
    SetTournamentPlayers([]);
    SetTournamentMatches([]);
    SetShowTournamentInfo(true);
    SetSelectedMatch();
    SetMatchRequiringSelectedMatch();
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

  function formatDate(stringDate) {
    return format(new Date(stringDate), "dd.MM.yyyy H:mm");
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

  function getMatchDetail(matchId) {
    const url = API_BASE_URL + 'Matches/' + matchId;
    fetch(url, {
      method: 'GET'
    })
    .then(response => response.json())
    .then(matchFromServer => {
      console.log(matchFromServer);
      SetSelectedMatch(matchFromServer);
    })
    .catch((error) => {
      console.log(error);
      alert(error);
    });
  }

  function getRequiringMatch(matchId) {
    const url = API_BASE_URL + 'Matches/' + matchId;
    fetch(url, {
      method: 'GET'
    })
    .then(response => response.json())
    .then(matchFromServer => {
      console.log(matchFromServer);
      SetMatchRequiringSelectedMatch(matchFromServer);
    })
    .catch((error) => {
      console.log(error);
      alert(error);
    });
  }

  function getRequiringMatchFromPOMR(pomrId) {
    const url = API_BASE_URL + 'PlayerOrMatchResults/' + pomrId;
    if (pomrId == null) SetMatchRequiringSelectedMatch();
    else {
      fetch(url, {
        method: 'GET'
      })
      .then(response => response.json())
      .then(pomrFromServer => {
        console.log(pomrFromServer);
        getRequiringMatch(pomrFromServer.originalMatchId);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });
    }
  }

  function getRequiringMatchFromMATCH(matchId) {
    const url = API_BASE_URL + 'Matches/' + matchId;
    fetch(url, {
      method: 'GET'
    })
    .then(response => response.json())
    .then(matchFromServer => {
      console.log(matchFromServer);
      getRequiringMatchFromPOMR(matchFromServer.playerRequiringResultId);
    })
    .catch((error) => {
      console.log(error);
      alert(error);
    });
  }

  function setNewTournament(tournament) {
    SetTournament(tournament);
    SetTournamentPlayers([]);
    SetTournamentMatches([]);
    SetShowTournamentInfo(true);
    SetSelectedMatch();
    SetMatchRequiringSelectedMatch();
  }

  // POMR = Player or match result
  function getPOMRName(pomr) {
    if(pomr.isPlayer && pomr.player != null) return pomr.player.name;
    if(!pomr.isPlayer && pomr.match != null) return ("Winner of '" + pomr.match.name + "'");
    return "[empty]";
  }

  // APP BODY
  return (
    <div>
      <div className="App md:container md:mx-auto my-5 px-5">
        <h1 className="text-3xl font-bold text-blue-800">
          Tournament Manager - Data View <span className="clickable" onClick={getTournaments}><FontAwesomeIcon icon={faArrowsRotate}/></span>
        </h1>
        
        {tournaments.length > 0 ? renderTournamentsTable() : <div>No tournaments found...</div>}

        {selectedTournament != null && renderTounament()}
        {selectedTournament != null && renderTournamentPlayersDiv()}
        {selectedTournament != null && renderMatchesDiv()}
      </div>
    </div>
  );

  function renderTournamentsTable() {
    return (
      <div className="mt-5">
        <div className="header2">Tournaments</div>
        <table className="tournament-table simple-table">
          <thead>
            <tr>
              <th scope="col">Name</th>
              <th scope="col">Description</th>
              <th scope="col"></th>
            </tr>
          </thead>
          <tbody>
            {tournaments.map((tournament) => (
              <tr key={tournament.id}>
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
        <div className="text-blue-bold">{selectedTournament.name}</div>
        <div>{selectedTournament.description}</div>
        
        <div>
          Start time:&nbsp;
            {selectedTournament.startDate ?
            formatDate(selectedTournament.startDate) :
            "No date given"}
        </div>
        <div>
          End time:&nbsp;
            {selectedTournament.endDate ?
            formatDate(selectedTournament.endDate) :
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
                <th scope="col">Name</th>
                <th scope="col">Note</th>
              </tr>
            </thead>
            <tbody>
              {tournamentPlayers.map((player) => (
                <tr key={player.id}>
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

  function renderMatchesDiv() {
    if(tournamentMatches.length == 0) return (
      <div className="mt-5">
        <div className="header2 clickable" onClick={getTournamentMatches}>
          Matches <FontAwesomeIcon icon={faAngleDown}/>
        </div>
      </div>
    )
    else return (
      <div className="mt-5">
        <div className="header2 clickable" onClick={() => SetTournamentMatches([])}>
          Matches <FontAwesomeIcon icon={faAngleUp}/>
        </div>
        <div>
          TIP: You can click on a match to show it in relationship to other matches!
        </div>
        <div>
          <table className="tournament-table simple-table">
            <thead>
              <tr>
                <th scope="col">Name</th>
                <th scope="col">Players</th>
                <th scope="col">Start</th>
                <th scope="col">Score</th>
                <th scope="col">Winner</th>
              </tr>
            </thead>
            <tbody>
              {tournamentMatches.map((match) => (
                <tr key={match.id} onClick={() => { getMatchDetail(match.id); getRequiringMatchFromPOMR(match.playerRequiringResultId); }}>
                  <td>{match.name}</td>
                  <td>
                    <span className="text-blue-bold">{getPOMRName(match.players[0])}</span>{/*
                    */} vs {/*
                    */}<span className="text-blue-bold">{getPOMRName(match.players[1])}</span>
                  </td>
                  <td>
                    {match.start != null ? formatDate(match.start) :
                      <div className="text-center">-</div>}
                  </td>
                  <td>
                    {match.score != null ? match.score :
                      <div className="text-center">-</div>}
                  </td>
                  <td>
                    {match.winner != null ?
                      <div className="text-blue-bold">{match.winner.name}</div> :
                      <div className="text-center">-</div>}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
        {selectedMatch != null && !selectedMatch.IsEmpty && renderSelectedMatch()}
      </div>
    )
  }

  function renderSelectedMatch() {
    return (
      <div className="interactive-match">
        <div className="header2 mt-3 mb-3">Interactive match view</div>
        <div>Click on the connected match to show its connections instead.</div>
        <div>
          <table className="match-table table-fixed">
            <tbody>
              <tr>
                <td>
                  {selectedMatch.players[0].isEmpty || selectedMatch.players[0].isPlayer ?
                    <div className="match-info-empty"></div> :
                    <div className="match-info font-bold clickable" onClick={
                      () => { getMatchDetail(selectedMatch.players[0].matchId);
                        SetMatchRequiringSelectedMatch(selectedMatch) }}>
                          {getPOMRName(selectedMatch.players[0])}</div> }
                </td>
                <td rowSpan={2}>
                  <div className="match-info match-info-selected font-bold">{selectedMatch.name}</div>
                </td>
                <td rowSpan={2}>
                  {matchRequiringSelectedMatch == null ?
                      <div className="match-info-empty"></div> :
                      <div className="match-info font-bold" onClick={
                        () => { getMatchDetail(matchRequiringSelectedMatch.id);
                          getRequiringMatchFromMATCH(matchRequiringSelectedMatch.id)}}>
                            {matchRequiringSelectedMatch.name}</div> }
                </td>
              </tr>
              <tr>
                <td>
                {selectedMatch.players[1].isEmpty || selectedMatch.players[1].isPlayer ?
                    <div className="match-info-empty"></div> :
                    <div className="match-info font-bold clickable" onClick={
                        () => { getMatchDetail(selectedMatch.players[1].matchId);
                          SetMatchRequiringSelectedMatch(selectedMatch) }}>
                            {getPOMRName(selectedMatch.players[1])}</div> }
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    );
  }
}
