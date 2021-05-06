import { Component, OnInit } from '@angular/core';
import { CompetitionName } from 'src/app/utils/constants';
import {Router} from "@angular/router";
import { UserSessionService } from 'src/app/services/user-session.service';
import { IParticipant, ITeamParticipant } from 'src/app/models/participant';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {


  title: string = CompetitionName;
  participant?: ITeamParticipant;


  constructor(private router: Router, private userSessionService: UserSessionService) { }

  ngOnInit(): void {
    this.userSessionService.onUserSignInChanged().subscribe(isSignedIn => {
        if(isSignedIn) {
            this.participant = this.userSessionService.getParticipant();
        }
    });
  }

  
  public get isSignedIn(): boolean {
    return this.participant !== undefined;
  }
  

  goToSignIn() {
    this.router.navigate(['/signin']);
  }

}
