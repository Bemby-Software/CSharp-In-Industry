 import { Component, OnInit } from '@angular/core';
import { ICriteria } from 'src/app/models/criteria';

const dummyDescription: string = "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Lorem ipsum dolor sit, amet consectetur adipisicing elit. sit, amet consectetur adipisicing elit. Lorem ipsum dolor sit, amet consectetur adipisicing elit.";

@Component({
  selector: 'app-judged-on',
  templateUrl: './judged-on.component.html',
  styleUrls: ['./judged-on.component.scss']
})
export class JudgedOnComponent implements OnInit {

  criteria: ICriteria[] = [
    {
      title: "Readability",
      icon: "glasses",
      description: dummyDescription
    },
    {
      title: "Structure",
      icon: "warehouse",
      description: dummyDescription
    },
    {
      title: "Quality",
      icon: "clipboard-check",
      description: dummyDescription
    },
    {
      title: "Testing",
      icon: "vial",
      description: dummyDescription
    },
    {
      title: "Time Management",
      icon: "clock",
      description: dummyDescription
    },
    {
      title: "Process",
      icon: "project-diagram",
      description: dummyDescription
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }

  getClassForCriteriaIcon(i: ICriteria) {
    return `fas text-8xl fa-${i.icon}`;
  }

}
