import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GithubAccountComponent } from './github-account.component';

describe('GithubAccountComponent', () => {
  let component: GithubAccountComponent;
  let fixture: ComponentFixture<GithubAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GithubAccountComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GithubAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
