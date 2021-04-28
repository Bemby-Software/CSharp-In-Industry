import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {AuthService} from "../../services/auth.service";
import {of} from "rxjs";

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent implements OnInit {


  justSignedUp: boolean = false;

  emaail: string = "";
  token: string = "";

  constructor(private route: ActivatedRoute, private authService: AuthService) {


    route.queryParams.subscribe(params => {

      if(params.hasOwnProperty('justSignedUp')) {
        this.justSignedUp = params["justSignedUp"];
      }
    })
  }

  ngOnInit(): void {
    this.authService.signIn('test', 'test123')
      .subscribe(res => console.log(res));
  }

}
