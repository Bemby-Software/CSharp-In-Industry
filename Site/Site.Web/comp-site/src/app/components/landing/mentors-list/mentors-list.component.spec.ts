import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MentorsListComponent } from './mentors-list.component';

describe('MentorsListComponent', () => {
  let component: MentorsListComponent;
  let fixture: ComponentFixture<MentorsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MentorsListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MentorsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
