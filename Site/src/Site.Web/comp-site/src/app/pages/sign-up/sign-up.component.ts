import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IApiError } from 'src/app/models/errors';
import { IParticipant } from 'src/app/models/participant';
import { ITeam } from 'src/app/models/team';
import { ValidationService } from 'src/app/services/validation-service.service';


const emailInUseError = "The email provided is already in use";

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {


  participantError: string = "";

  team: ITeam = {
    id: 0,
    name: '',
    participants: []
  }

  onClearForm = new EventEmitter();

  constructor(private route: ActivatedRoute, private validationService: ValidationService) {
      route.queryParams.subscribe(params => {
          this.team.name = params["name"];
      })
   }

   removeParticipant(email: string) {
     const participant = this.team.participants.find(p => p.email === email);
     if(participant) {
        var index = this.team.participants.indexOf(participant);
        this.team.participants.splice(index, 1);
     }
     else {
       console.log("No participant with email" + email);
     }

   }

   addParticipant(participant: IParticipant) {
     if(this.checkParticipant(participant)) {
        this.team.participants.push(participant)
        this.onClearForm.emit();
     }
   }


   checkParticipant(participant: IParticipant): boolean {

      var existing = this.team.participants.find(p => p.email === participant.email);

      if(participant.forename === "") {
        this.participantError = "A forename is required";
        return false;
      }

      if(participant.surname === "") {
        this.participantError = "A surname is required";
        return false;
      }

      if(existing === undefined) {

        this.validationService.isEmailInUse(participant.email)
        .subscribe(isInUse => {
            this.participantError = "";
            return true;
        }, (err: HttpErrorResponse) => {    

          this.participantError = "Something went wrong please try again";

          if(err.status === 400) {
            var dto = err.error as IApiError;

            if(dto.isUserFriendly) {
              this.participantError = dto.reason
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
        this.participantError = emailInUseError;
        return false;
      }

      return true;
   }

  ngOnInit(): void {
  }

}
