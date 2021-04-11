import { Component, OnInit } from '@angular/core';
import { IMentor } from 'src/app/models/mentor';

@Component({
  selector: 'app-mentors-list',
  templateUrl: './mentors-list.component.html',
  styleUrls: ['./mentors-list.component.scss']
})
export class MentorsListComponent implements OnInit {

  mentors: IMentor[] = [
    {
      name: "Rob",
      snapline: " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Odio, eligendi ipsam illo iste,architecto cumque vel magni ipsum fugit, quasi alias non impedit voluptas labore hic quam minus  commodi magnam.",
      imageUrl: "https://images.ctfassets.net/1wryd5vd9xez/4DxzhQY7WFsbtTkoYntq23/a4a04701649e92a929010a6a860b66bf/https___cdn-images-1.medium.com_max_2000_1_Y6l_FDhxOI1AhjL56dHh8g.jpeg"
    },
    {
      name: "Billy",
      snapline: " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Odio, eligendi ipsam illo iste,architecto cumque vel magni ipsum fugit, quasi alias non impedit voluptas labore hic quam minus  commodi magnam.",
      imageUrl: "https://images.ctfassets.net/1wryd5vd9xez/4DxzhQY7WFsbtTkoYntq23/a4a04701649e92a929010a6a860b66bf/https___cdn-images-1.medium.com_max_2000_1_Y6l_FDhxOI1AhjL56dHh8g.jpeg"
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
