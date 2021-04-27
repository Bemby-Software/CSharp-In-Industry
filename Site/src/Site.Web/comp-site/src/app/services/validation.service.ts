import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, OperatorFunction } from 'rxjs';

export interface IResult<T> {
  result: T;
  success: boolean;
}

export class SimpleResult implements IResult<string> {
  /**
   *
   */
  constructor(public result: string, public success: boolean) {
  }
}

export function toSimpleResult(response$ : Observable<any>) : Observable<SimpleResult> {
  return new Observable<SimpleResult>((subscriber) => {
    response$.subscribe((response) => {
      subscriber.next(new SimpleResult(response.body, response.statusCode === 200));
      subscriber.complete();
    });
  });
}

@Injectable({
  providedIn: 'root'
})
export class ValidationService {
  constructor(private client: HttpClient) { 
  }

  isTeamNameInUse(name: string) {    
    return this.client.get<boolean>(`api/validation/isTeamNameInUse/${name}`);
  }

  isEmailInUse(email: string) {
    return this.client.get(`api/validation/isEmailOk/${email}`);
  }

  example(params : any[]) : Observable<IResult<string>> {
    return this.client.post('url', JSON.stringify(params)).pipe(toSimpleResult);
  }
}
