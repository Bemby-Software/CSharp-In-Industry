import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SponsorsItemComponent } from './sponsors-item.component';

describe('SponsorsItemComponent', () => {
  let component: SponsorsItemComponent;
  let fixture: ComponentFixture<SponsorsItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SponsorsItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SponsorsItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
