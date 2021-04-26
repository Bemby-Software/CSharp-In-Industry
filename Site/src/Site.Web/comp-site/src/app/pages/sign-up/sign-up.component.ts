import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { IApiError } from 'src/app/models/errors';
import { ITeam } from 'src/app/models/team';
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

  constructor(private route: ActivatedRoute, private addService: ParticipantsService, private teamsService: TeamsService, private router: Router, private toastr: ToastrService) {
    route.queryParams.subscribe(params => {
      this.team.name = params["name"];
    })

    this.addSubscription = addService.onAdd().subscribe(newParticipant => {
      this.team.participants.push({ ...newParticipant });
    })

    this.removeSubscription = addService.onRemove().subscribe(email => this.removeParticipant(email));
  }


  signUp() {
    this.teamsService.signUp(this.team)
      .subscribe(() => {
          this.toastr.success(`${this.team.name} signed up`);      
          this.team = { ...this.defaultTeam };          
          this.router.navigate(["/signin"]);        
      }, (response: HttpErrorResponse) => {

        if (response.status !== 400) {
          this.toastr.error("Oops! Something went wrong")
        }

        const error = response.error as IApiError;

        if (error.isUserFriendly) {
          this.toastr.error(error.reason);
        }
        else {
          this.toastr.error("Oops! Something went wrong")
        }        
      });
  }

  removeParticipant(email: string) {
    const participant = this.team.participants.find(p => p.email === email);
    if (participant) {
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
