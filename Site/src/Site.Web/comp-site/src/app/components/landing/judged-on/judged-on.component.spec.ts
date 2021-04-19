import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JudgedOnComponent } from './judged-on.component';

describe('JudgedOnComponent', () => {
  let component: JudgedOnComponent;
  let fixture: ComponentFixture<JudgedOnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JudgedOnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JudgedOnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
