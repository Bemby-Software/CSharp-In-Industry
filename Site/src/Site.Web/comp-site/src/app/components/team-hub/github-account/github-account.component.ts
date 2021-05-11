import { Component, OnInit } from '@angular/core';
import { UserSessionService } from 'src/app/services/user-session.service';

@Component({
  selector: 'app-github-account',
  templateUrl: './github-account.component.html',
  styleUrls: ['./github-account.component.scss']
})
export class GithubAccountComponent implements OnInit {
  

  repo: string = '';

  get accountLinked() {
    return false;
  }

  constructor(private userSessionService: UserSessionService) { }

  ngOnInit(): void {
      
  }

}
