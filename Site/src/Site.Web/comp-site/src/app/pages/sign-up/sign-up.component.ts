import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ITeam } from 'src/app/models/team';
import { ParticipantsService } from 'src/app/services/participants-service.service';
import { ValidationService } from 'src/app/services/validation-service.service';


@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit, OnDestroy {


  participantError: string = "";

  team: ITeam = {
    id: 0,
    name: '',
    participants: []
  }

  addSubscription: Subscription;
  removeSubscription: Subscription;

  constructor(private route: ActivatedRoute, private validationService: ValidationService, private addService: ParticipantsService) {
      route.queryParams.subscribe(params => {
          this.team.name = params["name"];
      })

      this.addSubscription = addService.onAdd().subscribe(newParticipant => {
        this.team.participants.push({...newParticipant});
      })

      this.removeSubscription = addService.onRemove().subscribe(email => this.removeParticipant(email));
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


  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.addSubscription.unsubscribe();
    this.removeSubscription.unsubscribe();
  }

}
