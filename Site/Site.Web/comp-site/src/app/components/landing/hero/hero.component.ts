import { ThrowStmt } from '@angular/compiler';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-hero',
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.scss']
})
export class HeroComponent implements OnInit {

  @Input() scrollTo: HTMLElement | null;

  constructor() { 
    this.scrollTo = null;
  }

  ngOnInit(): void {
  }

  goToSignUp() {
    this.scrollTo?.scrollIntoView({behavior: "smooth"});
  }

}
