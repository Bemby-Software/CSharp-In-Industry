import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { IParticipant } from 'src/app/models/participant';
import { ParticipantsService } from 'src/app/services/participants.service';

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
export class AddParticipantComponent implements OnInit, OnDestroy {

  participant: IParticipant = newParticipant;

  

  error: string = ""; 

  private errorSub: Subscription;

  constructor(private addService: ParticipantsService) { 
      this.errorSub = this.addService.onError().subscribe(error => {
        this.error = error;
      })
  }

  ngOnInit(): void {
    
  }

  onLeaveEmail() {
    this.addService.check(this.participant);
  }

  private clear() {
      this.participant.email = "";
      this.participant.forename = "";
      this.participant.surname = "";
  }

  onAddParticipant() {
    if(this.addService.add(this.participant)) {
        this.clear(); 
    };
  }

  ngOnDestroy(): void {
    this.errorSub.unsubscribe();
  }

}
