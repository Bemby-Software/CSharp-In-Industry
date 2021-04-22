import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IParticipant } from 'src/app/models/participant';
import { ITeam } from 'src/app/models/team';

@Component({
  selector: 'app-participants-table',
  templateUrl: './participants-table.component.html',
  styleUrls: ['./participants-table.component.scss']
})
export class ParticipantsTableComponent implements OnInit {

  @Input() participants: IParticipant[] = []

  @Output() onParticipantRemoved = new EventEmitter<string>();

  onRemove(email: string) {
    console.log(email)
    this.onParticipantRemoved.emit(email);
  }

  constructor() { }

  ngOnInit(): void {
  }

}
