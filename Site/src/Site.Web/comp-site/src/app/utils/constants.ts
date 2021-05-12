import { ITeamParticipant } from "../models/participant";

export const CompetitionName: string = "Industry C#";

export const MockTeamParticipant: ITeamParticipant = {
    id: 1,
    forename: "Joe",
    surname: "Bloggs",
    email: "joe.bloggs@hotmail.com",
    team: {
        id: 1,
        name: "Test Team 1",
        participants: [
            {
                id: 1,
                forename: "Joe",
                surname: "Bloggs",
                email: "joe.bloggs@hotmail.com",
            },
            {
                id: 1,
                forename: "Joe",
                surname: "Bloggs",
                email: "joe.bloggs@hotmail.com",
            },
        ]
    }
}
