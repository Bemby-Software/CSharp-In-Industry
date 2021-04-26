import { Component, Input, OnInit } from '@angular/core';
import { IMentor } from 'src/app/models/mentor';

@Component({
  selector: 'app-mentor-card',
  templateUrl: './mentor-card.component.html',
  styleUrls: ['./mentor-card.component.scss']
})
export class MentorCardComponent implements OnInit {

  @Input() mentor: IMentor | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
