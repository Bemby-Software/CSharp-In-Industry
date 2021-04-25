import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { IApiError } from '../models/errors';
import { IParticipant } from '../models/participant';
import { ValidationService } from './validation.service';

const emailInUseError = "The email provided is already in use";

@Injectable({
  providedIn: 'root'
})
export class ParticipantsService {

  constructor(private validationSerivce: ValidationService) { }

  private onClearSubject = new Subject();
  private onParticipantAdded = new Subject<IParticipant>();
  private onErrorChanged = new Subject<string>();
  private onRemoveSubject = new Subject<string>();
  private inUseEmail: string[] = [];


  remove(email: string) {
      const toRemove = this.inUseEmail.find(inUse => inUse == email);
      if(toRemove) {
        var index = this.inUseEmail.indexOf(toRemove);
        this.inUseEmail.splice(index, 1);
        this.onRemoveSubject.next(email);
      }
  } 

  clear() {
    this.onClearSubject.next();
  }

  add(participant: IParticipant): boolean {
    
    if(this.check(participant)) {
      this.onParticipantAdded.next(participant);
      this.inUseEmail.push(participant.email);
      return true;
    }

    return false;
  }


  setError(error: string) {
    this.onErrorChanged.next(error);
  }

  check(participant: IParticipant): boolean {

      if(participant.forename === "") {
        this.setError("A forename is required");
        return false;
      }

      if(participant.surname === "") {
        this.setError("A surname is required");
        return false;
      }

      var existingEmail = this.inUseEmail.find(email => email === participant.email);

      if(existingEmail === undefined) {

        this.validationSerivce.isEmailInUse(participant.email)
        .subscribe(isInUse => {
            this.setError("");
            return true;
        }, (err: HttpErrorResponse) => {    

          this.setError("Something went wrong please try again")

          if(err.status === 400) {
            var dto = err.error as IApiError;

            if(dto.isUserFriendly) {
              this.setError(dto.reason);
            }
            else {              
            }          
            return false;    
          }
          else {
            return false;
          }

                
        })

      } else {
        this.setError(emailInUseError);
        return false;
      }

      return true;
  }

  onError(): Observable<string> {
    return this.onErrorChanged.asObservable();
  }

  onAdd(): Observable<IParticipant> {
    return this.onParticipantAdded.asObservable();
  }

  onClear(): Observable<any> {
    return this.onClearSubject.asObservable();
  }

  onRemove(): Observable<string> {
    return this.onRemoveSubject.asObservable();
  }
}
