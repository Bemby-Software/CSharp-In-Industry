import { Component, OnInit } from '@angular/core';
import { ITeamParticipant } from 'src/app/models/participant';
import { UserSessionService } from 'src/app/services/user-session.service';
import { MockTeamParticipant } from 'src/app/utils/constants';

@Component({
  selector: 'app-team-hub',
  templateUrl: './team-hub.component.html',
  styleUrls: ['./team-hub.component.scss']
})
export class TeamHubComponent implements OnInit {

  constructor(private userSessionService: UserSessionService) { }

  participant?: ITeamParticipant;

  ngOnInit(): void {
    this.participant = this.userSessionService.getParticipant();
    // this.participant = MockTeamParticipant;
  
  }

}
