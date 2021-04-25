import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IApiError } from '../models/errors';

@Injectable({
  providedIn: 'root'
})
export class ApiHelperService {

  constructor() { }

  handle400ErrorWithToast(response: HttpErrorResponse): boolean {
      if(response.status !== 400) {
        return false;
      }

      const error = response.error as IApiError;

      if(error.isUserFriendly) {
          console.log(error.reason);
      }
      else {

      }

      return true;

  }
}
