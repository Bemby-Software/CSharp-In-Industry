import { Component, OnInit } from '@angular/core';
import { CompetitionName } from 'src/app/utils/constants';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {
  

  title: string = CompetitionName;

  constructor() { }

  ngOnInit(): void {
  }

}
