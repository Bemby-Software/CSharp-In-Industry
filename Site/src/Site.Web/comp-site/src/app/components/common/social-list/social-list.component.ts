import { Component, Input, OnInit } from '@angular/core';
import { ISocialLink } from 'src/app/models/social';

@Component({
  selector: 'app-social-list',
  templateUrl: './social-list.component.html',
  styleUrls: ['./social-list.component.scss']
})
export class SocialListComponent implements OnInit {


  @Input() buttons: ISocialLink[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
