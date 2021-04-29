import { Injectable } from '@angular/core';
import {HttpErrorResponse, HttpHeaders, HttpResponse, HttpResponseBase} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {DataResult, IApiError, IDataResult, IResult, SimpleResult} from "../models/errors";
import {catchError, map, tap} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class ApiHelperService {

  constructor() { }

  getAuthHeadersOptions(token: string) {
    let headers = new HttpHeaders();
    headers.set('X-COMP-AUTH-', token)
    return {
      headers: headers
    }
  }

  handleSimpleHttpResponse<T>(observable: Observable<T>): Observable<IResult> {
      return observable.pipe(
        tap((response: any) => console.log(response)),
        map((response: HttpResponseBase) => {
            return SimpleResult.Success();
        }),
        catchError((response: HttpErrorResponse) => {

          if(response.status === 400) {
            const apiError = response.error as IApiError;

            if(apiError && apiError.isUserFriendly) {
              return of(SimpleResult.Failure(apiError.reason))
            }

          }

          return of(SimpleResult.Failure());
        })
      );
  }

  handleDataHttpResponse<T>(observable: Observable<T>): Observable<IDataResult<T>> {
    return observable.pipe(
      tap((response: any) => console.log(response)),
      map((response: T) => {
        return DataResult.Success<T>(response);
      }),
      catchError((response: HttpErrorResponse) => {

        if(response.status === 400) {
          const apiError = response.error as IApiError;

          if(apiError && apiError.isUserFriendly) {
            return of(DataResult.Failure<T>(apiError.reason))
          }

        }

        return of(DataResult.Failure<T>());
      })
    );
  }


}
