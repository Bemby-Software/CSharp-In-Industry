import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ITeam } from 'src/app/models/team';
import { ApiHelperService } from 'src/app/services/api-helper.service';
import { ParticipantsService } from 'src/app/services/participants.service';
import { TeamsService } from 'src/app/services/teams.service';


@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit, OnDestroy {


  participantError: string = "";

  defaultTeam: ITeam = {
    id: 0,
    name: '',
    participants: []
  };

  team: ITeam = this.defaultTeam;

  addSubscription: Subscription;
  removeSubscription: Subscription;

  constructor(private route: ActivatedRoute, private addService: ParticipantsService, private teamsService: TeamsService, 
    private apiHelperService: ApiHelperService, private router: Router) {
      route.queryParams.subscribe(params => {
          this.team.name = params["name"];
      })

      this.addSubscription = addService.onAdd().subscribe(newParticipant => {
        this.team.participants.push({...newParticipant});
      })

      this.removeSubscription = addService.onRemove().subscribe(email => this.removeParticipant(email));
   }


   signUp() {
      this.teamsService.signUp(this.team)
      .subscribe(() => {
          this.team = {...this.defaultTeam};
          this.router.navigate(["/signin"]);
      }, this.apiHelperService.handle400ErrorWithToast);
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
