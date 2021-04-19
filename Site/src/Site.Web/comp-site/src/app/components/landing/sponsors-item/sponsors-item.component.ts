import { Component, Input, OnInit } from '@angular/core';
import { ISponsor } from 'src/app/models/sponsor';

@Component({
  selector: 'app-sponsors-item',
  templateUrl: './sponsors-item.component.html',
  styleUrls: ['./sponsors-item.component.scss']
})
export class SponsorsItemComponent implements OnInit {

  @Input() sponsor: ISponsor | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
