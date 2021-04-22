import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IParticipant } from 'src/app/models/participant';

@Component({
  selector: 'app-add-participant',
  templateUrl: './add-participant.component.html',
  styleUrls: ['./add-participant.component.scss']
})
export class AddParticipantComponent implements OnInit {

  participant: IParticipant = {
    id: 0,
    forename: "",
    surname: "",
    email: ""
  }

  @Input() error: string = ""; 

  @Output() onValidateParticipant = new EventEmitter<IParticipant>();

  @Output() onNewParticipant = new EventEmitter<IParticipant>();

  constructor() { }

  ngOnInit(): void {
  }



  onLeaveEmail() {
    this.onValidateParticipant.emit(this.participant);
  }

  onAddParticipant() {
    this.onNewParticipant.emit(this.participant);    
  }

}
