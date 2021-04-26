import { Component, OnInit } from '@angular/core';
import { ISponsor } from 'src/app/models/sponsor';

@Component({
  selector: 'app-sponsors-list',
  templateUrl: './sponsors-list.component.html',
  styleUrls: ['./sponsors-list.component.scss']
})
export class SponsorsListComponent implements OnInit {

  sponsors: ISponsor[] = [
    {name: "Next"},
    {name: "VIP Digital"},
    {name: "Version 1"},
    {name: "Hull University"},
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
