import { Component, OnInit } from '@angular/core';
import { Icons } from 'src/app/models/icons';
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
      imageUrl: "https://mk0trickyphotos51tq5.kinstacdn.com/wp-content/uploads/2017/08/final-1.png",
      socials: [
        {icon: Icons.GitHub, url: ''},
        {icon: Icons.LinkedIn, url: ''},
        {icon: Icons.Twitter, url: ''},    
      ]
    },
    {
      name: "Billy",
      snapline: " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Odio, eligendi ipsam illo iste,architecto cumque vel magni ipsum fugit, quasi alias non impedit voluptas labore hic quam minus  commodi magnam.",
      imageUrl: "data:image/jpeg;base64,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",
      socials: [
        {icon: Icons.GitHub, url: ''},
        {icon: Icons.LinkedIn, url: ''},
        {icon: Icons.Twitter, url: ''},        
      ]
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
