import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamNameEntryComponent } from './team-name-entry.component';

describe('TeamNameEntryComponent', () => {
  let component: TeamNameEntryComponent;
  let fixture: ComponentFixture<TeamNameEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TeamNameEntryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TeamNameEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
