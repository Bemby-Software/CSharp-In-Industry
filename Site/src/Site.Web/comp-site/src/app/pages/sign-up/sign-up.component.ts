import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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

  constructor(private route: ActivatedRoute, private validationService: ValidationService) {
      route.queryParams.subscribe(params => {
          this.team.name = params["name"];
      })
   }

   removeParticipant(email: string) {
     console.log(participant);
     var participant = this.team.participants.find(p => p.email === email);
     if(participant) {
        var index = this.team.participants.indexOf(participant);
        this.team.participants.slice(index);
     }
     else {
       console.log("No participant with email" + email);
     }
    
   }

   addParticipant(participant: IParticipant) {
     if(this.checkParticipant(participant)) {
        this.team.participants.push(participant)
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
            this.participantError = isInUse ? emailInUseError : "";
            return !isInUse;
        }, err => {
          this.participantError = "Something went wrong please try again";
          return false;
          console.log(err)
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
