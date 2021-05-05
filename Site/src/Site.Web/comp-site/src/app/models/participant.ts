import {ITeam} from "./team";

export interface IParticipant {
    id: number;
    forename: string;
    surname: string;
    email: string;
}

export interface ITeamParticipant extends IParticipant{
    team: ITeam;
}
