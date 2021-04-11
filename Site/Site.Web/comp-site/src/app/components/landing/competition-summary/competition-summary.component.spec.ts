import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompetitionSummaryComponent } from './competition-summary.component';

describe('CompetitionSummaryComponent', () => {
  let component: CompetitionSummaryComponent;
  let fixture: ComponentFixture<CompetitionSummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompetitionSummaryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompetitionSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
