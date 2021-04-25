import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IParticipant } from 'src/app/models/participant';
import { ParticipantsService } from 'src/app/services/participants-service.service';

@Component({
  selector: 'app-participants-table',
  templateUrl: './participants-table.component.html',
  styleUrls: ['./participants-table.component.scss']
})
export class ParticipantsTableComponent implements OnInit {

  @Input() participants: IParticipant[] = []


  onRemove(participant: IParticipant) {    
    this.participantsService.remove(participant.email);
  }

  constructor(private participantsService: ParticipantsService) { }

  ngOnInit(): void {
  }

}
