import { Component, OnInit } from '@angular/core';
import { ValidationService } from 'src/app/services/validation-service.service';

@Component({
  selector: 'app-team-name-entry',
  templateUrl: './team-name-entry.component.html',
  styleUrls: ['./team-name-entry.component.scss']
})
export class TeamNameEntryComponent implements OnInit {

  isTeamNameOk: boolean = true;
  teamName: string = "";

  constructor(private validationService: ValidationService) { }

  ngOnInit(): void {
    
  }

  onLeaveTeamNameBox() {


    if(this.teamName === "") {
      return;
    }

    this.validationService.isTeamNameInUse(this.teamName)
      .subscribe((isInUse) => {
        
        if(isInUse) {
          this.isTeamNameOk = false;
        }
        else {
          this.isTeamNameOk = true;
        }

      }, err => {
        this.isTeamNameOk = false;
        console.log(err);
      })
  }

}
