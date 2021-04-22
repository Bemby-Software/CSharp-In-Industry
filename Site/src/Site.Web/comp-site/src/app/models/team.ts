import { IParticipant } from "./participant";

export interface ITeam {
    id: number;
    name: string;
    participants: IParticipant[];
}