import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IParticipant } from 'src/app/models/participant';

@Component({
  selector: 'app-participants-table',
  templateUrl: './participants-table.component.html',
  styleUrls: ['./participants-table.component.scss']
})
export class ParticipantsTableComponent implements OnInit {

  @Input() participants: IParticipant[] = []

  @Output() onParticipantRemoved = new EventEmitter<string>();

  onRemove(participant: IParticipant) {    
    this.onParticipantRemoved.emit(participant.email);
  }

  constructor() { }

  ngOnInit(): void {
  }

}
