import { Injectable } from '@angular/core';
import { Observable, of } from "rxjs";
import { IResult } from "../models/errors";
import { ParticipantService } from "./participant.service";
import { map, mergeMap } from "rxjs/operators";
import { ITeamParticipant } from "../models/participant";

@Injectable({
  providedIn: 'root'
})
export class UserSessionService {

  private participant: ITeamParticipant | null = null;

  constructor(private participantsService: ParticipantService) {

  }

  getParticipant() {
    return this.participant;
  }

  private getParticipantDetails(token: string): Observable<IResult> {
    return this.participantsService.getParticipantDetails(token)
      .pipe(map(participantResult => {

        if (participantResult.successful) {
          this.participant = participantResult.data;
        }

        return participantResult.asResult();

      }))
  }

  signIn(email: string, token: string): Observable<IResult> {
    return this.participantsService.signIn(email, token)
      .pipe(
        mergeMap((result) => {
          if (result.successful) {
            return this.getParticipantDetails(token);
          }
          return of(result);
        }));
  }


}
