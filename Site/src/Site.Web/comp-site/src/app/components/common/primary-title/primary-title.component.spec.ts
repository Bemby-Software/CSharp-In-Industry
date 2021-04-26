import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimaryTitleComponent } from './primary-title.component';

describe('PrimaryTitleComponent', () => {
  let component: PrimaryTitleComponent;
  let fixture: ComponentFixture<PrimaryTitleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrimaryTitleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimaryTitleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
