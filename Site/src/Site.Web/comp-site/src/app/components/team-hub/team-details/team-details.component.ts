import { Component, Input, OnInit } from '@angular/core';
import { ITeam } from 'src/app/models/team';

@Component({
  selector: 'app-team-details',
  templateUrl: './team-details.component.html',
  styleUrls: ['./team-details.component.scss']
})
export class TeamDetailsComponent implements OnInit {

  @Input() team?: ITeam

  constructor() { }

  ngOnInit(): void {
  }

}
