import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IParticipant } from 'src/app/models/participant';

const newParticipant = {
  id: 0,
  forename: "",
  surname: "",
  email: ""
}

@Component({
  selector: 'app-add-participant',
  templateUrl: './add-participant.component.html',
  styleUrls: ['./add-participant.component.scss']
})
export class AddParticipantComponent implements OnInit {

  participant: IParticipant = newParticipant;

  @Input() onClear = new EventEmitter();

  @Input() error: string = ""; 

  @Output() onValidateParticipant = new EventEmitter<IParticipant>();

  @Output() onNewParticipant = new EventEmitter<IParticipant>();

  constructor() { }

  ngOnInit(): void {
    this.onClear.subscribe(() => this.participant = newParticipant);
  }



  onLeaveEmail() {
    this.onValidateParticipant.emit(this.participant);
  }

  onAddParticipant() {
    this.onNewParticipant.emit(this.participant);    
  }

}
