import { Component, Input, OnInit } from '@angular/core';
import { CompetitionName } from 'src/app/utils/constants';

@Component({
  selector: 'app-hero',
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.scss']
})
export class HeroComponent implements OnInit {

  @Input() scrollTo: HTMLElement | null;
  title: string = CompetitionName;

  constructor() { 
    this.scrollTo = null;
  }

  ngOnInit(): void {
  }

  goToSignUp() {
    this.scrollTo?.scrollIntoView({behavior: "smooth"});
  }

}
