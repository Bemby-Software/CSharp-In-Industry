import { Injectable } from '@angular/core';
import {IDataResult, IResult, SimpleResult} from "../models/errors";
import {HttpClient, HttpErrorResponse, HttpHeaders, HttpResponseBase} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {catchError, map, tap} from "rxjs/operators";
import {ApiHelperService} from "./api-helper.service";
import {IParticipant, ITeamParticipant} from "../models/participant";
import {UserSessionService} from "./user-session.service";

@Injectable({
  providedIn: 'root'
})
export class ParticipantService {

  constructor(private http: HttpClient, private apiHelper: ApiHelperService) { }

  isSignedIn(): boolean {
    return false;
  }

  signIn(email: string, token: string) {
      const request$ = this.http.post('api/auth', {email: email, token: token});
      return this.apiHelper.handleSimpleHttpResponse(request$);
  }

  getParticipantDetails(token: string) : Observable<IDataResult<ITeamParticipant>> {
    const request$ = this.http.get<ITeamParticipant>(`api/participant?token=${token}&includeTeam=${true}`, this.apiHelper.getAuthHeadersOptions(token))
    return this.apiHelper.handleDataHttpResponse<ITeamParticipant>(request$)
  }


}
