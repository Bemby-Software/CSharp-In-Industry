import { Injectable } from '@angular/core';
import {Observable, of} from "rxjs";
import {IResult, SimpleResult} from "../models/errors";
import {ParticipantService} from "./participant.service";
import {catchError, map} from "rxjs/operators";
import {IParticipant, ITeamParticipant} from "../models/participant";

@Injectable({
  providedIn: 'root'
})
export class UserSessionService {

  private participant: ITeamParticipant | null = null;

  constructor(private participantsService: ParticipantService) {

  }

  signIn(email: string, token: string) : Observable<IResult> {
    // @ts-ignore
    return this.participantsService.signIn(email, token)
      .pipe(map(result => {
        if (result.successful) {
          return this.participantsService.getParticipantDetails(token)
            .pipe(map(participantResult => {

              if (participantResult.successful) {
                this.participant = participantResult.data;
              }

              return participantResult.asResult();

            }))
        }

        return result;
      }));
  }


}
