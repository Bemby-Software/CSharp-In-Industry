import { Component, Input, OnInit } from '@angular/core';
import { Icons } from 'src/app/models/icons';



@Component({
  selector: 'app-social-button',
  templateUrl: './social-button.component.html',
  styleUrls: ['./social-button.component.scss']
})
export class SocialButtonComponent implements OnInit {

  @Input() url: string = "";
  @Input() icon: Icons = Icons.GitHub;

  constructor() { }

  ngOnInit(): void {
  }

}
