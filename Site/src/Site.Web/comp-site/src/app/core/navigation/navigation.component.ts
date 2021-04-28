import { Component, OnInit } from '@angular/core';
import { CompetitionName } from 'src/app/utils/constants';
import {Router} from "@angular/router";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {


  title: string = CompetitionName;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  goToSignIn() {
    this.router.navigate(['/signin']);
  }

}
