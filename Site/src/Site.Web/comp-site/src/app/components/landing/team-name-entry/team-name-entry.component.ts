import { i18nMetaToJSDoc } from '@angular/compiler/src/render3/view/i18n/meta';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ValidationService } from 'src/app/services/validation.service';

const teamNameInUse: string = "Oops! That team name is in use.";
const teamNameRequired: string = "Oops! A team name is required";

@Component({
  selector: 'app-team-name-entry',
  templateUrl: './team-name-entry.component.html',
  styleUrls: ['./team-name-entry.component.scss']
})
export class TeamNameEntryComponent implements OnInit {
  teamName: string = "";
  teamNameError = "";



  constructor(private validationService: ValidationService, private router: Router) { }

  ngOnInit(): void {

  }

  checkTeamName() {


    if (this.teamName === "") {
      this.teamNameError = teamNameRequired;
      return;
    }

    this.validationService.isTeamNameInUse(this.teamName)
      .subscribe((isInUse) => {

        if (isInUse) {
          this.teamNameError = teamNameInUse;
        }
        else {
          this.teamNameError = "";
        }

      }, err => {
        this.teamNameError = "Oops! Sorry something went wrong";
        console.log(err);
      })
  }


  onSignUp() {

    if (this.teamName === "") {
      this.teamNameError = teamNameRequired;
      return;
    }

    this.validationService.isTeamNameInUse(this.teamName)
      .subscribe((isInUse) => {

        if (isInUse) {
          this.teamNameError = teamNameInUse;
        }
        else {
          this.router.navigate(["/signup"], { queryParams: { name: this.teamName } });
        }

      }, err => {
        this.teamNameError = "Oops! Sorry something went wrong";
        console.log(err);
      })
  }

}
