delete from Matches;
delete from PlayerOrMatchResults;
delete from Players;
delete from Tournaments;

update sqlite_sequence
set seq = 0;

insert into Tournaments (Name, Description, StartDate, EndDate)
values ("Test tournament", "The virtual tournament held in nowhere for testing this fine project.", "2022-05-20 12:00:00", "2022-05-20 20:00:00");

insert into Players (Name, TournamentId, Note)
values ("Player A", 1, "Look here! This player has a note!");
insert into Players (Name, TournamentId)
values ("Player B", 1);
insert into Players (Name, TournamentId)
values ("Player C", 1);
insert into Players (Name, TournamentId)
values ("Player D", 1);
insert into Players (Name, TournamentId)
values ("Player E", 1);
insert into Players (Name, TournamentId)
values ("Player F", 1);

insert into Matches(IsFinished, TournamentId, WinnerId, Name, Score)
values (true, 1, 1, "A vs B", "5:0");
insert into Matches(IsFinished, TournamentId, Name)
values (false, 1, "C vs D");
insert into Matches(IsFinished, TournamentId, WinnerId, Name, Score)
values (true, 1, 5, "E vs F", "2:1");

insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 1, 1, 0);                          
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 1, 2, 0);                          
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 2, 3, 0);                          
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 2, 4, 0);                          
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 3, 5, 0);                          
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 3, 6, 0);

insert into Matches(IsFinished, TournamentId, Name)
values (false, 1, "Winner of A vs B versus winner of C vs D");

insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 4, 1, 0);
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, MatchId, IsEmpty)
values (false, 4, 2, 0);

update Matches
set PlayerRequiringResultId = 8
where Id = 2;

/* second tournament */

insert into Tournaments (Name, Description, StartDate, EndDate)
values ("Another tournament", "Just to show it is possible to have more!", "2022-05-20 12:00:00", "2022-05-20 20:00:00");

insert into Players (Name, TournamentId, Note)
values ("My precious time", 2, "Who reads this anyway.");
insert into Players (Name, TournamentId, Note)
values ("Project from c#", 2, "Stop reading my random placeholder data!");

insert into Matches(IsFinished, TournamentId, Name)
values (false, 2, "This is gonna be brutal.");

insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 5, 7, 0); 
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 5, 8, 0); 

/* third tournament */

insert into Tournaments (Name, Description, StartDate, EndDate)
values ("The big tournament", "8 players, only one will win!", "2022-05-25 12:00:00", "2022-05-25 20:00:00");

/* ids 9-16 */
insert into Players (Name, TournamentId)
values ("Mr A", 3);
insert into Players (Name, TournamentId)
values ("Mr B", 3);
insert into Players (Name, TournamentId)
values ("Mr C", 3);
insert into Players (Name, TournamentId)
values ("Mr D", 3);
insert into Players (Name, TournamentId)
values ("Mr E", 3);
insert into Players (Name, TournamentId)
values ("Mr F", 3);
insert into Players (Name, TournamentId)
values ("Mr G", 3);
insert into Players (Name, TournamentId)
values ("Mr H", 3);

/* round 1: ids 6 - 9 */
insert into Matches(IsFinished, TournamentId, Name)
values (false, 3, "Round 1-1");
insert into Matches(IsFinished, TournamentId, Name)
values (false, 3, "Round 1-2");
insert into Matches(IsFinished, TournamentId, Name)
values (false, 3, "Round 1-3");
insert into Matches(IsFinished, TournamentId, Name)
values (false, 3, "Round 1-4");

/* round 1: ids 11 - 18 */
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 6, 9, 0);
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 6, 10, 0); 
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 7, 11, 0); 
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 7, 12, 0); 
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 8, 13, 0); 
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 8, 14, 0); 
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 9, 15, 0); 
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, PlayerId, IsEmpty)
values (true, 9, 16, 0); 

/* round 2: ids 10-11 */
insert into Matches(IsFinished, TournamentId, Name)
values (false, 3, "Round 2-1");
insert into Matches(IsFinished, TournamentId, Name)
values (false, 3, "Round 2-2");

/* round 2: ids 19 - 22*/
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, MatchId, IsEmpty)
values (false, 10, 6, 0);
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, MatchId, IsEmpty)
values (false, 10, 7, 0);
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, MatchId, IsEmpty)
values (false, 11, 8, 0);
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, MatchId, IsEmpty)
values (false, 11, 9, 0);

/* edit from round 1 */
update Matches
set PlayerRequiringResultId = 19
where Id = 6;
update Matches
set PlayerRequiringResultId = 20
where Id = 7;
update Matches
set PlayerRequiringResultId = 21
where Id = 8;
update Matches
set PlayerRequiringResultId = 22
where Id = 9;

/* round 3: id 12 */
insert into Matches(IsFinished, TournamentId, Name)
values (false, 3, "Round 3-1");

/* round 3: ids 23-24 */
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, MatchId, IsEmpty)
values (false, 12, 10, 0);
insert into PlayerOrMatchResults(IsPlayer, OriginalMatchId, MatchId, IsEmpty)
values (false, 12, 11, 0);

/* edit from round 2 */
update Matches
set PlayerRequiringResultId = 23
where Id = 10;
update Matches
set PlayerRequiringResultId = 24
where Id = 11;