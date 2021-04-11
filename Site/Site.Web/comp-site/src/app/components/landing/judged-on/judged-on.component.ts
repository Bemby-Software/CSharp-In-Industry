 import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-judged-on',
  templateUrl: './judged-on.component.html',
  styleUrls: ['./judged-on.component.scss']
})
export class JudgedOnComponent implements OnInit {

  list: number[] = [1,2,3,4,5,6];

  constructor() { }

  ngOnInit(): void {
  }

}
