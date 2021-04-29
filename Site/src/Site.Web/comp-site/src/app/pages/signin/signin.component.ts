import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {ParticipantService} from "../../services/participant.service";
import {of} from "rxjs";
import {UserSessionService} from "../../services/user-session.service";

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent implements OnInit {


  justSignedUp: boolean = false;

  emaail: string = "";
  token: string = "";

  constructor(private route: ActivatedRoute, private userSessionService: UserSessionService) {


    route.queryParams.subscribe(params => {

      if(params.hasOwnProperty('justSignedUp')) {
        this.justSignedUp = params["justSignedUp"];
      }
    })
  }

  ngOnInit(): void {
    this.userSessionService.signIn('test', 'test123')
      .subscribe(res => {

        console.log(res);
        if(res.successful) {

        }
        else {

        }
      });
  }

}
