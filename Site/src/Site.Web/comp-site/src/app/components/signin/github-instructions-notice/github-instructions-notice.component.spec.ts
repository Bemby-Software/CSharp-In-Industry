import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GithubInstructionsNoticeComponent } from './github-instructions-notice.component';

describe('GithubInstructionsNoticeComponent', () => {
  let component: GithubInstructionsNoticeComponent;
  let fixture: ComponentFixture<GithubInstructionsNoticeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GithubInstructionsNoticeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GithubInstructionsNoticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
