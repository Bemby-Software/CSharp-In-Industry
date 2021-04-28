import { Injectable } from '@angular/core';
import {IResult, SimpleResult} from "../models/errors";
import {HttpClient, HttpErrorResponse, HttpResponseBase} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {catchError, map, tap} from "rxjs/operators";
import {ApiHelperService} from "./api-helper.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private apiHelper: ApiHelperService) { }

  isSignedIn(): boolean {
    return false;
  }

  signIn(email: string, token: string) {
      const request$ = this.http.post('api/auth', {email: email, token: token});
      return this.apiHelper.handleSimpleHttpResponse(request$);
  }


}
