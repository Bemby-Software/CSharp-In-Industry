import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {UserSessionService} from "../../services/user-session.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent implements OnInit {


  justSignedUp: boolean = false;

  email: string = "";
  token: string = "";
  error: string = ""

  constructor(private route: ActivatedRoute, private router: Router, private userSessionService: UserSessionService, private toastRService: ToastrService) {


    route.queryParams.subscribe(params => {

      if(params.hasOwnProperty('justSignedUp')) {
        this.justSignedUp = params["justSignedUp"];
      }
    })
  }

  get failed() {
    return this.error != "";
  }

  ngOnInit(): void {
  }

  signIn() {
    this.userSessionService.signIn(this.email, this.token)
      .subscribe(res => {
        console.log(res);
        if(res.successful) {
            this.router.navigate(['/team-hub'])
        }
        else {
          this.toastRService.error(res.error);
          this.error = res.error;
        }
      });
  }

}
